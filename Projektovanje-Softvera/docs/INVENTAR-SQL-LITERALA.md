# Inventar SQL literala

Faza 1, korak 1. Spisak svih mesta gde tekst ulazi u SQL kao literal `'…'`. Napravljen na grani `improvements` posle commit-a `ee457b5`.

Komande:

```bash
git grep -n "Sql.Tekst(" -- "*.cs"
git grep -nP "'\{(?!Sql\.Tekst\()" -- "*.cs"
git grep -nE "\"'\" *\+|string\.Format" -- "*.cs"
```

## Zaključak

- Sva tekstualna polja koja ulaze u SQL prolaze kroz `Sql.Tekst()` (`Common/Helpers/SqlHelper.cs`), koji udvaja apostrof.
- U trenutku pravljenja inventara nijedan literal nije imao prefiks `N`. Kolacija baze je `SQL_Latin1_General_CP1_CI_AS`, pa su se ć, č, đ (Ć, Č, Đ) gubili pri upisu i u uslovima pretrage. **Faza 1, korak 2:** svi literali iz tabela ispod dobili su prefiks `N` (`N'…'`, `LIKE N'%…%'`), uključujući uslove sa lokalnom promenljivom `naziv` u `Karta.SearchCondition` i `Listing.SearchCondition`. Enumi, datumi i brojevi su ostali bez prefiksa.
- `Sql.Tekst()` ne izbegava džoker znake `%`, `_` i `[` u `LIKE` uslovima. To rešava faza 6.1.
- Treća komanda (`"'" +` i `string.Format`) nema pogodaka.

## Domenske klase (`Common/Domen`)

| Fajl | Član | Kolona | Operator |
| --- | --- | --- | --- |
| `KategorijaDogadjaja.cs` | `InsertValues` | `naziv`, `opis` | vrednost |
| | `UpdateValues` | `naziv`, `opis` | `=` |
| | `SearchCondition` | `kd.naziv`, `kd.opis` | `LIKE '%…%'` |
| `Dogadjaj.cs` | `InsertValues` | `naziv`, `mesto` | vrednost |
| | `UpdateValues` | `naziv`, `mesto` | `=` |
| | `SearchCondition` | `d.naziv`, `d.mesto` | `LIKE '%…%'` |
| `Broker.cs` | `InsertValues` | `korisnickoIme`, `sifra`, `ime`, `prezime`, `telefon` | vrednost |
| | `UpdateValues` | `korisnickoIme`, `sifra`, `ime`, `prezime`, `telefon` | `=` |
| | `SearchCondition` (sa šifrom, prijava) | `b.korisnickoIme`, `b.sifra` | `=` |
| | `SearchCondition` (bez šifre) | `b.korisnickoIme` | `LIKE '%…%'` |
| | `SearchCondition` | `b.ime`, `b.prezime`, `b.telefon` | `LIKE '%…%'` |
| `Konsignator.cs` | `InsertValues` | `email`, `telefon`, `adresa` | vrednost |
| | `UpdateValues` | `email`, `telefon`, `adresa` | `=` |
| | `SearchCondition` | `k.email`, `k.telefon`, `k.adresa` | `LIKE '%…%'` |
| | `SearchCondition` (`NazivKriterijum`) | `fl.ime`, `fl.prezime`, `pl.naziv` | `LIKE '%…%'` |
| `FizickoLice.cs` | `SubtypeInsertValues` | `jmbg`, `ime`, `prezime`, `brojLicneKarte` | vrednost |
| | `SubtypeUpdateValues` | `jmbg`, `ime`, `prezime`, `brojLicneKarte` | `=` |
| | `AdditionalSearchCondition` | `fl.jmbg`, `fl.ime`, `fl.prezime`, `fl.brojLicneKarte` | `LIKE '%…%'` |
| `PravnoLice.cs` | `SubtypeInsertValues` | `pib`, `maticniBroj`, `naziv` | vrednost |
| | `SubtypeUpdateValues` | `pib`, `maticniBroj`, `naziv` | `=` |
| | `AdditionalSearchCondition` | `pl.pib`, `pl.maticniBroj`, `pl.naziv` | `LIKE '%…%'` |
| `Karta.cs` | `InsertValues` | `sektor`, `red`, `sediste` | vrednost |
| | `UpdateValues` | `sektor`, `red`, `sediste` | `=` |
| | `SearchCondition` | `ka.sektor`, `ka.red`, `ka.sediste` | `LIKE '%…%'` |
| | `SearchCondition` (`Konsignator.NazivKriterijum`) | `fl.ime`, `fl.prezime`, `pl.naziv` | `LIKE '%…%'` |
| | `SearchCondition` (`Dogadjaj.Mesto`) | `d.mesto` | `LIKE '%…%'` |
| `Listing.cs` | `InsertValues` / `UpdateValues` (`NapomenaSql`) | `napomena` (`NULL` ako je prazna) | vrednost / `=` |
| | `SearchCondition` (`Konsignator.NazivKriterijum`) | `fl.ime`, `fl.prezime`, `pl.naziv` | `LIKE '%…%'` |
| | `UslovNadKartama` (`KriterijumKarta.Sektor`) | `ka2.sektor` | `LIKE '%…%'` |
| | `UslovNadKartama` (`KriterijumDogadjaj.Mesto`) | `d2.mesto` | `LIKE '%…%'` |
| `BrokerKategorijaDogadjaja.cs` | — | nema tekstualnih kolona | — |

## Sistemske operacije (`SistemskeOperacije`)

| Fajl | Član | Kolona | Operator |
| --- | --- | --- | --- |
| `BrokerSO/KreirajBrokerSO.cs` | `Validate` (jedinstvenost) | `b.korisnickoIme` | `=` |
| `BrokerSO/PromeniBrokerSO.cs` | `Validate` (jedinstvenost, `idBroker <>`) | `b.korisnickoIme` | `=` |
| `KartaSO/KreirajKartaSO.cs` | `ProveriZauzetostSedista` | `ka.sektor`, `ka.red`, `ka.sediste` | `=` |
| `KartaSO/PromeniKartaSO.cs` | `ProveriZauzetostSedista` (`idKarta <>`) | `ka.sektor`, `ka.red`, `ka.sediste` | `=` |
| `KategorijaDogadjajaSO/UbaciKategorijaDogadjajaSO.cs` | `Validate` (jedinstvenost, Faza 2) | `kd.naziv` | `=` |
| `KategorijaDogadjajaSO/PromeniKategorijaDogadjajaSO.cs` | `Validate` (jedinstvenost, `idKategorijaDogadjaja <>`, Faza 2) | `kd.naziv` | `=` |
| `KonsignatorSO/KreirajKonsignatorSO.cs` | `ProveriJedinstvenostEmaila` (Faza 2) | `k.email` | `=` |
| | `ValidirajFizickoLice` | `fl.jmbg` | `=` |
| | `ValidirajPravnoLice` | `pl.pib` | `=` |
| `KonsignatorSO/PromeniKonsignatorSO.cs` | `ProveriJedinstvenostEmaila` (`idKonsignator <>`, Faza 2) | `k.email` | `=` |
| | `ValidirajFizickoLice` (`idKonsignator <>`) | `fl.jmbg` | `=` |
| | `ValidirajPravnoLice` (`idKonsignator <>`) | `pl.pib` | `=` |
| `PrijaviBrokerSO.cs` | `ExecuteConcreteOperation` | koristi `Broker.SearchCondition` (sa šifrom) | `=` |

Ostali uslovi u SO (`ObrisiBrokerSO`, `ObrisiDogadjajSO`, `ObrisiKategorijaDogadjajaSO`, `ObrisiKonsignatorSO`, `KreirajListingSO`, `PromeniListingSO`) sadrže samo celobrojne Id-eve (`= {Id}`, `IN ({identifikatori})`).

## Literali bez `Sql.Tekst()` (druga komanda)

Nijedan nije slobodan tekst. Navedeni su da bi inventar bio potpun.

| Fajl | Član | Kolona | Vrsta vrednosti |
| --- | --- | --- | --- |
| `BrokerKategorijaDogadjaja.cs` | `InsertValues`, `UpdateValues` | `datumSpecijalizacije` | `DateTime`, format `yyyy-MM-dd` |
| `Dogadjaj.cs` | `InsertValues`, `UpdateValues` | `datumOdrzavanja` | `DateTime`, format `yyyy-MM-dd HH:mm:ss` |
| | `SearchCondition` | `CAST(d.datumOdrzavanja AS DATE)` | `DateTime`, format `yyyy-MM-dd` |
| `Konsignator.cs` | `InsertValues` | `datumRegistracije` | `DateTime`, format `yyyy-MM-dd` |
| `Karta.cs` | `InsertValues`, `UpdateValues`, `SearchCondition` | `tip`, `format`, `status` | enum (`TipKarte`, `FormatKarte`, `StatusKarte`) |
| `Listing.cs` | `InsertValues`, `UpdateValues` | `datumObjave`, `datumIsteka` | `DateTime`, format `yyyy-MM-dd HH:mm:ss` |
| | `InsertValues`, `UpdateValues`, `SearchCondition` | `status`, `split` | enum (`StatusListinga`, `TipSplita`) |
| | `SearchCondition` | `CAST(l.datumObjave AS DATE)` (`DatumObjaveOd`, `DatumObjaveDo`) | `DateTime`, format `yyyy-MM-dd` |
| | `UslovNadKartama` | `ka2.tip` | enum (`TipKarte`) |

Ostali pogoci druge komande nisu SQL nad produkcionom bazom: poruke u `MessageBox.Show` (`Klijent/GuiControllers`), poruka izuzetka u `PodesavanjeTestova.cs` i pomoćni upiti u testovima (`TestBaza.Execute` / `TestBaza.BrojRedova` sa datumima i e-mailom iz `TestPodaci`).

Brojevi ulaze bez navodnika: Id-evi kao `int`, a `decimal` kroz `Sql.Broj()` (invarijantna kultura).

## String polja bez `Sql.Tekst()`

Nema ih.

## Tipovi i dužine tekstualnih kolona

Faza 1, korak 1.2. Rezultat upita nad `INFORMATION_SCHEMA.COLUMNS` (razvojna baza; test baza ima istu šemu). Sve tekstualne kolone su `nvarchar` sa kolacijom `SQL_Latin1_General_CP1_CI_AS`, pa prefiks `N` čuva ć, č i đ i nijedna kolona ne mora da se menja.

Dužine su ulaz za Fazu 2 (`MaxLength` na poljima i provera dužine u `Validate()`), a tipovi za fazu 6.1 (parametri tipa `nvarchar`, iste dužine kao kolona).

| Tabela | Kolona | Tip | Dužina | Napomena |
| --- | --- | --- | --- | --- |
| `Broker` | `korisnickoIme` | `nvarchar` | 50 | |
| | `sifra` | `nvarchar` | 100 | dovoljno za BCrypt heš (60 znakova) u Fazi 3 |
| | `ime` | `nvarchar` | 50 | |
| | `prezime` | `nvarchar` | 50 | |
| | `telefon` | `nvarchar` | 30 | |
| `Dogadjaj` | `naziv` | `nvarchar` | 150 | |
| | `mesto` | `nvarchar` | 150 | |
| `FizickoLice` | `jmbg` | `nvarchar` | 13 | Faza 2: tačno 13 cifara |
| | `ime` | `nvarchar` | 50 | |
| | `prezime` | `nvarchar` | 50 | |
| | `brojLicneKarte` | `nvarchar` | 20 | Faza 2: 9 cifara |
| `Karta` | `sektor` | `nvarchar` | 50 | |
| | `red` | `nvarchar` | 10 | |
| | `sediste` | `nvarchar` | 10 | |
| | `tip` | `nvarchar` | 20 | enum |
| | `format` | `nvarchar` | 20 | enum |
| | `status` | `nvarchar` | 20 | enum |
| `KategorijaDogadjaja` | `naziv` | `nvarchar` | 100 | |
| | `opis` | `nvarchar` | 500 | |
| `Konsignator` | `email` | `nvarchar` | 100 | |
| | `telefon` | `nvarchar` | 30 | |
| | `adresa` | `nvarchar` | 200 | |
| `Listing` | `status` | `nvarchar` | 20 | enum |
| | `split` | `nvarchar` | 20 | enum |
| | `napomena` | `nvarchar` | 1000 | |
| `PravnoLice` | `pib` | `nvarchar` | 9 | Faza 2: tačno 9 cifara |
| | `maticniBroj` | `nvarchar` | 8 | Faza 2: tačno 8 cifara |
| | `naziv` | `nvarchar` | 150 | |
