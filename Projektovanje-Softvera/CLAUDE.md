# PreprodajaKarata – pravila za rad
- .NET 8, WinForms klijent, TCP/JSON server, SQL Server LocalDB.
- Imena klasa, metoda i poruka su na srpskom (latinica sa č, ć, ž, š, đ). Commit poruke i tagovi su na engleskom.
- Radi se na grani improvements. Ne commit-uj i ne pravi tagove bez izričitog zahteva.
- Testovi: dotnet test u ovom folderu. Integracioni testovi rade samo nad bazom Seminarski-Softveri_Test. Razvojnu bazu Seminarski-Softveri ne menjaj bez pitanja.
- Ne menjaj tekst postojećih poruka izuzetaka: testovi ih porede doslovno.
- Tekstualni literal u SQL-u uvek ide kao N'{Sql.Tekst(x)}'. Parametrizacija dolazi tek u fazi 6.1 – ne uvodi je ranije.
- Ne menjaj *.Designer.cs osim kad je to cilj zadatka; MaxLength i događaje polja podešavaj u konstruktoru user control-e.
- Reference: docs/POZNATE-GRESKE.md, docs/INVENTAR-SQL-LITERALA.md, docs/SPECIFIKACIJA-TESTOVA.md.