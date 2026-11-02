# Specifikacija integracionih testova – SistemskeOperacije.Tests

Ovaj dokument opisuje sve integracione testove iz koraka 0.5 Faze 0. Testovi su sigurnosna mreža za izmene iz faza 1–6 (apostrof, validacija, heširanje, parametrizacija upita, optimizacije). Svaka sistemska operacija (SO) dobija bar jedan test koji izvršava njen SQL; dubina zavisi od nivoa rizika.

| Nivo | Entiteti | Pokrivenost |
| --- | --- | --- |
| A – detaljno | Listing, Karta, PrijaviBroker | sva poslovna pravila, svaki kriterijum pretrage, vezivanje i oslobađanje karata |
| B – svaka SQL putanja | Dogadjaj, Broker, Konsignator | uspešan test po SO, jedinstvenost, blokirajuće zavisnosti, po jedan test po tipu kriterijuma |
| C – smoke | KategorijaDogadjaja | uspešan test po SO, blokirano brisanje |

---

## 1. Postojeća infrastruktura (ne menja se)

| Fajl | Uloga |
| --- | --- |
| `PodesavanjeTestova.cs` | `[assembly: DoNotParallelize]`; `[AssemblyInitialize]` odbija bazu bez sufiksa `_Test` |
| `IntegracioniTest.cs` | bazna klasa; `[TestInitialize]` poziva `dbo.ResetujBazu` i postavlja kulturu `sr-Latn-RS` |
| `TestBaza.cs` | `Execute(sql)`, `BrojRedova(tabela, uslov)`, `Postoji(tabela, uslov)` – direktno nad bazom, mimo SO |
| `TestPodaci.cs` | fabrike test podataka (dopunjuje se, odeljak 3) |
| `appsettings.json` | ključ `SeminarskiSoftveri` → `Seminarski-Softveri_Test` |

Stanje baze posle `ResetujBazu` (pre svakog testa):

- `Broker`: id 1, korisničko ime `vtonic`, šifra `Sifra#2026`.
- `KategorijaDogadjaja`: id 1 `Koncert`, id 2 `Sportski događaj`, id 3 `Pozorišna predstava`.
- Sve ostale tabele su prazne.

Test se ne oslanja na konkretne Id-eve, osim na kategorije 1–3 tamo gde je to izričito navedeno. Sve ostalo pravi sam, kroz `TestPodaci`.

---

## 2. Pravila za pisanje testova

1. **Jedna test klasa po SO.** Ime klase je `<ImeSO>Test`, fajl `<ImeSO>Test.cs`, u folderu entiteta (`KategorijaDogadjaja/`, `Dogadjaj/`, `Broker/`, `Konsignator/`, `Karta/`, `Listing/`). Namespace je za sve `SistemskeOperacije.Tests`. Postojeće tri klase premesti u odgovarajuće foldere bez izmene sadržaja (`PrijaviBrokerSOTest` ide u `Broker/`).
2. **Oblik klase:**
   ```csharp
   [TestClass]
   public class UbaciDogadjajSOTest : IntegracioniTest
   {
       private UbaciDogadjajSO so = null!;
       private Dogadjaj dogadjaj = null!;

       [TestInitialize]
       public void SetUp()
       {
           dogadjaj = TestPodaci.NapraviDogadjaj();
       }

       [TestMethod]
       public void TestPredusloviNull() { ... }
   }
   ```
   - polja su instance, nikad `static`;
   - podaci se prave u `[TestInitialize]` metodi `SetUp()` ili u samom testu;
   - nema `[ClassInitialize]`, `[ClassCleanup]` ni `[TestCleanup]`, jer bazu vraća `ResetujBazu`.
3. **Imena metoda:**
   - `TestPreduslovi<Opis>` za slučajeve koje odbija `Validate()`;
   - `TestIzvrsiOperaciju<Opis>` za ishod izvršenja (šta je upisano, pročitano, obrisano).
4. **Provera izuzetaka:**
   - `null` objekat i prazan kriterijum pretrage: `Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());`, bez provere poruke;
   - poslovno pravilo: `Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());` pa `Assert.AreEqual("<poruka>", ex.Message);`.
   - Poruke se prepisuju **doslovno iz koda SO**, uključujući nedoslednosti (npr. `"Nije prosledjen id kategorije događaja."` bez „đ“). Poruke navedene u ovom dokumentu su prepisane iz koda u trenutku pisanja; ako se razlikuju, važi kod.
5. **Provera rezultata.** Uspeh se uvek proverava ponovnim čitanjem: kroz `Pretrazi<X>SO` ili kroz `TestBaza.BrojRedova/Postoji`. Nije dovoljno da `ExecuteTemplate()` ne baci izuzetak.
6. **Odbijen upis ne menja bazu.** Kod provera jedinstvenosti i blokiranog brisanja, posle izuzetka proveri i da se broj redova nije promenio, odnosno da zapis i dalje postoji.
7. **`[DataRow]`** za varijante istog pravila (neispravan Id `0` i `-1`, tipovi splita, statusi).
8. **Datumi su relativni** u odnosu na `DateTime.Today` (npr. `DateTime.Today.AddMonths(1)`), nikad fiksni.
9. **Produkcioni kod se ne menja.** Menja se samo sadržaj projekta `SistemskeOperacije.Tests`.
10. **Poznate greške.** Ako kod ne radi ono što ovaj dokument opisuje kao ispravno ponašanje, test se ne prilagođava kodu. Test se napiše kako je opisano, označi sa `[Ignore("Poznata greška: <kratko>")]` i upiše u `POZNATE-GRESKE.md` (odeljak 5). Slučajevi za koje se to već zna označeni su u tabelama sa **(PG)**.
11. **Postojeći testovi.** `KreirajKartaSOTest`, `ObrisiDogadjajSOTest` i `PrijaviBrokerSOTest` ostaju kakvi jesu; dodaju se samo testovi navedeni kao novi.

---

## 3. TestPodaci – fabrike

Konvencija: `Napravi…` vraća ispravan objekat **bez upisa** u bazu; `Kreiraj…` poziva `Napravi…`, upisuje objekat kroz odgovarajuću SO i vraća ga sa popunjenim Id-em. Jedinstvene vrednosti (korisničko ime, email, naziv) dobijaju sufiks `UniqueSuffix()`.

| Metoda | Status | Opis |
| --- | --- | --- |
| `KreirajBrokera(string password)` | postoji | ne menjati potpis |
| `KreirajBrokera(string password, params int[] idKategorija)` | nova | broker sa specijalizacijama za date kategorije (1–3 iz osnovnih podataka), `DatumSpecijalizacije = default` |
| `NapraviDogadjaj(DateTime? datum = null)` | nova | naziv sa sufiksom, mesto `Beograd`, datum podrazumevano `DateTime.Today.AddMonths(1)` |
| `KreirajDogadjaj(DateTime? datum = null)` | izmena | poziva `NapraviDogadjaj`; postojeći pozivi bez argumenta rade isto kao do sada |
| `NapraviFizickoLice()` | nova | izdvaja pravljenje objekta iz postojeće `KreirajFizickoLice()` |
| `KreirajFizickoLice()` | izmena | poziva `NapraviFizickoLice()`, pa `KreirajKonsignatorSO` |
| `NapraviPravnoLice()` / `KreirajPravnoLice()` | nove | email sa sufiksom, `Pib` 9 nasumičnih cifara, `MaticniBroj` 8 nasumičnih cifara, `NazivFirme` sa sufiksom |
| `NapraviKategoriju()` / `KreirajKategoriju()` | nove | naziv i opis sa sufiksom |
| `NapraviKartu(...)` | postoji | ne menjati potpis |
| `KreirajKartu(dogadjaj, konsignator, sektor = "A", red = "1", sediste = "1")` | nova | `NapraviKartu` + `KreirajKartaSO` |
| `KreirajKarte(dogadjaj, konsignator, int broj)` | nova | `broj` karata u istom sektoru (sufiks), red `1`, sedišta `1..broj` |
| `NapraviListing(broker, konsignator, List<Karta> karte, StatusListinga status = objavljen, TipSplita split = bilo_koja_kolicina, int minKolicina = 1)` | nova | `DatumIsteka = DateTime.Today.AddDays(7)`, `CenaPoKarti = 6000m`, `ProcenatProvizije = 10m`; u `Karte` idu objekti sa `IdKarta` |
| `KreirajListing(...)` | nova | isti parametri; `NapraviListing` + `KreirajListingSO` |

Napomena: `KreirajKarte` i `NapraviListing` treba praviti za događaj iz `KreirajDogadjaj()` (datum za mesec dana), da `DatumIsteka` ne bude posle datuma događaja.

---

## 4. Testovi po SO

Kolona „Očekivano“ sadrži poruku izuzetka (pod navodnicima) ili proveru posle izvršenja. „AE“ znači `Assert.ThrowsExactly<ArgumentException>`.

### 4.1. Nivo C – KategorijaDogadjaja (`KategorijaDogadjaja/`)

**UbaciKategorijaDogadjajaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviBezNaziva | `Naziv = ""` | "Naziv kategorije događaja je obavezan." |
| TestIzvrsiOperacijuUspesno | `NapraviKategoriju()` | `Id > 0`; `Pretrazi` vraća isti naziv i opis |
| TestIzvrsiOperacijuDuplikatNaziva **(PG)** | druga kategorija sa nazivom `Koncert` | izuzetak sa porukom "Kategorija događaja sa unetim nazivom već postoji."; broj redova nepromenjen. SO ne proverava jedinstvenost, pa sada pada `SqlException` na UQ ograničenju |

**PromeniKategorijaDogadjajaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviNeispravanId | `[DataRow(0)] [DataRow(-1)]` | "Nije prosleđen id kategorije događaja." |
| TestIzvrsiOperacijuUspesno | `KreirajKategoriju()`, izmeni opis | `Pretrazi` vraća novi opis |

**PretraziKategorijaDogadjajaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestIzvrsiOperacijuUspesno | `KreirajKategoriju()` | vraća isti Id, naziv i opis |
| TestIzvrsiOperacijuNepostojecaKategorija | `IdKategorijaDogadjaja = 999999` | "Sistem ne može da nađe kategoriju događaja." |

**ObrisiKategorijaDogadjajaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviKategorijaDodeljenaBrokeru | `KreirajBrokera("lozinka123", kategorija.Id)` | "Kategorija događaja je dodeljena bar jednom brokeru i ne može se obrisati."; kategorija i dalje postoji |
| TestIzvrsiOperacijuUspesno | `KreirajKategoriju()` | `TestBaza.Postoji` vraća `false` |

**VratiListuKategorijaDogadjajaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviPrazanKriterijum | `new KategorijaDogadjaja()` | AE |
| TestIzvrsiOperacijuPoNazivu | kriterijum `Naziv = "Sport"` | tačno jedna kategorija, `Sportski događaj` |

**VratiListuSviKategorijaDogadjajaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUspesno | `KreirajKategoriju()` | broj elemenata = `TestBaza.BrojRedova("KategorijaDogadjaja")` (4) |

### 4.2. Nivo B – Dogadjaj (`Dogadjaj/`)

Dogadjaj je pilot za parametrizaciju u Fazi 6.1, pa su pretrage pokrivene po svakom kriterijumu.

**UbaciDogadjajSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviBezNaziva | `Naziv = ""` | "Naziv događaja je obavezan." |
| TestPredusloviDatumUProslosti | `DatumOdrzavanja = Today.AddDays(-1)` | "Datum održavanja ne može biti u prošlosti." |
| TestPredusloviDanasnjiDatum | `DatumOdrzavanja = Today` | prolazi |
| TestIzvrsiOperacijuUspesno | `NapraviDogadjaj()` | `Id > 0`; `Pretrazi` vraća isti naziv, mesto i datum |
| TestIzvrsiOperacijuSrpskaSlova | `Naziv = "Đurđevdanski koncert Čačak"`, `Mesto = "Ćuprija"` | `Pretrazi` vraća doslovno isti naziv i mesto |

**PromeniDogadjajSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviNeispravanId | `[DataRow(0)] [DataRow(-1)]` | "Nije prosleđen id događaja." |
| TestPredusloviDatumUProsliostiDozvoljen | postojeći događaj, `DatumOdrzavanja = Today.AddDays(-10)` | prolazi (pravilo o prošlosti važi samo za Ubaci) |
| TestIzvrsiOperacijuUspesno | izmeni naziv i mesto | `Pretrazi` vraća nove vrednosti |

**PretraziDogadjajSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestIzvrsiOperacijuUspesno | `KreirajDogadjaj()` | isti Id, naziv, mesto i datum |
| TestIzvrsiOperacijuNepostojeciDogadjaj | `IdDogadjaj = 999999` | "Sistem ne može da nađe događaj." |

**ObrisiDogadjajSOTest** – postoji i kompletan je; ne dodaje se ništa.

**VratiListuDogadjajSOTest**

Postavka za sve testove osim prvog: tri događaja: `Exit festival` / `Novi Sad` / za 30 dana, `Exit konferencija` / `Beograd` / za 40 dana, `Derbi` / `Beograd` / za 30 dana.

| Test | Kriterijum | Očekivano |
| --- | --- | --- |
| TestPredusloviPrazanKriterijum | `new Dogadjaj()` | AE |
| TestIzvrsiOperacijuPoNazivu | `Naziv = "Exit"` | 2 događaja |
| TestIzvrsiOperacijuPoMestu | `Mesto = "Beograd"` | 2 događaja |
| TestIzvrsiOperacijuPoDatumu | `DatumOdrzavanja = Today.AddDays(30)` | 2 događaja (`Exit festival`, `Derbi`) |
| TestIzvrsiOperacijuKombinovaniKriterijumi | `Naziv = "Exit"`, `Mesto = "Beograd"` | 1 događaj (`Exit konferencija`) |

**VratiListuSviDogadjajSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUspesno | dva `KreirajDogadjaj()` | 2 elementa |

### 4.3. Nivo B – Broker (`Broker/`)

**KreirajBrokerSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviBezSifre | `Sifra = ""` | "Šifra brokera je obavezna." |
| TestPredusloviDuplikatKorisnickogImena | `KorisnickoIme = "vtonic"` | "Broker sa unetim korisničkim imenom već postoji."; broj redova u `Broker` nepromenjen |
| TestIzvrsiOperacijuUspesno | broker bez specijalizacija | `Id > 0`; `Pretrazi` vraća iste podatke, prazne specijalizacije |
| TestIzvrsiOperacijuSaSpecijalizacijama | specijalizacije za kategorije 1 i 2, datum `default` | 2 reda u `BrKd`; oba sa `datumSpecijalizacije = Today` |

**PromeniBrokerSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviNeispravanId | `[DataRow(0)] [DataRow(-1)]` | "Nije prosleđen id brokera." |
| TestPredusloviDuplikatKorisnickogImena | drugi broker dobija `KorisnickoIme = "vtonic"` | "Broker sa unetim korisničkim imenom već postoji." |
| TestPredusloviIstoKorisnickoImeSopstvenogZapisa | izmena imena, korisničko ime nepromenjeno | prolazi |
| TestIzvrsiOperacijuZamenaSpecijalizacija | broker sa kategorijama 1 i 2; šalje se samo 2 i 3 | u `BrKd` tačno kategorije 2 i 3 |
| TestIzvrsiOperacijuZadrzavaDatumSpecijalizacije | broker sa kategorijom 1; `TestBaza.Execute` postavi datum na `Today.AddDays(-30)`; objekat iz `Pretrazi` se pošalje u `Promeni` | datum specijalizacije i dalje `Today.AddDays(-30)` |
| TestIzvrsiOperacijuSifraIzPretrage | `Pretrazi`, izmeni ime, `Promeni` | prijava sa starom šifrom (`PrijaviBrokerSO`) uspeva |
| TestIzvrsiOperacijuBezNoveSifre **(PG)** | `Pretrazi`, `Sifra = null`, izmeni ime, `Promeni` | prijava sa starom šifrom uspeva. `UpdateValues` uvek upisuje `sifra`, pa se šifra sada briše; rešava se u Fazi 3 |

**PretraziBrokerSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestIzvrsiOperacijuUspesnoSaSpecijalizacijama | broker sa kategorijama 1 i 3 | vraća 2 specijalizacije sa nazivima kategorija |
| TestIzvrsiOperacijuNepostojeciBroker | `IdBroker = 999999` | "Sistem ne može da nađe brokera." |

**ObrisiBrokerSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviBrokerSaListingom | broker sa kreiranim listingom | "Broker ima kreirane listinge i ne može se obrisati."; broker i dalje postoji |
| TestIzvrsiOperacijuUspesno | broker sa kategorijama 1 i 2 | nema reda u `Broker` ni u `BrKd` za taj Id |

**VratiListuBrokerSOTest**

| Test | Kriterijum | Očekivano |
| --- | --- | --- |
| TestPredusloviPrazanKriterijum | `new Broker()` | AE |
| TestIzvrsiOperacijuPoImenu | dva nova brokera, jedan sa `Ime = "Marko"`; kriterijum `Ime = "Mark"` | 1 broker |
| TestIzvrsiOperacijuPoKategoriji | broker sa kategorijama 1 i 2; kriterijum: jedna specijalizacija sa kategorijom 2 | broker se vraća sa **obe** specijalizacije |
| TestIzvrsiOperacijuBezDuplikata | isti broker kao gore; kriterijum `KorisnickoIme` = njegovo | tačno 1 element, iako JOIN daje 2 reda |

Napomena: kriterijum ne sme da ima popunjenu `Sifra`, jer tada `SearchCondition` prelazi u granu za prijavu.

**VratiListuSviBrokerSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUspesno | dva nova brokera (jedan sa 2 specijalizacije) | 3 elementa (sa osnovnim brokerom) |

**PrijaviBrokerSOTest** – postoji. U Fazi 1 dodata su dva testa za SQL injekciju (odeljak 4.8); testovi sa apostrofom i srpskim slovima u šifri su u `BrokerPosebniZnaciTest`.

### 4.4. Nivo B – Konsignator (`Konsignator/`)

**KreirajKonsignatorSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviBazniBezPodtipa | `new Konsignator { … }` | "Konsignator mora biti fizičko ili pravno lice." |
| TestPredusloviNeispravanEmail | `Email = "test.rs"` | "E-mail konsignatora nije u odgovarajućem formatu." |
| TestPredusloviDuplikatJmbg | drugo fizičko lice sa istim JMBG-om | "Konsignator sa unetim JMBG-om već postoji."; broj redova nepromenjen |
| TestPredusloviDuplikatPib | drugo pravno lice sa istim PIB-om | "Konsignator sa unetim PIB-om već postoji." |
| TestPredusloviDuplikatEmail **(PG)** | drugo fizičko lice sa istim email-om | izuzetak sa porukom "Konsignator sa unetim e-mailom već postoji."; broj redova nepromenjen. SO ne proverava, pa sada pada `SqlException` na UQ ograničenju |
| TestIzvrsiOperacijuFizickoLice | `NapraviFizickoLice()` | red u `Konsignator` i u `FizickoLice`; `DatumRegistracije = Today` |
| TestIzvrsiOperacijuPravnoLice | `NapraviPravnoLice()` | red u `Konsignator` i u `PravnoLice` |
| TestIzvrsiOperacijuRollbackPriGresciPodtipa | fizičko lice sa `Jmbg` od 14 cifara (kolona je `nvarchar(13)`) | `Assert.ThrowsExactly<SqlException>` (greška mora doći iz baze pri upisu podtipa, posle uspešnog upisa u `Konsignator`); `TestBaza.BrojRedova("Konsignator", "email = '<email>'") == 0`. Ako validacija iz Faze 2 odbije ovaj slučaj ranije, testu treba drugi okidač, a ne slabija provera |

**PromeniKonsignatorSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviNeispravanId | fizičko lice sa `[DataRow(0)] [DataRow(-1)]` | "Nije prosleđen id konsignatora." |
| TestPredusloviPromenaTipa | postojeće fizičko lice; šalje se `PravnoLice` sa istim Id-em | "Tip konsignatora se ne može promeniti." |
| TestPredusloviDuplikatJmbg | dva fizička lica; drugom se postavi JMBG prvog | "Konsignator sa unetim JMBG-om već postoji." |
| TestPredusloviIstiJmbgSopstvenogZapisa | izmena imena, JMBG nepromenjen | prolazi |
| TestIzvrsiOperacijuFizickoLice | izmena imena i telefona | `Pretrazi` vraća nove vrednosti iz obe tabele |
| TestIzvrsiOperacijuPravnoLice | izmena naziva firme i adrese | `Pretrazi` vraća nove vrednosti iz obe tabele |

**PretraziKonsignatorSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestIzvrsiOperacijuVracaFizickoLice | `KreirajFizickoLice()`; kriterijum `new Konsignator { IdKonsignator = … }` | rezultat je `FizickoLice` sa JMBG-om, imenom i prezimenom |
| TestIzvrsiOperacijuVracaPravnoLice | `KreirajPravnoLice()` | rezultat je `PravnoLice` sa PIB-om i nazivom |
| TestIzvrsiOperacijuNepostojeciKonsignator | `IdKonsignator = 999999` | "Sistem ne može da nađe konsignatora." |

**ObrisiKonsignatorSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviBazniBezPodtipa | `new Konsignator { IdKonsignator = … }` | "Konsignator mora biti fizičko ili pravno lice." |
| TestPredusloviKonsignatorSaKartama | fizičko lice sa jednom kartom | "Konsignator ima unete karte i ne može se obrisati."; obe tabele i dalje imaju red |
| TestIzvrsiOperacijuFizickoLice | `KreirajFizickoLice()` | nema reda ni u `FizickoLice` ni u `Konsignator` |
| TestIzvrsiOperacijuPravnoLice | `KreirajPravnoLice()` | nema reda ni u `PravnoLice` ni u `Konsignator` |

Napomena: provera „Konsignator ima kreirane listinge…“ se ne testira, jer je nedostižna. Listing mora imati bar jednu kartu istog konsignatora, a karta se ne može obrisati, pa uvek prvo okine provera karata. Upisati u `POZNATE-GRESKE.md` kao zapažanje (bez testa).

**VratiListuKonsignatorSOTest**

Postavka: fizičko lice `Ime = "Petar"`, `Prezime = "Jovanović"` i pravno lice `NazivFirme = "Jovanović DOO"`.

| Test | Kriterijum | Očekivano |
| --- | --- | --- |
| TestPredusloviPrazanKriterijum | `new Konsignator()` | AE |
| TestIzvrsiOperacijuPoTipu | `TipKriterijum = TipKonsignatora.fizicko_lice` | 1 element, tipa `FizickoLice` |
| TestIzvrsiOperacijuPoNazivu | `NazivKriterijum = "Jovanović"` | 2 elementa, oba podtipa |
| TestIzvrsiOperacijuPoPoljuPodtipa | `new FizickoLice { Ime = "Pet" }` | 1 element |

**VratiListuSviKonsignatorSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUspesno | jedno fizičko i jedno pravno lice | 2 elementa, svaki odgovarajućeg podtipa |

### 4.5. Nivo A – Karta (`Karta/`)

**KreirajKartaSOTest** – postoji; dodaje se jedan test:

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuCenaSaDecimalama | `NominalnaCena = 12.50m` (kultura je `sr-Latn-RS`) | `Pretrazi` vraća `12.50m` |

**PromeniKartaSOTest**

Postavka: događaj, fizičko lice i `KreirajKartu(…, "A", "1", "1")`; objekat za izmenu je rezultat `PretraziKartaSO`.

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviNeispravanId | `[DataRow(0)] [DataRow(-1)]` | "Nije prosleđen id karte." |
| TestPredusloviBezStatusa | `Status = null` | "Status karte je obavezan." |
| TestPredusloviNepostojecaKarta | `IdKarta = 999999`, ostala polja ispravna | "Sistem ne može da nađe kartu." |
| TestPredusloviPromenaKonsignatora | drugi konsignator | "Konsignator karte se ne može promeniti." |
| TestPredusloviPromenaDogadjaja | drugi događaj | "Događaj karte se ne može promeniti." |
| TestPredusloviZauzetoSediste | druga karta `A/1/2`; menja se na `A/1/1` | "Karta za to sedište na izabranom događaju već postoji." |
| TestPredusloviIstoSedisteSopstvenogZapisa | izmena cene, sedište nepromenjeno | prolazi |
| TestIzvrsiOperacijuUspesno | izmena sektora i cene | `Pretrazi` vraća nove vrednosti |
| TestIzvrsiOperacijuZadrzavaListing | karta je na listingu; u izmeni se pošalje `Listing = null` | `Pretrazi` i dalje vraća `Listing.IdListing` tog listinga |

**PretraziKartaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestIzvrsiOperacijuUspesno | slobodna karta | popunjeni `Dogadjaj` (naziv) i `Konsignator` (tipa `FizickoLice`); `Listing == null` |
| TestIzvrsiOperacijuKartaNaListingu | karta na listingu | `Listing.IdListing` = Id listinga |
| TestIzvrsiOperacijuNepostojecaKarta | `IdKarta = 999999` | "Sistem ne može da nađe kartu." |

**VratiListuKartaSOTest**

Postavka: događaj D1 (`Mesto = "Novi Sad"`) i D2 (`Mesto = "Beograd"`); fizičko lice FL (`Prezime = "Jovanović"`) i pravno lice PL; karte:

- K1: D1, FL, sektor `Parter`, tip `stajaca`, na listingu;
- K2: D1, FL, sektor `Tribina`, tip `sedeca`, slobodna;
- K3: D2, PL, sektor `Parter`, tip `vip`, slobodna.

| Test | Kriterijum | Očekivano |
| --- | --- | --- |
| TestPredusloviPrazanKriterijum | `new Karta()` | AE |
| TestIzvrsiOperacijuPoSektoru | `Sektor = "Part"` | K1, K3 |
| TestIzvrsiOperacijuPoTipu | `Tip = TipKarte.vip` | K3 |
| TestIzvrsiOperacijuPoStatusu | `Status = StatusKarte.plasirana` | K1 |
| TestIzvrsiOperacijuPoKonsignatoru | `Konsignator = new Konsignator { IdKonsignator = PL.Id }` | K3 |
| TestIzvrsiOperacijuPoNazivuKonsignatora | `Konsignator = new Konsignator { NazivKriterijum = "Jovan" }` | K1, K2 |
| TestIzvrsiOperacijuPoMestuDogadjaja | `Dogadjaj = new Dogadjaj { Mesto = "Novi" }` | K1, K2 |
| TestIzvrsiOperacijuSamoSlobodne | `SamoSlobodne = true` | K2, K3 |

**VratiListuSviKartaSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUspesno | tri karte | 3 elementa |

### 4.6. Nivo A – Listing (`Listing/`)

Zajednička postavka (ako test ne kaže drugačije): broker iz `KreirajBrokera`, događaj iz `KreirajDogadjaj()`, fizičko lice i `KreirajKarte(dogadjaj, konsignator, 2)`.

**KreirajListingSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviBezKarata | `Karte` prazna lista | "Listing mora imati bar jednu kartu." |
| TestPredusloviNedozvoljenStatus | `[DataRow(StatusListinga.povucen)] [DataRow(StatusListinga.realizovan)]` | "Novi listing može biti samo nacrt ili objavljen." |
| TestPredusloviNeispravnaProvizija | `[DataRow(-1.0)] [DataRow(101.0)]` | "Procenat provizije listinga mora biti između 0 i 100." |
| TestPredusloviDatumIstekaDanas | `DatumIsteka = Today` | "Datum isteka listinga mora biti posle datuma objave." |
| TestPredusloviDatumIstekaPosleDogadjaja | `DatumIsteka` = datum događaja + 1 dan | "Datum isteka listinga ne može biti posle datuma održavanja događaja." |
| TestPredusloviNepostojecaKarta | u `Karte` karta sa `IdKarta = 999999` | "Sistem ne može da nađe karte listinga." |
| TestPredusloviKartaDrugogKonsignatora | jedna karta drugog konsignatora | "Sve karte listinga moraju pripadati istom konsignatoru." |
| TestPredusloviKarteRazlicitihDogadjaja | jedna karta za drugi događaj | "Sve karte listinga moraju biti za isti događaj." |
| TestPredusloviKartaNaDrugomListingu | karta je već na drugom listingu | "Karta se već nalazi na drugom listingu." |
| TestPredusloviMinKolicinaNula | `MinKolicina = 0` | "Minimalna količina listinga mora biti veća od nule." |
| TestPredusloviMinKolicinaVecaOdBrojaKarata | 2 karte, `MinKolicina = 3` | "Minimalna količina ne može biti veća od broja karata na listingu." |
| TestPredusloviPraviloSplita | `[DataRow]` po redu iz tabele ispod | poruka iz tabele |
| TestPredusloviIspravanSplit | `parne_kolicine`, 2 karte, `MinKolicina = 2` | prolazi |
| TestIzvrsiOperacijuUspesno | `[DataRow(StatusListinga.nacrt)] [DataRow(StatusListinga.objavljen)]` | `Id > 0`; `DatumObjave.Date == Today`; obe karte imaju `idListing` tog listinga i status `plasirana` (`TestBaza` ili `PretraziKartaSO`) |

Redovi za `TestPredusloviPraviloSplita` (split, broj karata, `MinKolicina`, poruka):

| Split | Karata | MinKolicina | Poruka |
| --- | --- | --- | --- |
| `bez_splita` | 2 | 1 | "Kod prodaje bez deljenja minimalna količina mora biti jednaka broju karata." |
| `bilo_koja_kolicina` | 2 | 2 | "Kod prodaje u bilo kojoj količini minimalna količina mora biti 1." |
| `parne_kolicine` | 3 | 2 | "Kod prodaje u parnim količinama broj karata i minimalna količina moraju biti parni." |
| `min_kolicina` | 2 | 1 | "Kod prodaje sa minimalnom količinom ta količina mora biti veća od 1." |
| `izbegni_usamljenu` | 1 | 1 | "Kod izbegavanja usamljene karte listing mora imati bar dve karte, a minimalna količina mora biti 1." |

**PromeniListingSOTest**

Postavka: listing sa kartama A i B (`KreirajKarte(…, 3)`, treća karta C ostaje slobodna); objekat za izmenu je rezultat `PretraziListingSO`.

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestPredusloviNeispravanId | `[DataRow(0)] [DataRow(-1)]` | "Nije prosleđen id listinga." |
| TestPredusloviNepostojeciListing | `IdListing = 999999`, ostala polja ispravna | "Sistem ne može da nađe listing." |
| TestPredusloviPromenaBrokera | drugi broker | "Broker listinga se ne može promeniti." |
| TestPredusloviPromenaKonsignatora | drugi konsignator | "Konsignator listinga se ne može promeniti." |
| TestPredusloviKartaSaDrugogListinga | drugi listing sa kartom D; u izmenu prvog se doda D | "Karta se već nalazi na drugom listingu." |
| TestPredusloviDatumIstekaPreObjave | listing `objavljen`; `TestBaza.Execute` postavi `datumObjave` na `Today.AddDays(-3)`; status ostaje `objavljen`, `DatumIsteka = Today.AddDays(-3)` | "Datum isteka listinga mora biti posle datuma objave." |
| TestIzvrsiOperacijuOslobadjaIzbaceneKarte | šalje se samo karta A | B: `idListing` NULL, status `u_inventaru`; A i dalje na listingu |
| TestIzvrsiOperacijuDodajeNoveKarte | šalju se A, B i C | C ima `idListing` listinga i status `plasirana` |
| TestIzvrsiOperacijuStatusKarata | `[DataRow(StatusListinga.realizovan, StatusKarte.prodata)] [DataRow(StatusListinga.povucen, StatusKarte.povucena)] [DataRow(StatusListinga.objavljen, StatusKarte.plasirana)]` | obe karte imaju dati status |
| TestIzvrsiOperacijuDatumObjavePriObjavljivanju | listing `nacrt`; `TestBaza.Execute` postavi `datumObjave` na `Today.AddDays(-3)`; menja se na `objavljen` | `DatumObjave.Date == Today` |
| TestIzvrsiOperacijuDatumObjaveBezObjavljivanja | listing `objavljen`; `datumObjave` postavljen na `Today.AddDays(-3)`; menja se samo cena | `DatumObjave.Date == Today.AddDays(-3)` |
| TestIzvrsiOperacijuUspesno | izmena cene i napomene | `Pretrazi` vraća nove vrednosti |

**PretraziListingSOTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestPredusloviNull | `null` | AE |
| TestIzvrsiOperacijuUspesno | listing sa 2 karte | `Karte.Count == 2`; popunjen `Broker` i `Konsignator` (tipa `FizickoLice`); karte imaju `Dogadjaj` |
| TestIzvrsiOperacijuNepostojeciListing | `IdListing = 999999` | "Sistem ne može da nađe listing." |

**VratiListuListingSOTest**

Postavka: događaji D1 i D2; listinzi:

- L1: D1, 3 karte (sektori `Parter`, `Parter`, `Tribina`), status `objavljen`, cena 5000;
- L2: D2, 1 karta (sektor `Tribina`), status `nacrt`, cena 9000.

| Test | Kriterijum | Očekivano |
| --- | --- | --- |
| TestPredusloviPrazanKriterijum | `new Listing()` | AE |
| TestIzvrsiOperacijuPoStatusu | `Status = objavljen` | L1, sa `Karte.Count == 3` (listing se vraća jednom, iako JOIN daje 3 reda) |
| TestIzvrsiOperacijuPoRasponuCene | `CenaOd = 6000`, `CenaDo = 10000` | L2 |
| TestIzvrsiOperacijuPoRasponuDatumaObjave | L2-u se `TestBaza.Execute` postavi `datumObjave` na `Today.AddDays(-10)`; `DatumObjaveOd = Today` | L1 |
| TestIzvrsiOperacijuPoKonsignatoru | L2 pripada drugom konsignatoru; `Konsignator = new Konsignator { IdKonsignator = … }` | L2 |
| TestIzvrsiOperacijuPoSektoruKarte | `KriterijumKarta = new Karta { Sektor = "Parter" }` | L1, i dalje sa **sve 3** karte (podupit ne odseca ostale karte) |
| TestIzvrsiOperacijuPoDogadjaju | `KriterijumDogadjaj = new Dogadjaj { IdDogadjaj = D2.Id }` | L2 |

### 4.7. Serijalizacija (opciono)

Klasa `JsonNetworkSerializerTest` u folderu `Komunikacija/`. Ne nasleđuje `IntegracioniTest`, jer ne radi sa bazom. Pre pisanja pročitaj API klase `Common/Komunikacija/JsonNetworkSerializer.cs`.

| Test | Očekivano |
| --- | --- |
| FizickoLice poslato kao `Konsignator` | posle deserijalizacije `ReadType<Konsignator>` je tipa `FizickoLice` sa svim poljima |
| PravnoLice poslato kao `Konsignator` | tipa `PravnoLice` |
| `List<Karta>` sa ugnježdenim `Dogadjaj` i `Konsignator` | isti broj elemenata i vrednosti; konsignator zadržava podtip |
| `Broker` sa specijalizacijama | specijalizacije sa kategorijama i datumima |

### 4.8. Faza 1 – apostrof, srpska slova i SQL injekcija (`PosebniZnaci/`)

Testovi proveravaju da tekst sa posebnim znacima prolazi kroz upis, izmenu i pretragu bez ikakve promene. Pokrivaju sva mesta iz `INVENTAR-SQL-LITERALA.md` (koren rešenja) koja primaju slobodan tekst.

Odstupanje od pravila 1 iz odeljka 2: testovi su grupisani po **entitetu**, a ne po SO, jer svaki test prolazi kroz više SO (upis, pa `Pretrazi…SO`). Klase su `<Entitet>PosebniZnaciTest`, u folderu `PosebniZnaci/`. Izuzetak su testovi injekcije u `PrijaviBrokerSOTest`.

**Konstante u `TestPodaci`**

| Konstanta | Vrednost | Šta proverava |
| --- | --- | --- |
| `Apostrof` | `O'Brien` | udvajanje apostrofa |
| `SrpskaSlova` | `Đurđevdanski koncert Čačak` | ć, č, đ (Ć, Č, Đ); traži prefiks `N` |
| `Kombinovano` | `Ćirić O'Đorđević` | apostrof i srpska slova zajedno |
| `InjekcijaUslov` | `' OR '1'='1` | izlazak iz literala u uslovu `WHERE` |
| `InjekcijaNaredba` | `x'; DROP TABLE Karta; --` | izlazak iz literala i nova naredba |
| `EmailSaApostrofom` | `o'brien@test.rs` | e-mail sa apostrofom (prolazi `CK_Konsignator_email`) |

Pomoćne metode: `DeoTeksta(tekst)` vraća tekst bez prvog i poslednjeg znaka (kriterijum za pretragu po delu teksta; posebni znaci ostaju u njemu); `Skrati(tekst, 10)` skraćuje tekst za kolone `red` i `sediste` (`nvarchar(10)`).

**Pravila**

- Svaki test sa `[TestMethod]` i pet `[DataRow]` (sve konstante osim `EmailSaApostrofom`). Atribut `[DataTestMethod]` se ne koristi, jer je u MSTest 4 zastareo (upozorenje MSTEST0044).
- Posebni znaci idu samo u slobodna tekstualna polja. JMBG, PIB, matični broj, broj lične karte i telefon ostaju ispravne cifre, jer ih Faza 2 ograničava na cifre. E-mail je uvek `EmailSaApostrofom`.
- Pročitana vrednost se poredi sa `Assert.AreEqual(tekst, …)` (redni, znak po znak).
- Pretraga uvek ima i kontrolni zapis sa običnim tekstom (iz `TestPodaci`); očekuje se **tačno jedan** rezultat. Tako `InjekcijaUslov` ne može da prođe ako uslov vrati sve redove.
- Redovi `SrpskaSlova` i `Kombinovano` su padali dok svi tekstualni literali nisu dobili prefiks `N` (Faza 1, korak 2); nikad nisu bili označeni sa `[Ignore]`.

**KategorijaDogadjajaPosebniZnaciTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUpis | `Ubaci` sa `Naziv = Opis = tekst` | `Pretrazi` vraća isti `Naziv` i `Opis` |
| TestIzvrsiOperacijuIzmena | `KreirajKategoriju()`, `Naziv = Opis = tekst`, `Promeni` | `Pretrazi` vraća isti `Naziv` i `Opis` |
| TestIzvrsiOperacijuPretragaPoNazivu | kontrolna kategorija + kategorija sa `Naziv = tekst`; kriterijum `Naziv = DeoTeksta(tekst)` | 1 rezultat, isti Id i `Naziv` |
| TestIzvrsiOperacijuPretragaPoOpisu | isto, po `Opis` | 1 rezultat, isti Id i `Opis` |

**DogadjajPosebniZnaciTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUpis | `Ubaci` sa `Naziv = Mesto = tekst` | `Pretrazi` vraća isti `Naziv` i `Mesto` |
| TestIzvrsiOperacijuIzmena | `KreirajDogadjaj()`, `Naziv = Mesto = tekst`, `Promeni` | `Pretrazi` vraća isti `Naziv` i `Mesto` |
| TestIzvrsiOperacijuPretragaPoNazivu | kontrolni događaj + događaj sa `Naziv = tekst`; kriterijum `Naziv = DeoTeksta(tekst)` | 1 rezultat, isti Id i `Naziv` |
| TestIzvrsiOperacijuPretragaPoMestu | isto, po `Mesto` | 1 rezultat, isti Id i `Mesto` |
| TestIzvrsiOperacijuPretragaDzokerZnak **(PG)** | "Popust 100% rasprodato" i "Koncert bez popusta"; kriterijum `Naziv = "%"` | samo prvi događaj. `[Ignore("Ograničenje do faze 6.1: džoker znaci u LIKE")]`; sada vraća oba |

**BrokerPosebniZnaciTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUpis | `Kreiraj` sa `KorisnickoIme = Sifra = Ime = Prezime = tekst` | `Pretrazi` vraća ista četiri polja |
| TestIzvrsiOperacijuIzmena | `KreirajBrokera`, sva četiri polja `= tekst` (i nova šifra, zbog PG iz 4.3), `Promeni` | `Pretrazi` vraća ista četiri polja |
| TestIzvrsiOperacijuPretragaPoKorisnickomImenu | kontrolni broker + broker sa tekstom; kriterijum `KorisnickoIme = DeoTeksta(tekst)` (bez šifre, pa `LIKE`) | 1 rezultat (ni `vtonic` iz `ResetujBazu`), isti Id i `KorisnickoIme` |
| TestIzvrsiOperacijuPretragaPoImenuIPrezimenu | isto; kriterijum `Ime = Prezime = DeoTeksta(tekst)` | 1 rezultat, isti `Ime` i `Prezime` |
| TestIzvrsiOperacijuPrijavaSifraSaPosebnimZnacima | `KreirajBrokera(tekst)` (šifra = tekst), `PrijaviBrokerSO` | prijava uspeva; vraćen isti Id i `Sifra == tekst` |
| TestIzvrsiOperacijuPrijavaSifraBezApostrofa | šifra `lo'zinka`, prijava sa `lozinka` | "Korisničko ime ili šifra nisu ispravni." |
| TestPredusloviKreirajPostojeceKorisnickoImeSaApostrofom | broker `o'brien`, pa drugi `o'brien` | "Broker sa unetim korisničkim imenom već postoji."; broj redova nepromenjen |
| TestPredusloviKreirajKorisnickoImeBezApostrofa | broker `o'brien`, pa `obrien` | drugi se upisuje |
| TestPredusloviPromeniNaPostojeceKorisnickoImeSaApostrofom | broker `o'brien`; drugi broker menja korisničko ime u `o'brien` | "Broker sa unetim korisničkim imenom već postoji."; staro korisničko ime ostaje |

**PrijaviBrokerSOTest** (dodato)

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuSqlInjectionKorisnickoIme | `[DataRow("' OR 1=1 --")] [DataRow("' OR '1'='1")]`; ista vrednost i za šifru | "Korisničko ime ili šifra nisu ispravni."; `Result == null` |
| TestIzvrsiOperacijuSqlInjectionSifra | isti `DataRow`-ovi kao šifra, uz postojeće korisničko ime | "Korisničko ime ili šifra nisu ispravni."; `Result == null` |

Postojeći `TestIzvrsiOperacijuSqlInjection` (`' OR 1=1 --`, šifra "bilo sta") ostaje nepromenjen.

**FizickoLicePosebniZnaciTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUpis | `Kreiraj` sa `Ime = Prezime = Adresa = tekst`, `Email = EmailSaApostrofom` | `Pretrazi` vraća ista četiri polja |
| TestIzvrsiOperacijuIzmena | `KreirajFizickoLice()`, ista četiri polja, `Promeni` | `Pretrazi` vraća ista četiri polja |
| TestIzvrsiOperacijuPretragaPoImenuIPrezimenu | kontrolno lice + lice sa tekstom; kriterijum `new FizickoLice { Ime = Prezime = DeoTeksta(tekst) }` (`AdditionalSearchCondition`) | 1 rezultat, isti `Ime` i `Prezime` |
| TestIzvrsiOperacijuPretragaPoAdresi | kriterijum `new Konsignator { Adresa = DeoTeksta(tekst) }` | 1 rezultat, ista `Adresa` |
| TestIzvrsiOperacijuPretragaPoNazivKriterijumu | kontrolno fizičko i pravno lice; kriterijum `NazivKriterijum = DeoTeksta(tekst)` | 1 rezultat; `Name == "tekst tekst"` |
| TestIzvrsiOperacijuPretragaPoEmailuSaApostrofom | lice sa `EmailSaApostrofom`; kriterijum `Email = "o'brien"` (bez `DataRow`) | 1 rezultat, isti `Email` |

**PravnoLicePosebniZnaciTest**

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUpis | `Kreiraj` sa `NazivFirme = Adresa = tekst`, `Email = EmailSaApostrofom` | `Pretrazi` vraća ista tri polja |
| TestIzvrsiOperacijuIzmena | `KreirajPravnoLice()`, ista tri polja, `Promeni` | `Pretrazi` vraća ista tri polja |
| TestIzvrsiOperacijuPretragaPoNazivuFirme | kriterijum `new PravnoLice { NazivFirme = DeoTeksta(tekst) }` | 1 rezultat, isti `NazivFirme` |
| TestIzvrsiOperacijuPretragaPoAdresi | kriterijum `new Konsignator { Adresa = DeoTeksta(tekst) }` | 1 rezultat, ista `Adresa` |
| TestIzvrsiOperacijuPretragaPoNazivKriterijumu | kontrolno fizičko i pravno lice; `NazivKriterijum = DeoTeksta(tekst)` | 1 rezultat; `Name == tekst` |

**KartaPosebniZnaciTest**

Postavka (`SetUp`): jedan događaj i jedno fizičko lice. Karta sa tekstom ima `Sektor = tekst`, `Red = Sediste = Skrati(tekst, 10)`.

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUpis | `Kreiraj` karte sa tekstom | `Pretrazi` vraća isti `Sektor`, `Red`, `Sediste` |
| TestIzvrsiOperacijuIzmena | karta `A/1/1`; `Pretrazi`, polja `= tekst`, `Promeni` | `Pretrazi` vraća ista tri polja |
| TestIzvrsiOperacijuPretragaPoSektoruRedu | kontrolna karta `A/1/1` + karta sa tekstom; kriterijum `DeoTeksta` za sva tri polja | 1 rezultat, ista tri polja |
| TestIzvrsiOperacijuPretragaPoImenuKonsignatora | drugo fizičko lice sa `Ime = Prezime = tekst`; po jedna karta za oba lica; kriterijum `Konsignator.NazivKriterijum = DeoTeksta(tekst)` | 1 rezultat; konsignator karte ima isti `Ime` i `Prezime` |
| TestIzvrsiOperacijuPretragaPoMestuDogadjaja | drugi događaj sa `Mesto = tekst`; po jedna karta za oba; kriterijum `Dogadjaj.Mesto = DeoTeksta(tekst)` | 1 rezultat; događaj karte ima isto `Mesto` |
| TestPredusloviZauzetoSedisteSaPosebnimZnacima | `[DataRow("A'1")] [DataRow("Č")]`; karta `sektor/1/1`, pa još jedna ista | "Karta za to sedište na izabranom događaju već postoji."; broj redova nepromenjen |
| TestPredusloviSlobodnoSedisteSlicanSektor | `[DataRow("A1", "A'1")] [DataRow("C", "Č")]`; karta `postojeci/1/1`, pa `novi/1/1` | druga karta se upisuje, `Sektor == novi` (bez prefiksa `N` bi `'Č'` postalo `'C'`, pa bi SO javila zauzeto sedište) |
| TestPredusloviPromeniNaZauzetoSedisteSaPosebnimZnacima | `[DataRow("A'1")] [DataRow("Č")]`; karta `sektor/1/1` i karta `B/1/1`; druga se menja u `sektor/1/1` | "Karta za to sedište na izabranom događaju već postoji."; sektor druge karte ostaje `B` |

**ListingPosebniZnaciTest**

Postavka (`SetUp`): broker, događaj, fizičko lice. Pretraga uvek pravi i kontrolni listing sa jednom običnom kartom.

| Test | Postavka | Očekivano |
| --- | --- | --- |
| TestIzvrsiOperacijuUpis | listing sa `Napomena = tekst`, `Kreiraj` | `Pretrazi` vraća istu `Napomena` |
| TestIzvrsiOperacijuIzmena | `KreirajListing`, `Pretrazi`, `Napomena = tekst`, `Promeni` | `Pretrazi` vraća istu `Napomena` |
| TestIzvrsiOperacijuPretragaPoImenuKonsignatora | drugo fizičko lice sa `Ime = Prezime = tekst` i njegov listing; `Konsignator.NazivKriterijum = DeoTeksta(tekst)` | 1 rezultat; `Konsignator.Name == "tekst tekst"` |
| TestIzvrsiOperacijuPretragaPoSektoruKarte | listing sa kartom `Sektor = tekst`; `KriterijumKarta.Sektor = DeoTeksta(tekst)` (podupit `ka2`) | 1 rezultat; `Karte[0].Sektor == tekst` |
| TestIzvrsiOperacijuPretragaPoMestuDogadjaja | drugi događaj sa `Mesto = tekst` i listing za njega; `KriterijumDogadjaj.Mesto = DeoTeksta(tekst)` (podupit `d2`) | 1 rezultat; `Karte[0].Dogadjaj.Mesto == tekst` |

**Stanje posle Faze 1, korak 1** (grana `improvements`, produkcioni kod nepromenjen): 378 testova, 306 prolazi, 67 pada, 5 preskočeno. Svi pali testovi su redovi `SrpskaSlova` / `Kombinovano` i red `("C", "Č")`.

**Stanje posle Faze 1, korak 2** (prefiks `N` za sve tekstualne literale): 378 testova, 374 prolazi, 0 pada, 4 preskočeno (`TestIzvrsiOperacijuPretragaDzokerZnak` i tri ranije poznate greške). Isti rezultat u tri uzastopna pokretanja.

---

## 5. POZNATE-GRESKE.md

Fajl u korenu projekta `SistemskeOperacije.Tests`. Jedan unos po grešci:

```markdown
## <kratak naslov>

- **SO / klasa:** <npr. UbaciKategorijaDogadjajaSO>
- **Test:** <ime test metode ili „bez testa“>
- **Očekivano:** <šta bi trebalo da se desi>
- **Stvarno:** <šta se dešava>
- **Rešava se u:** <Faza 2 / Faza 3 / Faza 6 / nije planirano>
```

Unosi poznati unapred:

1. `UbaciKategorijaDogadjajaSO` ne proverava jedinstvenost naziva (rešava se: nije planirano, odlučiti).
2. `KreirajKonsignatorSO` ne proverava jedinstvenost email-a (rešava se: nije planirano, odlučiti).
3. `PromeniBrokerSO` / `Broker.UpdateValues` briše šifru kada nova nije uneta (rešava se: Faza 3).
4. Provera listinga u `ObrisiKonsignatorSO` je nedostižna (bez testa; samo zapažanje).
5. Džoker znaci `%`, `_` i `[` u `LIKE` pretrazi nisu izbegnuti (test `DogadjajPosebniZnaciTest.TestIzvrsiOperacijuPretragaDzokerZnak`; rešava se: Faza 6.1).

---

## 6. Redosled rada i kriterijum završetka

1. Dopuni `TestPodaci` (odeljak 3) i premesti postojeće tri klase u foldere.
2. Nivo C: `KategorijaDogadjaja`.
3. Nivo B: `Dogadjaj`, `Broker`, `Konsignator`.
4. Nivo A: `Karta`, `Listing`.
5. Opciono: serijalizacija.
6. Napravi `POZNATE-GRESKE.md`.

Posle svakog entiteta pokreni sve testove (`dotnet test SistemskeOperacije.Tests`). Nastavi tek kada svi testovi tog entiteta prolaze ili su označeni sa `[Ignore]` i upisani u `POZNATE-GRESKE.md`.

Posao je završen kada:

- projekat se build-uje bez grešaka i bez novih upozorenja;
- svi testovi prolaze, osim onih sa `[Ignore]`;
- testovi prolaze i kada se pokrenu tri puta zaredom;
- svaki `[Ignore]` ima unos u `POZNATE-GRESKE.md`.
