# Heširanje šifara brokera (Faza 3)

Jedini izvor pravila za Fazu 3. Odluke su potvrđene 10. 11. 2026.

**Cilj:** šifre brokera se u bazi čuvaju kao BCrypt heš (biblioteka `BCrypt.Net-Next`), a heš nikad ne napušta server. Šifra u čitljivom obliku putuje samo od klijenta ka serveru (kreiranje, izmena, prijava); server je hešira ili proverava i nikad je ne vraća klijentu.

## Tok podataka

| Operacija | Klijent šalje | Upisuje se u bazu | Vraća se klijentu |
| --- | --- | --- | --- |
| Kreiranje (`KreirajBrokerSO`) | `Broker` sa `Sifra` (proverena pravilima iz Faze 2; potvrdu proverava samo klijent, H4) | `sifraHash = BCrypt.HashPassword(Sifra, workFactor)` (H2, H7) | `Broker` sa `Sifra = null`; `SifraHash` se ne serijalizuje (H1, H8) |
| Izmena sa novom šifrom (`PromeniBrokerSO`) | `Broker` sa `Sifra` = nova šifra | novi heš u `sifraHash`; ostale kolone kao do sada | `Broker` sa `Sifra = null` (H8) |
| Izmena bez nove šifre (`PromeniBrokerSO`) | `Broker` sa `Sifra = null` ili praznom | kolona `sifraHash` se ne upisuje, heš ostaje isti (H8) | `Broker` sa `Sifra = null` |
| Prijava (`PrijaviBrokerSO`) | `KorisnickoIme` i `Sifra` | ništa | uspeh: `Broker` sa `Sifra = null`; neuspeh: „Korisničko ime ili šifra nisu ispravni.“ (H6) |
| Pretraga i liste (`PretraziBrokerSO`, `VratiListu…BrokerSO`) | kriterijum; popunjena `Sifra` u kriterijumu se ignoriše | ništa | `Broker` sa `Sifra = null`; `SifraHash` se ne serijalizuje (H1) |

Prijava se više ne radi upitom sa šifrom u uslovu. `PrijaviBrokerSO` čita brokera po korisničkom imenu (tačno poređenje), pa poredi unetu šifru sa hešom iz baze (`BCrypt.Verify`). Ako korisničko ime ne postoji, provera se ipak radi nad lažnim hešom (H6).

## Implementacija

Stanje na kraju Faze 3 (13. 11. 2026.). Putanje fajlova su relativne u odnosu na folder `Projektovanje-Softvera`, a skripte baze na koren repozitorijuma (`db/`).

| Gde | Klasa / fajl | Uloga |
| --- | --- | --- |
| `Common/Domen` | `Broker` | `Sifra` je `string?` i samo ulaz od klijenta. Novo svojstvo `SifraHash` (`string?`, `[JsonIgnore]`) puni `GetReaderList` iz kolone `sifraHash`; `Sifra` se nikad ne čita iz baze (H1). `InsertColumns` / `InsertValues` upisuju `sifraHash`. `UpdateValues` dodaje `sifraHash = N'…'` samo kad `SifraHash` nije prazan (H8). Iz `SearchCondition` je uklonjena grana za prijavu (`b.korisnickoIme = … AND b.sifra = …`); popunjena `Sifra` u kriterijumu se ignoriše. `SelectColumns` nije menjan (H1) |
| `Common/Validacija` | `Ogranicenja.BrokerSifraHash` | konstanta 100 (dužina kolone `sifraHash`), ranije `BrokerSifra`; `SifraMax` (64) ostaje pravilo za unos |
| | `PravilaBroker.ProveriPotvrduSifre(sifra, potvrda)` | poređenje razlikuje velika i mala slova, `null` i prazno su isto; greška na polju `PotvrdaSifre`, poruka „Šifra i potvrda šifre se ne poklapaju.“; koristi je samo klijent (H4) |
| `SistemskeOperacije` | `SistemskeOperacije.csproj` | jedini projekat sa paketom `BCrypt.Net-Next` 4.2.0 (H8) |
| | `Bezbednost/PasswordHasher` | `Hash(password)`: `BCrypt.HashPassword` sa work factor-om iz `PasswordHashingConfig`; za `null` ili praznu šifru `ArgumentException` („Šifra za heširanje nije prosleđena.“). `Verify(password, hash)`: `BCrypt.Verify`; vraća `false` bez izuzetka za prazan ili neispravan heš (`SaltParseException`, `ArgumentException`) i za praznu šifru. Za prazan heš proverava unetu šifru nad lažnim hešom (`dummyHash`, `Lazy`, nasumična vrednost, isti work factor), da vreme odziva bude isto (H6, H10, H11) |
| | `Bezbednost/PasswordHashingConfig` | `WorkFactor` se čita jednom (`Lazy`) iz `appsettings.json` u `AppContext.BaseDirectory`, ključ `PasswordHashing:WorkFactor`. Ključ je obavezan: bez njega „Nedostaje podešavanje 'PasswordHashing:WorkFactor'.“, van opsega 4–31 „Podešavanje 'PasswordHashing:WorkFactor' mora biti ceo broj od 4 do 31.“ (`InvalidOperationException`, H2) |
| | `BrokerSO/KreirajBrokerSO` | posle validacije `SifraHash = PasswordHasher.Hash(Sifra)`, upis, pa `Sifra = null` na rezultatu (H8) |
| | `BrokerSO/PromeniBrokerSO` | `SifraHash` je heš nove šifre ili `null` kad nova šifra nije uneta (tada se kolona ne upisuje); `Sifra = null` na rezultatu (H8) |
| | `PrijaviBrokerSO` | čita brokera po korisničkom imenu (`b.korisnickoIme = N'…'`, tačno poređenje) i poziva `PasswordHasher.Verify(Sifra, SifraHash)`; nepostojeće korisničko ime ide u `Verify` sa `null` hešom (lažni heš, H6). Poruka greške je nepromenjena |
| `Server`, `MigracijaSifara`, `SistemskeOperacije.Tests` | `appsettings.json` | `PasswordHashing:WorkFactor`: server 11, alat 11, testovi 4 (H2) |
| `db` (koren repozitorijuma) | `MigrationPasswordHash.sql` | `sp_rename` kolone `Broker.sifra` u `sifraHash` u bazi `Seminarski-Softveri`; sme više puta (H3) |
| | `CreateDatabase.sql`, `CreateTestDatabase.sql` | kolona `sifraHash nvarchar(100) NOT NULL`; početni broker `vtonic` (id 1) dobija BCrypt heš (work factor 11) šifre iz `SPECIFIKACIJA-TESTOVA.md`, odeljak 1; isto i u `dbo.ResetujBazu` test baze |
| | `Dummy.sql` | isti heš za brokera `vtonic` |
| `MigracijaSifara` | `Program.cs`, `README.md`, `appsettings.json`, `MigracijaSifara.csproj` (u `Projektovanje-Softvera.sln`) | bez argumenata hešira vrednosti koje ne počinju sa `$2`, u jednoj transakciji, uz potvrdu `d/n`; odbija bazu čiji naziv završava na `_Test` i bazu bez kolone `sifraHash`. `--hesiraj "<šifra>"` ispisuje samo heš zadate šifre, bez baze. Koristi `SqlParameter` i nikad ne ispisuje šifre ni heševe iz baze (H3) |
| `Klijent` | `UserControls/UCBroker` (+ `Designer`) | polje i labela „Potvrda šifre“ (`txtPotvrdaSifre`, `MaxLength` 64); u izmeni labele „Nova šifra“ / „Potvrda nove šifre“ i tooltip „Ostavite prazno ako ne menjate šifru.“; šifra se ne popunjava iz objekta; prazno polje daje `Sifra = null`; u detaljima su oba polja sakrivena (H4) |
| | `GuiControllers/BrokerGuiController.Validiraj` | greške iz `PravilaBroker.Proveri` i `ProveriPotvrduSifre` prikazuju se zajedno pored polja (H4) |
| | `FrmLogin` | `txtSifra.MaxLength = Ogranicenja.SifraMax` (64) (H8) |
| `SistemskeOperacije.Tests` | `TestBaza.SifraHash(idBroker)` | čita `sifraHash` direktno iz baze |
| | `Bezbednost/PasswordHasherTest` i izmene testova brokera | `SPECIFIKACIJA-TESTOVA.md`, 4.10 |
| `Common.Tests` | `PravilaBrokerTest` (potvrda šifre), `OgranicenjaTest` | `SPECIFIKACIJA-TESTOVA.md`, 4.10 |

## Rezultat

**Testovi** (`dotnet test`, tri uzastopna pokretanja 13. 11. 2026., isti rezultat u sva tri; LocalDB, Debug, `--no-build`)

| Projekat | Pre Faze 3 (kraj Faze 2) | Posle Faze 3 |
| --- | --- | --- |
| `SistemskeOperacije.Tests` – integracioni (nasleđuju `IntegracioniTest`) | 411 (409 prošlo, 2 preskočeno) | 420 (419 prošlo, 1 preskočeno: `TestIzvrsiOperacijuPretragaDzokerZnak`) |
| `SistemskeOperacije.Tests` – bez baze (`PasswordHasherTest`, `JsonNetworkSerializerTest`) | 4 | 16 |
| `SistemskeOperacije.Tests` ukupno | 415 (413 prošlo, 2 preskočeno) | 436 (435 prošlo, 1 preskočeno, 0 palo) |
| `Common.Tests` (jedinični) | 262, svi prolaze | 273, svi prolaze |
| Trajanje `SistemskeOperacije.Tests` (izveštaj `dotnet test`) | nije zabeleženo | 8 s, 7 s, 7 s |
| Trajanje `Common.Tests` | nije zabeleženo | 170 ms, 189 ms, 220 ms |
| Ceo `dotnet test` (zidno vreme) | nije zabeleženo | 10,9 s, 10,6 s, 10,6 s |

Trajanje pre Faze 3 nije zapisano na kraju Faze 2, a ne može naknadno da se izmeri na istoj bazi, jer kod iz Faze 2 čita kolonu `sifra`, koje u test bazi više nema. Novih testova je 21 + 11: 11 slučajeva u `PasswordHasherTest`, 1 u `JsonNetworkSerializerTest`, 9 integracionih i 11 slučajeva za potvrdu šifre u `PravilaBrokerTest`; `TestIzvrsiOperacijuBezNoveSifre` više nije preskočen. Work factor u testovima je 4, pa heširanje praktično ne utiče na trajanje.

**Provere u kodu** (13. 11. 2026.)

- `SifraHash` / `sifraHash` se pojavljuje samo u `Broker`, `KreirajBrokerSO`, `PromeniBrokerSO`, `PrijaviBrokerSO`, alatu `MigracijaSifara`, testovima i SQL skriptama. Jedini dodatak je konstanta `Ogranicenja.BrokerSifraHash` (dužina kolone) i njen red u `OgranicenjaTest`. `PasswordHasher` radi sa parametrom `hash` i ne koristi svojstvo.
- Heš ne ide u odgovor (`[JsonIgnore]`, test `TestBrokerSifraHashSeNeSerijalizuje`), ni u `Debug.WriteLine` servera (ispisuju se samo poruke izuzetaka; heš ima 60 znakova, pa upis u `nvarchar(100)` ne može da izazove grešku skraćivanja sa vrednošću), ni u `MessageBox` (klijent nikad nema heš; tabela brokera ima `AutoGenerateColumns = false`). `Broker.ToString()` ne sadrži ni šifru ni heš. `MigracijaSifara --hesiraj` ispisuje heš šifre zadate u komandnoj liniji, nikad iz baze.
- `Klijent` i `Common` ne referenciraju BCrypt, ni direktno ni preko zavisnosti; paket je samo u `SistemskeOperacije` (`MigracijaSifara`, `Server` i `SistemskeOperacije.Tests` ga dobijaju preko reference na taj projekat).
- Šifra početnog brokera u čitljivom obliku postoji samo u testovima (`PasswordHasherTest`, `PrijaviBrokerSOTest`) i dokumentaciji (`SPECIFIKACIJA-TESTOVA.md`, `MigracijaSifara/README.md`); u SQL skriptama je samo njen heš.

**Baze** (13. 11. 2026., samo čitanje): `Seminarski-Softveri` 5 brokera, svih 5 vrednosti u `sifraHash` počinju sa `$2`; `Seminarski-Softveri_Test` 1 broker posle `ResetujBazu`, heš. Kolona `sifra` ne postoji ni u jednoj bazi.

**Ručne provere** (klijent i server: kreiranje sa potvrdom šifre, izmena bez i sa novom šifrom, prijava, detalji brokera; alat `MigracijaSifara`): 13. 11. 2026.

## Ograničenja

- **Šifra putuje kroz TCP u čitljivom obliku.** Heširanje štiti šifre u bazi, ali ne i na mreži. Moguće rešenje: `SslStream` nad postojećom TCP vezom.
- **BCrypt koristi samo prva 72 bajta UTF-8.** 64 ASCII znaka staju u 72 bajta, ali slova č, ć, š, ž, đ zauzimaju po 2 bajta, pa šifra od 64 znaka sa njima može da pređe 72 bajta; ostatak se tada ignoriše (H7).
- **Pri promeni šifre ne traži se trenutna šifra** (H5).
- **Nema zaključavanja naloga ni ograničenja broja pokušaja prijave.**
- **Promena work factor-a ne menja postojeće heševe.** Heš nosi work factor sa kojim je napravljen; nema ponovnog heširanja pri prijavi.

## Odluke

| # | Odluka |
| --- | --- |
| H1 | `Broker` dobija svojstvo `SifraHash` sa `[JsonIgnore]`. `Sifra` je samo ulaz od klijenta ka serveru i nikad se ne čita iz baze. `SelectColumns` za `Broker` se ne menja, jer su eksplicitne kolone kandidat za optimizaciju 6.2 (10. 11. 2026.) |
| H2 | Work factor se čita iz `appsettings.json`, ključ `PasswordHashing:WorkFactor` (svojstvo `PasswordHashingConfig.WorkFactor`, H10): server 11, testovi 4, dozvoljen opseg 4–31. Ključ je obavezan (10. 11. 2026.) |
| H3 | Kolona `Broker.sifra` se preimenuje u `sifraHash` (tip ostaje `nvarchar(100)`). Razvojna baza se prevodi SQL skriptom `db/MigrationPasswordHash.sql` (preimenovanje kolone) i konzolnim alatom `MigracijaSifara`, koji hešira samo vrednosti koje još nisu heš (ne počinju sa `$2`). Oba smeju da se pokrenu više puta (10. 11. 2026.) |
| H4 | Forma brokera ima potvrdu šifre pri kreiranju i pri izmeni. Kreiranje: „Šifra“ i „Potvrda šifre“. Izmena: „Nova šifra“ i „Potvrda nove šifre“; prazna polja znače da šifra ostaje ista, a polje se nikad ne popunjava iz baze. Detalji: oba polja su sakrivena. Potvrdu proverava `PravilaBroker.ProveriPotvrduSifre` u projektu `Common`, samo na klijentu, jer server ne dobija potvrdu (10. 11. 2026.) |
| H5 | Pri promeni šifre ne traži se unos trenutne šifre (ograničenje) (10. 11. 2026.) |
| H6 | Za nepostojeće korisničko ime `PrijaviBrokerSO` radi BCrypt proveru nad lažnim hešom, da vreme odziva ne otkriva da li korisničko ime postoji. Poruka ostaje „Korisničko ime ili šifra nisu ispravni.“ (10. 11. 2026.) |
| H7 | Koristi se standardni `BCrypt.HashPassword` / `BCrypt.Verify`. Pravilo 8–64 znaka ostaje; granica od 72 bajta UTF-8 navodi se kao ograničenje (64 znaka sa č, ć, š, ž, đ mogu da pređu 72 bajta) (10. 11. 2026.) |
| H8 | `MaxLength` šifre na `FrmLogin` je 64. `KreirajBrokerSO` i `PromeniBrokerSO` posle heširanja postavljaju `Sifra = null` na objektu koji vraćaju. `PromeniBrokerSO` bez nove šifre ne upisuje kolonu `sifraHash`. Paket `BCrypt.Net-Next` je samo u projektu `SistemskeOperacije`. Postojeće šifre koje ne zadovoljavaju pravila iz faze 2 heširaju se kakve jesu, jer prijava ne proverava format (10. 11. 2026.) |
| H9 | Dokumentacija za rad iz faze 2 (vrednosna ograničenja, ugovori SO) radi se zajedno sa dokumentacijom faze 3 (10. 11. 2026.) |
| H10 | Komponente koje samo omotavaju spoljnu biblioteku imenuju se na engleskom, kao SOBase i DBBroker: PasswordHasher (Hash, Verify) i PasswordHashingConfig (WorkFactor), ključ PasswordHashing:WorkFactor, test PasswordHasherTest. Folder, domenski nazivi, pravila validacije, alat MigracijaSifara i poruke ostaju na srpskom (12. 11. 2026.) |
| H11 | Dopuna H10: u omotačima PasswordHasher i PasswordHashingConfig i unutrašnji nazivi su na engleskom – privatna polja, konstante i lokalne promenljive (`dummyHash`, `Key`, `value`, `wfNumber`). Poruke izuzetaka ostaju na srpskom. Test klasa PasswordHasherTest zadržava srpske nazive test metoda i promenljivih, kao ostali testovi (13. 11. 2026.) |
