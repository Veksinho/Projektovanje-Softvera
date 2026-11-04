# Inventar polja za unos

Faza 2, korak 2.1. Spisak svih polja za unos (`TextBox`, `RichTextBox`, `ComboBox`, `NumericUpDown`, `DateTimePicker`) u `Klijent/UserControls` i `Klijent/FrmLogin`. Napravljen na grani `improvements` posle commit-a `2ea953e`. Kod nije menjan.

Izvori:

- svojstvo: `VratiObjekat()` u user control-i, `Procitaj()` u `UCFizickoLice` / `UCPravnoLice`, a za pretrage `OsveziListu()` u odgovarajućem `*GuiController`-u (`UCPretraga*` nemaju `VratiObjekat`);
- kolona i dužina: `docs/INVENTAR-SQL-LITERALA.md`, tabela „Tipovi i dužine tekstualnih kolona“. Za brojčane kolone, koje inventar ne pokriva, tip je preuzet iz `docs/PRAVILA-VALIDACIJE.md` i označen sa ¹;
- `MaxLength` i filter: `*.Designer.cs` i konstruktori user control-a.

## Zaključak

- **Nijedno polje danas nema `MaxLength`** (ni u Designer-u ni u konstruktoru). `TextBox` ima podrazumevanih 32767 znakova, `RichTextBox` `int.MaxValue`.
- **Nijedno polje nema filter unosa** (`KeyPress`, `KeyDown`, `Validating`, `TextChanged` za proveru). Jedini `TextChanged` je u `UCListing` i samo osvežava prikaz iznosa.
- Brojevi se unose kao slobodan tekst i parsiraju sa `decimal.TryParse(..., CultureInfo.CurrentCulture)`; neispravan unos u formi postaje `-1` (Karta, Listing), a u pretrazi `0` (kriterijum se ignoriše).
- `ComboBox` sa `DropDownStyle = DropDownList` postoji samo u `UCListing` i `UCPretragaListing`. Svi ostali su `DropDown` (podrazumevano), pa korisnik može da kuca proizvoljan tekst; tada je `SelectedItem` `null`.
- Jedina ograničenja koja postoje: `UseSystemPasswordChar` na šifri (FrmLogin, UCBroker), `numMinKolicina` 1–100000, `MinDate` na datumu događaja (Add: danas) i datumu isteka listinga (Add: sutra).

Oznake u koloni „Danas“: **—** = nema `MaxLength` ni filtera.

## FrmLogin

| Kontrola | Tip | Svojstvo | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtKorisnickoIme` | TextBox | `Broker.KorisnickoIme` (`Trim`) | `Broker.korisnickoIme` (50) | — | `Validacija()`: samo obavezno |
| `txtSifra` | TextBox | `Broker.Sifra` (`Trim`, od 4. 11. 2026.) | `Broker.sifra` (100) | — | `UseSystemPasswordChar`; `Validacija()`: samo obavezna (`IsNullOrWhiteSpace`) |

## UCBroker

| Kontrola | Tip | Svojstvo | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtId` | TextBox | — (samo prikaz) | — | — | `Enabled = false` |
| `txtKorisnickoIme` | TextBox | `Broker.KorisnickoIme` | `Broker.korisnickoIme` (50) | — | |
| `txtSifra` | TextBox | `Broker.Sifra` (`Trim`) | `Broker.sifra` (100) | — | `UseSystemPasswordChar`; u Edit modu popunjen postojećom šifrom (`b.Sifra`); u Details sakriven |
| `txtIme` | TextBox | `Broker.Ime` | `Broker.ime` (50) | — | |
| `txtPrezime` | TextBox | `Broker.Prezime` | `Broker.prezime` (50) | — | |
| `txtTelefon` | TextBox | `Broker.Telefon` | `Broker.telefon` (30) | — | |

`clbKategorije` (`CheckedListBox` → `Broker.Specijalizacije`) nije tekstualno polje i nije u opsegu.

## UCKategorijaDogadjaja

| Kontrola | Tip | Svojstvo | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtId` | TextBox | — (samo prikaz) | — | — | `Enabled = false` |
| `txtNaziv` | TextBox | `KategorijaDogadjaja.Naziv` | `KategorijaDogadjaja.naziv` (100) | — | |
| `txtOpis` | RichTextBox | `KategorijaDogadjaja.Opis` | `KategorijaDogadjaja.opis` (500) | — | `RichTextBox`, podrazumevani `MaxLength` je `int.MaxValue` |

## UCDogadjaj

| Kontrola | Tip | Svojstvo | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtId` | TextBox | — (samo prikaz) | — | — | `Enabled = false` |
| `txtNaziv` | TextBox | `Dogadjaj.Naziv` | `Dogadjaj.naziv` (150) | — | |
| `txtMesto` | TextBox | `Dogadjaj.Mesto` | `Dogadjaj.mesto` (150) | — | |
| `dtpDatumOdrzavanja` | DateTimePicker | `Dogadjaj.DatumOdrzavanja` (`.Date`) | `Dogadjaj.datumOdrzavanja` (datum) | Add: `MinDate = DateTime.Today` | format podrazumevani (`Long`) |

## UCKonsignator

| Kontrola | Tip | Svojstvo | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `cmbTipKonsignatora` | ComboBox | bira podtip (`UCFizickoLice` / `UCPravnoLice`) | — | `DropDown` (kucanje dozvoljeno) | u Edit i Details onemogućen |
| `txtId` | TextBox | — (samo prikaz; `IdKonsignator` se uzima iz `k`) | — | — | `Enabled = false` |
| `txtEmail` | TextBox | `Konsignator.Email` | `Konsignator.email` (100) | — | |
| `txtTelefon` | TextBox | `Konsignator.Telefon` | `Konsignator.telefon` (30) | — | |
| `txtAdresa` | TextBox | `Konsignator.Adresa` | `Konsignator.adresa` (200) | — | |
| `dtpDatumRegistracije` | DateTimePicker | — (`VratiObjekat` prepisuje `k.DatumRegistracije`) | `Konsignator.datumRegistracije` (datum) | uvek `Enabled = false` | samo prikaz |

## UCFizickoLice (podtip u UCKonsignator)

| Kontrola | Tip | Svojstvo (`Procitaj`) | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtJmbg` | TextBox | `FizickoLice.Jmbg` | `FizickoLice.jmbg` (13) | — | |
| `txtIme` | TextBox | `FizickoLice.Ime` | `FizickoLice.ime` (50) | — | |
| `txtPrezime` | TextBox | `FizickoLice.Prezime` | `FizickoLice.prezime` (50) | — | |
| `txtBrojLicneKarte` | TextBox | `FizickoLice.BrojLicneKarte` | `FizickoLice.brojLicneKarte` (20) | — | |

## UCPravnoLice (podtip u UCKonsignator)

| Kontrola | Tip | Svojstvo (`Procitaj`) | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtNaziv` | TextBox | `PravnoLice.NazivFirme` | `PravnoLice.naziv` (150) | — | |
| `txtPib` | TextBox | `PravnoLice.Pib` | `PravnoLice.pib` (9) | — | |
| `txtMaticniBroj` | TextBox | `PravnoLice.MaticniBroj` | `PravnoLice.maticniBroj` (8) | — | |

## UCKarta

| Kontrola | Tip | Svojstvo | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `cmbKonsignator` | ComboBox | `Karta.Konsignator` | `idKonsignator` (FK) | `DropDown` (kucanje dozvoljeno) | |
| `cmbDogadjaj` | ComboBox | `Karta.Dogadjaj` | `idDogadjaj` (FK) | `DropDown` (kucanje dozvoljeno) | |
| `txtSektor` | TextBox | `Karta.Sektor` | `Karta.sektor` (50) | — | |
| `txtRed` | TextBox | `Karta.Red` | `Karta.red` (10) | — | |
| `txtSediste` | TextBox | `Karta.Sediste` | `Karta.sediste` (10) | — | |
| `txtNominalnaCena` | TextBox | `Karta.NominalnaCena` (`TryParse`, inače `-1`) | `Karta.nominalnaCena` (decimal(10,2)¹) | — | `KartaGuiController` proverava obaveznost i `TryParse` |
| `cmbTip` | ComboBox | `Karta.Tip` | `Karta.tip` (20, enum) | `DropDown` (kucanje dozvoljeno) | |
| `cmbFormat` | ComboBox | `Karta.Format` | `Karta.format` (20, enum) | `DropDown` (kucanje dozvoljeno) | |
| `cmbStatus` | ComboBox | `Karta.Status` | `Karta.status` (20, enum) | `DropDown` (kucanje dozvoljeno) | u Add zaključan na `u_inventaru` |
| `txtListing` | TextBox | — (samo prikaz `ListingPrikaz`) | — | — | `Enabled = false` |

## UCListing

| Kontrola | Tip | Svojstvo | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtId` | TextBox | — (samo prikaz) | — | — | `ReadOnly = true` |
| `cmbBroker` | ComboBox | `Listing.Broker` | `idBroker` (FK) | `DropDownList`; uvek onemogućen | |
| `cmbKonsignator` | ComboBox | `Listing.Konsignator` | `idKonsignator` (FK) | `DropDownList` | |
| `cmbStatus` | ComboBox | `Listing.Status` | `Listing.status` (20, enum) | `DropDownList` | u Add samo `nacrt` i `objavljen` |
| `dtpDatumObjave` | DateTimePicker | — (ne čita se u `VratiObjekat`) | `Listing.datumObjave` (datum) | `Format = Short`; uvek onemogućen | samo prikaz |
| `dtpDatumIsteka` | DateTimePicker | `Listing.DatumIsteka` (`.Date`) | `Listing.datumIsteka` (datum) | `Format = Short`; Add: `MinDate = sutra` | |
| `txtCenaPoKarti` | TextBox | `Listing.CenaPoKarti` (`TryParse`, inače `-1`) | `Listing.cenaPoKarti` (decimal(10,2)¹) | — | `TextChanged` samo osvežava prikaz |
| `txtProcenatProvizije` | TextBox | `Listing.ProcenatProvizije` (`TryParse`, inače `-1`) | `Listing.procenatProvizije` (decimal(5,2)¹) | — | `TextChanged` samo osvežava prikaz |
| `cmbSplit` | ComboBox | `Listing.Split` | `Listing.split` (20, enum) | `DropDownList` | |
| `numMinKolicina` | NumericUpDown | `Listing.MinKolicina` (`int`) | `Listing.minKolicina` (int, nije u inventaru) | `Minimum = 1`, `Maximum = 100000`, 0 decimala | |
| `txtNapomena` | TextBox | `Listing.Napomena` (prazno → `null`) | `Listing.napomena` (1000) | — | `Multiline = true` |

## UCPretragaBroker (`BrokerGuiController.OsveziListu`)

| Kontrola | Tip | Svojstvo kriterijuma | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtKorisnickoIme` | TextBox | `Broker.KorisnickoIme` | `Broker.korisnickoIme` (50), `LIKE` | — | |
| `txtIme` | TextBox | `Broker.Ime` | `Broker.ime` (50), `LIKE` | — | |
| `txtPrezime` | TextBox | `Broker.Prezime` | `Broker.prezime` (50), `LIKE` | — | |
| `txtTelefon` | TextBox | `Broker.Telefon` | `Broker.telefon` (30), `LIKE` | — | |
| `cmbKategorija` | ComboBox | `Broker.Specijalizacije[0].KategorijaDogadjaja` | `idKategorijaDogadjaja` (FK) | `DropDown` (kucanje dozvoljeno) | |

## UCPretragaKategorijaDogadjaja (`KategorijaDogadjajaGuiController.OsveziListu`)

| Kontrola | Tip | Svojstvo kriterijuma | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtNaziv` | TextBox | `KategorijaDogadjaja.Naziv` | `KategorijaDogadjaja.naziv` (100), `LIKE` | — | pretraga po opisu nema polje |

## UCPretragaDogadjaj (`DogadjajGuiController.OsveziListu`)

| Kontrola | Tip | Svojstvo kriterijuma | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtNaziv` | TextBox | `Dogadjaj.Naziv` | `Dogadjaj.naziv` (150), `LIKE` | — | |
| `txtMesto` | TextBox | `Dogadjaj.Mesto` | `Dogadjaj.mesto` (150), `LIKE` | — | |
| `dtpDatumOdrzavanja` | DateTimePicker | `Dogadjaj.DatumOdrzavanja` (ako je `Checked`) | `CAST(d.datumOdrzavanja AS DATE)` | `ShowCheckBox = true` | |

## UCPretragaKonsignator (`KonsignatorGuiController.OsveziListu`)

| Kontrola | Tip | Svojstvo kriterijuma | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `cmbTipKonsignatora` | ComboBox | `Konsignator.TipKriterijum` | — | `DropDown` (kucanje dozvoljeno) | |
| `txtNaziv` | TextBox | `Konsignator.NazivKriterijum` | `FizickoLice.ime` (50), `FizickoLice.prezime` (50), `PravnoLice.naziv` (150), `LIKE` | — | jedno polje za tri kolone |
| `txtEmail` | TextBox | `Konsignator.Email` | `Konsignator.email` (100), `LIKE` | — | |
| `txtTelefon` | TextBox | `Konsignator.Telefon` | `Konsignator.telefon` (30), `LIKE` | — | |

## UCPretragaKarta (`KartaGuiController.OsveziListu`)

| Kontrola | Tip | Svojstvo kriterijuma | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `txtSektor` | TextBox | `Karta.Sektor` | `Karta.sektor` (50), `LIKE` | — | |
| `cmbTip` | ComboBox | `Karta.Tip` | `Karta.tip` (20, enum) | `DropDown` (kucanje dozvoljeno) | |
| `cmbStatus` | ComboBox | `Karta.Status` | `Karta.status` (20, enum) | `DropDown` (kucanje dozvoljeno) | |
| `cmbKonsignator` | ComboBox | `Karta.Konsignator.IdKonsignator` | `idKonsignator` (FK) | `DropDown` (kucanje dozvoljeno) | |
| `txtNazivKonsignatora` | TextBox | `Karta.Konsignator.NazivKriterijum` | `FizickoLice.ime` (50), `FizickoLice.prezime` (50), `PravnoLice.naziv` (150), `LIKE` | — | jedno polje za tri kolone |
| `cmbDogadjaj` | ComboBox | `Karta.Dogadjaj.IdDogadjaj` | `idDogadjaj` (FK) | `DropDown` (kucanje dozvoljeno) | |
| `txtMesto` | TextBox | `Karta.Dogadjaj.Mesto` | `Dogadjaj.mesto` (150), `LIKE` | — | |

## UCPretragaListing (`ListingGuiController.OsveziListu`)

| Kontrola | Tip | Svojstvo kriterijuma | Kolona (dužina) | Danas | Napomena |
| --- | --- | --- | --- | --- | --- |
| `cmbStatus` | ComboBox | `Listing.Status` | `Listing.status` (20, enum) | `DropDownList` | |
| `cmbSplit` | ComboBox | `Listing.Split` | `Listing.split` (20, enum) | `DropDownList` | |
| `dtpObjavljenOd` | DateTimePicker | `Listing.DatumObjaveOd` (ako je `Checked`) | `CAST(l.datumObjave AS DATE)` | `Format = Short`, `ShowCheckBox = true` | |
| `dtpObjavljenDo` | DateTimePicker | `Listing.DatumObjaveDo` (ako je `Checked`) | `CAST(l.datumObjave AS DATE)` | `Format = Short`, `ShowCheckBox = true` | |
| `txtCenaOd` | TextBox | `Listing.CenaOd` (`ParsirajIznos`, inače `0`) | `Listing.cenaPoKarti` (decimal(10,2)¹), `>=` | — | neispravan unos se tiho ignoriše |
| `txtCenaDo` | TextBox | `Listing.CenaDo` (`ParsirajIznos`, inače `0`) | `Listing.cenaPoKarti` (decimal(10,2)¹), `<=` | — | neispravan unos se tiho ignoriše |
| `cmbKonsignator` | ComboBox | `Listing.Konsignator.IdKonsignator` | `idKonsignator` (FK) | `DropDownList` | |
| `txtNazivKonsignatora` | TextBox | `Listing.Konsignator.NazivKriterijum` | `FizickoLice.ime` (50), `FizickoLice.prezime` (50), `PravnoLice.naziv` (150), `LIKE` | — | jedno polje za tri kolone |
| `cmbDogadjaj` | ComboBox | `Listing.KriterijumDogadjaj.IdDogadjaj` | `idDogadjaj` (FK) | `DropDownList` | |
| `txtMesto` | TextBox | `Listing.KriterijumDogadjaj.Mesto` | `Dogadjaj.mesto` (150), `LIKE` | — | |
| `txtSektor` | TextBox | `Listing.KriterijumKarta.Sektor` | `Karta.sektor` (50), `LIKE` | — | |
| `cmbTipKarte` | ComboBox | `Listing.KriterijumKarta.Tip` | `Karta.tip` (20, enum) | `DropDownList` | |

¹ Tip brojčane kolone nije u `INVENTAR-SQL-LITERALA.md`; preuzet je iz `PRAVILA-VALIDACIJE.md`.

## Razlike u odnosu na `docs/PRAVILA-VALIDACIJE.md`

1. **Pravila još nisu primenjena nigde na klijentu.** Nijedno polje iz tabele pravila nema `MaxLength` ni filter cifara. Ovo je očekivano stanje pre koraka 2.2, ali znači da se svako pravilo uvodi od nule.
2. **Pretraga po JMBG, PIB, matičnom broju i broju lične karte nema polje.** Pravila za `UCPretraga*` traže filter cifara za ta polja, ali `UCPretragaKonsignator` ima samo tip, naziv, e-mail i telefon. Domenske klase (`FizickoLice.AdditionalSearchCondition`, `PravnoLice.AdditionalSearchCondition`) podržavaju te kriterijume, ali ih forma ne šalje. Od polja za koja pravilo traži filter cifara, u pretragama postoji samo telefon (`UCPretragaBroker.txtTelefon`, `UCPretragaKonsignator.txtTelefon`). Treba odlučiti da li se dodaju polja ili se pravilo skraćuje.
3. **Filter cifara za telefon u pretrazi nije precizan.** Pravilo kaže „filter cifara“, a telefon u formi sme da počne sa `+`. Nije navedeno da li pretraga prihvata `+` na prvom mestu.
4. **Polje „naziv konsignatora“ u pretragama pokriva tri kolone različite dužine** (`fl.ime` 50, `fl.prezime` 50, `pl.naziv` 150): `UCPretragaKonsignator.txtNaziv`, `UCPretragaKarta.txtNazivKonsignatora`, `UCPretragaListing.txtNazivKonsignatora`. Pravila ne kažu koji `MaxLength` važi; predlog je 150 (najduža kolona).
5. **Iznosi u pretrazi nisu pokriveni.** `UCPretragaListing.txtCenaOd` i `txtCenaDo` su brojčana polja, a red za pretrage pominje samo tekstualna polja. Nema pravila za filter, decimalni separator (`.` i `,`) ni za neispravan unos, koji `ParsirajIznos` danas tiho pretvara u `0` (kriterijum se ignoriše).
6. **Minimalna količina na listingu nema pravilo.** `UCListing.numMinKolicina` (kolona `int`) ima u Designer-u opseg 1–100000, ali u pravilima nema reda za nju.
7. **Datumi listinga nemaju pravilo.** Pravila za datum navode samo „datum kao do sada“ za Dogadjaj. Za `UCListing.dtpDatumIsteka` (Add: `MinDate` = sutra) i odnos datuma objave i isteka nema reda, kao ni za opseg `dtpObjavljenOd` / `dtpObjavljenDo` u pretrazi (Od ≤ Do).
8. **`MaxLength` nije uvek jednak dužini kolone.** Za polja sa tačnim brojem cifara pravilo je strože od kolone, pa `MaxLength` treba da prati pravilo: broj lične karte 9 (kolona 20), telefon 16 = `+` i 15 cifara (kolona 30), šifra 64 (kolona 100). Pravila ovo ne kažu izričito; za šifru na `FrmLogin` (samo obavezna) treba odlučiti između 64 i 100.
9. **Šifra u Edit modu nikad nije „neuneta“.** Pravilo za Promeni kaže „ista pravila samo ako je uneta“, a `UCBroker` u Edit modu popunjava `txtSifra` postojećom šifrom iz baze (`Broker` čita kolonu `sifra`). Polje zato nikad nije prazno, pa bi se pravila primenila i na postojeće šifre koje ih možda ne ispunjavaju.
10. **Šifra se različito obrađuje na dva mesta.** `FrmLogin` šalje `txtSifra.Text` bez `Trim`, a `UCBroker.VratiObjekat` radi `Trim`. Pravila ne kažu da li se razmaci na početku i kraju šifre uklanjaju; od toga zavisi i provera dužine 8–64.
11. **`ComboBox` sa slobodnim kucanjem.** Osim u `UCListing` i `UCPretragaListing`, sve liste su `DropDown`, pa korisnik može da ukuca tekst koji nije stavka (`SelectedItem` je tada `null`). Pravila ne pominju liste; obaveznost se danas proverava kao `SelectedItem == null`, pa ovo nije greška, ali `DropDownList` bi bio jednostavniji i ne zahteva promenu pravila. Promena je u `*.Designer.cs` ili u konstruktoru i nije deo ovog koraka.
12. **Opis kategorije je `RichTextBox`.** Pravilo ≤ 500 važi i za njega, ali podrazumevani `MaxLength` za `RichTextBox` je `int.MaxValue`, a ne 32767 kao kod `TextBox`.

### Stanje razlika (4. 11. 2026.)

| # | Stanje |
| --- | --- |
| 2 | Rešeno (O11): polja se ne dodaju, pravilo za pretrage je skraćeno na telefon |
| 4 | Rešeno (O11): `MaxLength` 150 |
| 5, 6, 7 | Rešeno (O13): pravila za cenu od/do, minimalnu količinu, datum isteka i objavljen od/do |
| 8 | Rešeno (O12): `MaxLength` prati pravilo; `MaxLength` šifre na `FrmLogin` ostaje za fazu 3 |
| 9 | Odloženo za fazu 3 (šifre); danas ne pravi grešku |
| 10 | Rešeno (O10): `FrmLogin` trimuje šifru, a `Validacija()` koristi `IsNullOrWhiteSpace` |
| 3 | Rešeno (O14): `+` dozvoljen na prvom mestu |
| 11 | Rešeno (O15): svih 12 lista postaje `DropDownList` |
