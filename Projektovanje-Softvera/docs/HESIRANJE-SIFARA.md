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

Tabela se dopunjuje na kraju faze.

| Gde | Klasa | Uloga |
| --- | --- | --- |
| `Common/Domen` | `Broker` | novo svojstvo `SifraHash` sa `[JsonIgnore]`; čita se iz kolone `sifraHash`; `Sifra` se nikad ne čita iz baze (H1) |
| `Common/Validacija` | `PravilaBroker.ProveriPotvrduSifre` | poređenje šifre i potvrde, greška na polju `PotvrdaSifre`; koristi ga samo klijent (H4) |
| `SistemskeOperacije` | `Sifre` | `HashPassword` i `Verify` iz `BCrypt.Net-Next`; work factor iz `Sifre:BCryptWorkFactor` (H2); `Proveri` vraća `false` bez izuzetka za `null`, prazan ili neispravan heš |
| | `KreirajBrokerSO`, `PromeniBrokerSO` | heširaju šifru pre upisa i postavljaju `Sifra = null` na rezultatu; `PromeniBrokerSO` bez nove šifre ne upisuje `sifraHash` (H8) |
| | `PrijaviBrokerSO` | čitanje po korisničkom imenu, provera heša, lažni heš za nepostojeće korisničko ime (H6) |
| `Server` / `SistemskeOperacije.Tests` | `appsettings.json` | ključ `Sifre:BCryptWorkFactor`: server 11, testovi 4 (H2) |
| `db` | `MigracijaFaza3.sql` | preimenovanje kolone `Broker.sifra` u `sifraHash` (H3) |
| `MigracijaSifara` | konzolni alat | hešira postojeće vrednosti koje još nisu heš (H3) |
| `Klijent` | `UCBroker`, `FrmLogin` | potvrda šifre, polje „Nova šifra“ u izmeni, `MaxLength` 64 na prijavi (H4, H8) |
| `SistemskeOperacije.Tests` | `TestBaza`, `Bezbednost/SifreTest` | pomoćni metod za čitanje `sifraHash` iz baze; jedinični testovi klase `Sifre` (`SPECIFIKACIJA-TESTOVA.md`, 4.10) |

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
| H2 | Work factor se čita iz `appsettings.json`, ključ `Sifre:BCryptWorkFactor`: server 11, testovi 4, dozvoljen opseg 4–31. Ključ je obavezan (10. 11. 2026.) |
| H3 | Kolona `Broker.sifra` se preimenuje u `sifraHash` (tip ostaje `nvarchar(100)`). Razvojna baza se prevodi SQL skriptom `db/MigracijaFaza3.sql` (preimenovanje kolone) i konzolnim alatom `MigracijaSifara`, koji hešira samo vrednosti koje još nisu heš (ne počinju sa `$2`). Oba smeju da se pokrenu više puta (10. 11. 2026.) |
| H4 | Forma brokera ima potvrdu šifre pri kreiranju i pri izmeni. Kreiranje: „Šifra“ i „Potvrda šifre“. Izmena: „Nova šifra“ i „Potvrda nove šifre“; prazna polja znače da šifra ostaje ista, a polje se nikad ne popunjava iz baze. Detalji: oba polja su sakrivena. Potvrdu proverava `PravilaBroker.ProveriPotvrduSifre` u projektu `Common`, samo na klijentu, jer server ne dobija potvrdu (10. 11. 2026.) |
| H5 | Pri promeni šifre ne traži se unos trenutne šifre (ograničenje) (10. 11. 2026.) |
| H6 | Za nepostojeće korisničko ime `PrijaviBrokerSO` radi BCrypt proveru nad lažnim hešom, da vreme odziva ne otkriva da li korisničko ime postoji. Poruka ostaje „Korisničko ime ili šifra nisu ispravni.“ (10. 11. 2026.) |
| H7 | Koristi se standardni `BCrypt.HashPassword` / `BCrypt.Verify`. Pravilo 8–64 znaka ostaje; granica od 72 bajta UTF-8 navodi se kao ograničenje (64 znaka sa č, ć, š, ž, đ mogu da pređu 72 bajta) (10. 11. 2026.) |
| H8 | `MaxLength` šifre na `FrmLogin` je 64. `KreirajBrokerSO` i `PromeniBrokerSO` posle heširanja postavljaju `Sifra = null` na objektu koji vraćaju. `PromeniBrokerSO` bez nove šifre ne upisuje kolonu `sifraHash`. Paket `BCrypt.Net-Next` je samo u projektu `SistemskeOperacije`. Postojeće šifre koje ne zadovoljavaju pravila iz faze 2 heširaju se kakve jesu, jer prijava ne proverava format (10. 11. 2026.) |
| H9 | Dokumentacija za rad iz faze 2 (vrednosna ograničenja, ugovori SO) radi se zajedno sa dokumentacijom faze 3 (10. 11. 2026.) |
