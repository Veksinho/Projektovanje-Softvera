# Poznate greške

Spisak mesta gde se kod ponaša drugačije od specifikacije (`SPECIFIKACIJA-TESTOVA.md`). Testovi za ove slučajeve su napisani kako je specificirano i označeni sa `[Ignore("Poznata greška: …")]`.

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
