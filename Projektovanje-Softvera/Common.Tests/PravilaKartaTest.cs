using Common.Domen;
using Common.Domen.Enumeracije;
using Common.Validacija;
using static Common.Tests.PomocPravila;

namespace Common.Tests
{
    [TestClass]
    public class PravilaKartaTest
    {
        private static Karta Ispravna() => new Karta
        {
            Konsignator = new FizickoLice { IdKonsignator = 1 },
            Dogadjaj = new Dogadjaj { IdDogadjaj = 1 },
            Sektor = "A",
            Red = "5",
            Sediste = "12",
            NominalnaCena = 1500.50m,
            Tip = TipKarte.sedeca,
            Format = FormatKarte.pdf,
            Status = StatusKarte.u_inventaru
        };

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestIspravnaKartaNemaGresaka(bool novi)
        {
            NemaGresaka(PravilaKarta.Proveri(Ispravna(), novi));
        }

        [TestMethod]
        public void TestStatusObavezanPriIzmeni()
        {
            Karta ka = Ispravna();
            ka.Status = null;

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Status), "Status karte je obavezan.");
        }

        [TestMethod]
        public void TestStatusSeNeProveravaPriKreiranju()
        {
            Karta ka = Ispravna();
            ka.Status = null;

            NemaGresaka(PravilaKarta.Proveri(ka, true));
        }

        [TestMethod]
        public void TestStatusPosleTipaIFormata()
        {
            Karta ka = Ispravna();
            ka.Tip = null;
            ka.Format = null;
            ka.Status = null;

            CollectionAssert.AreEqual(
                new[] { nameof(Karta.Tip), nameof(Karta.Format), nameof(Karta.Status) },
                PravilaKarta.Proveri(ka, false).Select(g => g.Polje).ToArray());
        }

        [TestMethod]
        public void TestCrticaIBesplatnaKartaSuDozvoljeni()
        {
            Karta ka = Ispravna();
            ka.Sektor = "-";
            ka.Red = "-";
            ka.Sediste = "-";
            ka.NominalnaCena = 0m;

            NemaGresaka(PravilaKarta.Proveri(ka, false));
        }

        [TestMethod]
        public void TestKonsignatorObavezan()
        {
            Karta ka = Ispravna();
            ka.Konsignator = null;

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Konsignator), "Konsignator karte je obavezan.");
        }

        [TestMethod]
        public void TestDogadjajObavezan()
        {
            Karta ka = Ispravna();
            ka.Dogadjaj = new Dogadjaj { IdDogadjaj = 0 };

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Dogadjaj), "Događaj karte je obavezan.");
        }

        [TestMethod]
        public void TestSektorObavezan()
        {
            Karta ka = Ispravna();
            ka.Sektor = "";

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Sektor), "Sektor karte je obavezan.");
        }

        [TestMethod]
        public void TestSektorDuzina()
        {
            Karta ka = Ispravna();
            ka.Sektor = new string('a', 51);

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Sektor), "Sektor karte može imati najviše 50 znakova.");
        }

        [TestMethod]
        public void TestRedObavezan()
        {
            Karta ka = Ispravna();
            ka.Red = "";

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Red), "Red karte je obavezan.");
        }

        [TestMethod]
        public void TestRedDuzina()
        {
            Karta ka = Ispravna();
            ka.Red = new string('1', 10);
            NemaGresaka(PravilaKarta.Proveri(ka, false));

            ka.Red = new string('1', 11);
            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Red), "Red karte može imati najviše 10 znakova.");
        }

        [TestMethod]
        public void TestSedisteObavezno()
        {
            Karta ka = Ispravna();
            ka.Sediste = "";

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Sediste), "Sedište karte je obavezno.");
        }

        [TestMethod]
        public void TestSedisteDuzina()
        {
            Karta ka = Ispravna();
            ka.Sediste = new string('1', 11);

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Sediste), "Sedište karte može imati najviše 10 znakova.");
        }

        [TestMethod]
        public void TestNominalnaCenaNegativna()
        {
            Karta ka = Ispravna();
            ka.NominalnaCena = -0.01m;

            JednaGreska(PravilaKarta.Proveri(ka, false),
                nameof(Karta.NominalnaCena), "Nominalna cena karte ne može biti negativna.");
        }

        [TestMethod]
        public void TestNominalnaCenaDecimale()
        {
            Karta ka = Ispravna();
            ka.NominalnaCena = 10.555m;

            JednaGreska(PravilaKarta.Proveri(ka, false),
                nameof(Karta.NominalnaCena), "Nominalna cena karte može imati najviše 2 decimale.");
        }

        [TestMethod]
        public void TestNominalnaCenaMaksimum()
        {
            Karta ka = Ispravna();
            ka.NominalnaCena = 99_999_999.99m;
            NemaGresaka(PravilaKarta.Proveri(ka, false));

            ka.NominalnaCena = 100_000_000m;
            JednaGreska(PravilaKarta.Proveri(ka, false),
                nameof(Karta.NominalnaCena), "Nominalna cena karte može biti najviše 99.999.999,99.");
        }

        [TestMethod]
        public void TestTipObavezan()
        {
            Karta ka = Ispravna();
            ka.Tip = null;

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Tip), "Tip karte je obavezan.");
        }

        [TestMethod]
        public void TestFormatObavezan()
        {
            Karta ka = Ispravna();
            ka.Format = null;

            JednaGreska(PravilaKarta.Proveri(ka, false), nameof(Karta.Format), "Format karte je obavezan.");
        }
    }
}
