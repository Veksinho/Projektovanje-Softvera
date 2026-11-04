# Pravila validacije unosa (Faza 2)

Jedini izvor pravila za Fazu 2. Pravila se implementiraju u `Common/Validacija` i koriste ih i forme (klijent) i `Validate()` u sistemskim operacijama (server). Odluke su potvrđene 3. 11. 2026.

**Princip:** validacija je jednostavna, ali postoji svuda. Polja za brojeve primaju samo cifre (kucanjem i lepljenjem), tekst je ograničen dužinom kolone, a kontrolne cifre (JMBG, PIB, matični broj) se ne proveravaju.

Svako tekstualno polje dobija i proveru dužine, sa porukom oblika „<Polje> <entiteta> može imati najviše N znakova.“. Postojeće poruke o obaveznosti ostaju iste (testovi ih porede doslovno).

## Pravila po polju

| Entitet | Polje | Kolona | Pravilo |
| --- | --- | --- | --- |
| Broker | korisničko ime | nvarchar(50) | obavezno, ≤ 50, bez pravila formata (O1) |
| Broker | šifra | nvarchar(100) | Kreiraj: obavezna, 8–64 znaka, bar jedno slovo i bar jedna cifra; Promeni: ista pravila samo ako je uneta; Prijavi: samo obavezna (O2) |
| Broker | ime, prezime | nvarchar(50) | obavezno, ≤ 50 |
| Broker | telefon | nvarchar(30) | obavezan, opcioni + na prvom mestu, zatim samo cifre, 6–15 cifara (O8) |
| KategorijaDogadjaja | naziv | nvarchar(100) | obavezan, ≤ 100, jedinstven (O6) |
| KategorijaDogadjaja | opis | nvarchar(500) | obavezan, ≤ 500 |
| Dogadjaj | naziv, mesto | nvarchar(150) | obavezno, ≤ 150; datum kao do sada |
| Konsignator | e-mail | nvarchar(100) | obavezan, ≤ 100, ispravan format (apostrof dozvoljen), jedinstven (O6) |
| Konsignator | telefon | nvarchar(30) | obavezan, opcioni + na prvom mestu, zatim samo cifre, 6–15 cifara (O8) |
| Konsignator | adresa | nvarchar(200) | obavezna, ≤ 200 |
| FizickoLice | JMBG | nvarchar(13) | obavezan, tačno 13 cifara, bez kontrolne cifre i provere datuma (O4) |
| FizickoLice | ime, prezime | nvarchar(50) | obavezno, ≤ 50 |
| FizickoLice | broj lične karte | nvarchar(20) | obavezan, tačno 9 cifara |
| PravnoLice | PIB | nvarchar(9) | obavezan, tačno 9 cifara, bez kontrolne cifre (O9) |
| PravnoLice | matični broj | nvarchar(8) | obavezan, tačno 8 cifara, bez kontrolne cifre (O5) |
| PravnoLice | naziv | nvarchar(150) | obavezan, ≤ 150 |
| Karta | sektor | nvarchar(50) | obavezan, ≤ 50; „-“ ostaje dozvoljen |
| Karta | red, sedište | nvarchar(10) | obavezno, ≤ 10; „-“ ostaje dozvoljen |
| Karta | nominalna cena | decimal(10,2) | ≥ 0, najviše 2 decimale, ≤ 99.999.999,99 (O3) |
| Listing | cena po karti | decimal(10,2) | > 0, najviše 2 decimale, ≤ 99.999.999,99 (O3) |
| Listing | procenat provizije | decimal(5,2) | 0–100, najviše 2 decimale |
| Listing | napomena | nvarchar(1000) | opciona, ≤ 1000 |
| Listing | minimalna količina | int | ceo broj, od 1 do broja karata na listingu, uz postojeća pravila po tipu splita; opseg `NumericUpDown` 1–100000 ostaje (O13) |
| Listing | datum isteka | datetime | obavezan; posle datuma objave (Kreiraj: posle današnjeg dana); ne posle datuma održavanja događaja (O13) |
| Pretraga listinga | cena od, cena do | decimal(10,2) | opciono; ista pravila unosa kao za iznose (bez minusa, najviše 2 decimale, ≤ 99.999.999,99); ako su unete obe, od ≤ do; neispravan unos daje poruku umesto da se ignoriše (O13) |
| Pretraga listinga | objavljen od, objavljen do | date | opciono; ako su oba izabrana, od ≤ do (O13) |
| Pretrage (`UCPretraga*`) | sva tekstualna polja | — | samo `MaxLength`; za telefon i filter cifara uz opcioni `+` na prvom mestu, bez provere formata (pretraga po delu broja) (O14); naziv konsignatora ≤ 150 (O11) |

## Detalji pravila

- **`MaxLength`** prati pravilo, a ne kolonu (O12): JMBG 13, broj lične karte 9, PIB 9, matični broj 8, telefon 16 (`+` i 15 cifara), šifra na formi brokera 64. Ostala tekstualna polja imaju `MaxLength` jednak dužini kolone.
- **Trim:** forme uklanjaju razmake na početku i kraju svih tekstualnih polja, uključujući šifru na prijavi i na formi brokera (O10). Provera dužine radi nad trimovanim tekstom.
- **Cifra** je samo `0`–`9` (ne `char.IsDigit`, da ne bi prošle druge Unicode cifre).
- **Telefon:** `+` je dozvoljen samo kao prvi znak; razmak, crtica i kosa crta nisu dozvoljeni. Primeri: `+381641234567` i `0641234567` su ispravni; `064 123 4567`, `064-1234567`, `06+41234567` i `+` nisu.
- **Šifra:** slovo se proverava sa `char.IsLetter` (važe i č, ć, ž, š, đ); velika slova i specijalni znaci nisu obavezni. Primeri: `lozinka1` i `Šifra123` su ispravni; `lozinkaa`, `12345678` i `loz1` nisu. Gornja granica 64 je zbog BCrypt-a u fazi 3 (koristi prva 72 bajta UTF-8). `PrijaviBrokerSO` ne proverava pravila šifre.
- **E-mail:** `MailAddress.TryCreate` i adresa jednaka unetom tekstu; `o'brien@test.rs` mora da prođe.
- **Iznosi:** forma prihvata i `.` i `,` kao decimalni separator i pretvara ih u separator tekuće kulture (`sr-Latn-RS`); znak minus nije dozvoljen.
- **Jedinstvenost** (O6) proverava server, upitom nad bazom: e-mail konsignatora („Konsignator sa unetim e-mailom već postoji.“) i naziv kategorije („Kategorija događaja sa unetim nazivom već postoji.“). Kolacija je CI, pa su „Koncert“ i „koncert“ isti naziv.

## Odluke

| # | Odluka |
| --- | --- |
| O1 | Korisničko ime: samo obavezno i ≤ 50, bez pravila formata do faze 6.1 (testovi posebnih znakova i SQL injection testovi ostaju smisleni) |
| O2 | Šifra: 8–64 znaka, bar jedno slovo i bar jedna cifra; potvrda šifre na formi dolazi u fazi 3 |
| O3 | Nominalna cena ≥ 0 (besplatne karte), cena po karti > 0; najviše 2 decimale i ≤ 99.999.999,99; provizija 0–100 |
| O4 | JMBG: tačno 13 cifara, bez kontrolne cifre i provere datuma |
| O5 | Matični broj: tačno 8 cifara, bez kontrolne cifre |
| O6 | Jedinstvenost e-maila konsignatora i naziva kategorije rešava se u fazi 2 |
| O7 | Jedinični testovi pravila su u novom projektu `Common.Tests` (MSTest, bez baze) |
| O8 | Telefon: opcioni + na prvom mestu, zatim samo cifre (bez razmaka i crtica), 6–15 cifara |
| O9 | PIB: tačno 9 cifara, bez kontrolne cifre |
| O10 | Šifra se trimuje i na prijavi (`FrmLogin`), kao na formi brokera (4. 11. 2026.) |
| O11 | Pretrage ne dobijaju polja za JMBG, PIB, matični broj i broj lične karte; naziv konsignatora u pretragama ima `MaxLength` 150, po najdužoj koloni (`pl.naziv`) (4. 11. 2026.) |
| O12 | `MaxLength` prati gornju granicu pravila, a ne dužinu kolone (4. 11. 2026.) |
| O13 | Pravila za minimalnu količinu i datum isteka listinga beleže provere koje već postoje u `KreirajListingSO` / `PromeniListingSO` i `ListingGuiController`; cena od/do i objavljen od/do u pretrazi listinga dobijaju proveru opsega (4. 11. 2026.) |
| O14 | Telefon u pretrazi prima isti filter kao u formi: opcioni `+` samo na prvom mestu, zatim cifre; dužina se ne proverava, `MaxLength` 16 (4. 11. 2026.) |
| O15 | Sve liste (`ComboBox`) su `DropDownList`, postavljeno u konstruktoru user control-e: `UCKonsignator.cmbTipKonsignatora`; `UCKarta.cmbKonsignator`, `cmbDogadjaj`, `cmbTip`, `cmbFormat`, `cmbStatus`; `UCPretragaBroker.cmbKategorija`; `UCPretragaKonsignator.cmbTipKonsignatora`; `UCPretragaKarta.cmbTip`, `cmbStatus`, `cmbKonsignator`, `cmbDogadjaj` (4. 11. 2026.) |
