using Common.Domen;
using Common.Domen.Enumeracije;
using Common.Validacija;
using static Common.Tests.PomocPravila;

namespace Common.Tests
{
    [TestClass]
    public class PravilaListingTest
    {
        private static List<Karta> Karte(int broj) =>
            Enumerable.Range(1, broj).Select(i => new Karta { IdKarta = i }).ToList();

        private static Listing Ispravan() => new Listing
        {
            Broker = new Broker { IdBroker = 1 },
            Konsignator = new FizickoLice { IdKonsignator = 1 },
            Status = StatusListinga.objavljen,
            Split = TipSplita.bilo_koja_kolicina,
            CenaPoKarti = 2500.50m,
            ProcenatProvizije = 12.5m,
            Napomena = null,
            DatumObjave = DateTime.Today.AddDays(-5),
            DatumIsteka = DateTime.Today.AddDays(10),
            Karte = Karte(4),
            MinKolicina = 1
        };

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestIspravanListingNemaGresaka(bool novi)
        {
            NemaGresaka(PravilaListing.Proveri(Ispravan(), novi));
        }

        [TestMethod]
        public void TestNullListingBacaIzuzetak()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => PravilaListing.Proveri(null!, true));
        }

        [TestMethod]
        public void TestBrokerObavezan()
        {
            Listing l = Ispravan();
            l.Broker = null;

            JednaGreska(PravilaListing.Proveri(l, true), nameof(Listing.Broker), "Broker listinga je obavezan.");
        }

        [TestMethod]
        public void TestKonsignatorObavezan()
        {
            Listing l = Ispravan();
            l.Konsignator = new FizickoLice { IdKonsignator = 0 };

            JednaGreska(PravilaListing.Proveri(l, true), nameof(Listing.Konsignator), "Konsignator listinga je obavezan.");
        }

        [TestMethod]
        public void TestStatusObavezan()
        {
            Listing l = Ispravan();
            l.Status = null;

            JednaGreska(PravilaListing.Proveri(l, true), nameof(Listing.Status), "Status listinga je obavezan.");
        }

        [TestMethod]
        [DataRow(StatusListinga.povucen)]
        [DataRow(StatusListinga.realizovan)]
        public void TestNoviListingSamoNacrtIliObjavljen(StatusListinga status)
        {
            Listing l = Ispravan();
            l.Status = status;

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.Status), "Novi listing može biti samo nacrt ili objavljen.");
            NemaGresaka(PravilaListing.Proveri(l, false));
        }

        [TestMethod]
        public void TestSplitObavezan()
        {
            Listing l = Ispravan();
            l.Split = null;

            JednaGreska(PravilaListing.Proveri(l, true), nameof(Listing.Split), "Način prodaje listinga je obavezan.");
        }

        [TestMethod]
        [DataRow("0")]
        [DataRow("-0.01")]
        [DataRow("-1")]
        public void TestCenaPoKartiVecaOdNule(string vrednost)
        {
            Listing l = Ispravan();
            l.CenaPoKarti = decimal.Parse(vrednost, System.Globalization.CultureInfo.InvariantCulture);

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.CenaPoKarti), "Cena po karti listinga mora biti veća od nule.");
        }

        [TestMethod]
        public void TestCenaPoKartiDecimale()
        {
            Listing l = Ispravan();
            l.CenaPoKarti = 0.001m;

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.CenaPoKarti), "Cena po karti listinga može imati najviše 2 decimale.");
        }

        [TestMethod]
        public void TestCenaPoKartiMaksimum()
        {
            Listing l = Ispravan();
            l.CenaPoKarti = 99_999_999.99m;
            NemaGresaka(PravilaListing.Proveri(l, true));

            l.CenaPoKarti = 100_000_000m;
            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.CenaPoKarti), "Cena po karti listinga može biti najviše 99.999.999,99.");
        }

        [TestMethod]
        [DataRow("-0.01")]
        [DataRow("100.01")]
        public void TestProvizijaOpseg(string vrednost)
        {
            Listing l = Ispravan();
            l.ProcenatProvizije = decimal.Parse(vrednost, System.Globalization.CultureInfo.InvariantCulture);

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.ProcenatProvizije), "Procenat provizije listinga mora biti između 0 i 100.");
        }

        [TestMethod]
        public void TestProvizijaGranice()
        {
            Listing l = Ispravan();
            l.ProcenatProvizije = 0m;
            NemaGresaka(PravilaListing.Proveri(l, true));

            l.ProcenatProvizije = 100m;
            NemaGresaka(PravilaListing.Proveri(l, true));
        }

        [TestMethod]
        public void TestProvizijaDecimale()
        {
            Listing l = Ispravan();
            l.ProcenatProvizije = 12.345m;

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.ProcenatProvizije), "Procenat provizije listinga može imati najviše 2 decimale.");
        }

        [TestMethod]
        public void TestNapomenaDuzina()
        {
            Listing l = Ispravan();
            l.Napomena = new string('a', 1000);
            NemaGresaka(PravilaListing.Proveri(l, true));

            l.Napomena = new string('a', 1001);
            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.Napomena), "Napomena listinga može imati najviše 1000 znakova.");
        }

        [TestMethod]
        public void TestDatumIstekaObavezan()
        {
            Listing l = Ispravan();
            l.DatumIsteka = default;

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.DatumIsteka), "Datum isteka listinga je obavezan.");
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void TestDatumIstekaNovogListingaPosleDanasnjegDana(int dana)
        {
            Listing l = Ispravan();
            l.DatumIsteka = DateTime.Today.AddDays(dana);

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.DatumIsteka), "Datum isteka listinga mora biti posle datuma objave.");
        }

        [TestMethod]
        public void TestDatumIstekaPostojecegListingaPosleDatumaObjave()
        {
            Listing l = Ispravan();
            l.DatumObjave = DateTime.Today.AddDays(-10);
            l.DatumIsteka = DateTime.Today.AddDays(-3);
            NemaGresaka(PravilaListing.Proveri(l, false));

            l.DatumIsteka = l.DatumObjave.Date.AddHours(23);
            JednaGreska(PravilaListing.Proveri(l, false),
                nameof(Listing.DatumIsteka), "Datum isteka listinga mora biti posle datuma objave.");
        }

        [TestMethod]
        public void TestKarteObavezne()
        {
            Listing l = Ispravan();
            l.Karte = new List<Karta>();

            JednaGreska(PravilaListing.Proveri(l, true), nameof(Listing.Karte), "Listing mora imati bar jednu kartu.");
        }

        [TestMethod]
        public void TestMinKolicinaVecaOdNule()
        {
            Listing l = Ispravan();
            l.MinKolicina = 0;

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.MinKolicina), "Minimalna količina listinga mora biti veća od nule.");
        }

        [TestMethod]
        public void TestMinKolicinaNajviseBrojKarata()
        {
            Listing l = Ispravan();
            l.Split = TipSplita.min_kolicina;
            l.MinKolicina = 5;

            JednaGreska(PravilaListing.Proveri(l, true),
                nameof(Listing.MinKolicina), "Minimalna količina ne može biti veća od broja karata na listingu.");
        }

        [TestMethod]
        [DataRow(TipSplita.bez_splita, 4, 4)]
        [DataRow(TipSplita.bilo_koja_kolicina, 4, 1)]
        [DataRow(TipSplita.parne_kolicine, 4, 2)]
        [DataRow(TipSplita.min_kolicina, 4, 2)]
        [DataRow(TipSplita.izbegni_usamljenu, 2, 1)]
        public void TestSplitIspravan(TipSplita split, int brojKarata, int minKolicina)
        {
            Listing l = Ispravan();
            l.Split = split;
            l.Karte = Karte(brojKarata);
            l.MinKolicina = minKolicina;

            NemaGresaka(PravilaListing.Proveri(l, true));
        }

        [TestMethod]
        [DataRow(TipSplita.bez_splita, 4, 2,
            "Kod prodaje bez deljenja minimalna količina mora biti jednaka broju karata.")]
        [DataRow(TipSplita.bilo_koja_kolicina, 4, 2,
            "Kod prodaje u bilo kojoj količini minimalna količina mora biti 1.")]
        [DataRow(TipSplita.parne_kolicine, 3, 2,
            "Kod prodaje u parnim količinama broj karata i minimalna količina moraju biti parni.")]
        [DataRow(TipSplita.parne_kolicine, 4, 3,
            "Kod prodaje u parnim količinama broj karata i minimalna količina moraju biti parni.")]
        [DataRow(TipSplita.min_kolicina, 4, 1,
            "Kod prodaje sa minimalnom količinom ta količina mora biti veća od 1.")]
        [DataRow(TipSplita.izbegni_usamljenu, 1, 1,
            "Kod izbegavanja usamljene karte listing mora imati bar dve karte, a minimalna količina mora biti 1.")]
        [DataRow(TipSplita.izbegni_usamljenu, 4, 2,
            "Kod izbegavanja usamljene karte listing mora imati bar dve karte, a minimalna količina mora biti 1.")]
        public void TestSplitNeispravan(TipSplita split, int brojKarata, int minKolicina, string poruka)
        {
            Listing l = Ispravan();
            l.Split = split;
            l.Karte = Karte(brojKarata);
            l.MinKolicina = minKolicina;

            JednaGreska(PravilaListing.Proveri(l, true), nameof(Listing.MinKolicina), poruka);
        }

        // Kriterijum pretrage (O13)

        [TestMethod]
        public void TestPrazanKriterijumNemaGresaka()
        {
            NemaGresaka(PravilaListing.ProveriKriterijum(new Listing()));
        }

        [TestMethod]
        public void TestPopunjenKriterijumNemaGresaka()
        {
            Listing k = new Listing
            {
                CenaOd = 100m,
                CenaDo = 100m,
                DatumObjaveOd = DateTime.Today.AddDays(-7),
                DatumObjaveDo = DateTime.Today.AddDays(-7).AddHours(5)
            };

            NemaGresaka(PravilaListing.ProveriKriterijum(k));
        }

        [TestMethod]
        public void TestCenaOdNegativna()
        {
            JednaGreska(PravilaListing.ProveriKriterijum(new Listing { CenaOd = -1m }),
                nameof(Listing.CenaOd), "Cena od ne može biti negativna.");
        }

        [TestMethod]
        public void TestCenaOdDecimale()
        {
            JednaGreska(PravilaListing.ProveriKriterijum(new Listing { CenaOd = 1.234m }),
                nameof(Listing.CenaOd), "Cena od može imati najviše 2 decimale.");
        }

        [TestMethod]
        public void TestCenaOdMaksimum()
        {
            JednaGreska(PravilaListing.ProveriKriterijum(new Listing { CenaOd = 100_000_000m }),
                nameof(Listing.CenaOd), "Cena od može biti najviše 99.999.999,99.");
        }

        [TestMethod]
        public void TestCenaDoNegativna()
        {
            JednaGreska(PravilaListing.ProveriKriterijum(new Listing { CenaDo = -1m }),
                nameof(Listing.CenaDo), "Cena do ne može biti negativna.");
        }

        [TestMethod]
        public void TestCenaDoDecimale()
        {
            JednaGreska(PravilaListing.ProveriKriterijum(new Listing { CenaDo = 1.234m }),
                nameof(Listing.CenaDo), "Cena do može imati najviše 2 decimale.");
        }

        [TestMethod]
        public void TestCenaDoMaksimum()
        {
            JednaGreska(PravilaListing.ProveriKriterijum(new Listing { CenaDo = 100_000_000m }),
                nameof(Listing.CenaDo), "Cena do može biti najviše 99.999.999,99.");
        }

        [TestMethod]
        public void TestCenaOdVecaOdCeneDo()
        {
            JednaGreska(PravilaListing.ProveriKriterijum(new Listing { CenaOd = 200m, CenaDo = 100m }),
                nameof(Listing.CenaOd), "Cena od ne može biti veća od cene do.");
        }

        [TestMethod]
        public void TestDatumObjaveOdPosleDatumaObjaveDo()
        {
            Listing k = new Listing
            {
                DatumObjaveOd = DateTime.Today,
                DatumObjaveDo = DateTime.Today.AddDays(-1)
            };

            JednaGreska(PravilaListing.ProveriKriterijum(k),
                nameof(Listing.DatumObjaveOd), "Datum objave od ne može biti posle datuma objave do.");
        }
    }
}
