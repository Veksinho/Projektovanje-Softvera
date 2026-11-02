# Poznate greške

Spisak mesta gde se kod ponaša drugačije od specifikacije (`SPECIFIKACIJA-TESTOVA.md`). Testovi za ove slučajeve su napisani kako je specificirano i označeni sa `[Ignore("Poznata greška: …")]`.

## Nema provere jedinstvenosti naziva kategorije

- **SO / klasa:** UbaciKategorijaDogadjajaSO
- **Test:** UbaciKategorijaDogadjajaSOTest.TestIzvrsiOperacijuDuplikatNaziva
- **Očekivano:** `Validate()` odbija naziv koji već postoji, sa porukom "Kategorija događaja sa unetim nazivom već postoji."; broj redova u `KategorijaDogadjaja` se ne menja.
- **Stvarno:** SO ne proverava jedinstvenost; upis pada na ograničenju `UQ_KategorijaDogadjaja` sa `SqlException` ("Violation of UNIQUE KEY constraint 'UQ_KategorijaDogadjaja'…"). Broj redova ostaje isti zbog rollback-a.
- **Rešava se u:** nije planirano, odlučiti

## Nema provere jedinstvenosti e-maila konsignatora

- **SO / klasa:** KreirajKonsignatorSO
- **Test:** KreirajKonsignatorSOTest.TestPredusloviDuplikatEmail
- **Očekivano:** `Validate()` odbija e-mail koji već postoji, sa porukom "Konsignator sa unetim e-mailom već postoji."; broj redova u `Konsignator` se ne menja.
- **Stvarno:** SO ne proverava jedinstvenost; upis pada na ograničenju `UQ_Konsignator_email` sa `SqlException` ("Violation of UNIQUE KEY constraint 'UQ_Konsignator_email'…").
- **Rešava se u:** nije planirano, odlučiti

## Izmena brokera bez nove šifre briše postojeću šifru

- **SO / klasa:** PromeniBrokerSO / Broker.UpdateValues
- **Test:** PromeniBrokerSOTest.TestIzvrsiOperacijuBezNoveSifre
- **Očekivano:** kada je `Sifra = null`, postojeća šifra ostaje nepromenjena i prijava sa starom šifrom uspeva.
- **Stvarno:** `UpdateValues` uvek upisuje `sifra = '…'`; `Sql.Tekst(null)` vraća prazan string, pa se šifra briše i `PrijaviBrokerSO` baca "Korisničko ime ili šifra nisu ispravni.".
- **Rešava se u:** Faza 3

## Provera listinga u brisanju konsignatora je nedostižna

- **SO / klasa:** ObrisiKonsignatorSO
- **Test:** bez testa (zapažanje)
- **Očekivano:** —
- **Stvarno:** provera "Konsignator ima kreirane listinge i ne može se obrisati." nikad ne može da okine. Listing mora imati bar jednu kartu istog konsignatora, a karta se ne može obrisati, pa uvek prvo okine provera "Konsignator ima unete karte i ne može se obrisati.".
- **Rešava se u:** nije planirano

## Džoker znaci u LIKE pretrazi nisu izbegnuti

- **SO / klasa:** sve `VratiListu…SO` koje pretražuju po tekstu (`SearchCondition` / `AdditionalSearchCondition` / `UslovNadKartama` svih domenskih klasa)
- **Test:** DogadjajPosebniZnaciTest.TestIzvrsiOperacijuPretragaDzokerZnak (`[Ignore("Ograničenje do faze 6.1: džoker znaci u LIKE")]`)
- **Očekivano:** pretraga događaja po nazivu `"%"` vraća samo događaje čiji naziv sadrži znak `%` ("Popust 100% rasprodato"), a ne i "Koncert bez popusta".
- **Stvarno:** `Sql.Tekst()` udvaja samo apostrof, pa kriterijum `%` postaje `LIKE '%%%'` i vraća sve događaje. Isto važi za `_` (bilo koji znak) i `[` (skup znakova). Ovo je ograničenje, ne greška u bezbednosti: korisnik ne može da izađe iz literala, ali ne može ni da traži doslovne znake `%`, `_` i `[`.
- **Rešava se u:** Faza 6.1 (izbegavanje džoker znakova u LIKE)
