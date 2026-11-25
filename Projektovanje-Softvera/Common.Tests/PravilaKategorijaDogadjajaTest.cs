using Common.Domen;
using Common.Validacija;
using static Common.Tests.PomocPravila;

namespace Common.Tests
{
    [TestClass]
    public class PravilaKategorijaDogadjajaTest
    {
        private static KategorijaDogadjaja Ispravna() => new KategorijaDogadjaja
        {
            Naziv = "Koncert",
            Opis = "Muzički događaji"
        };

        [TestMethod]
        public void TestIspravnaKategorijaNemaGresaka()
        {
            NemaGresaka(PravilaKategorijaDogadjaja.Proveri(Ispravna()));
        }

        [TestMethod]
        public void TestNazivObavezan()
        {
            KategorijaDogadjaja kd = Ispravna();
            kd.Naziv = " ";

            JednaGreska(PravilaKategorijaDogadjaja.Proveri(kd),
                nameof(KategorijaDogadjaja.Naziv), "Naziv kategorije događaja je obavezan.");
        }

        [TestMethod]
        public void TestNazivDuzina()
        {
            KategorijaDogadjaja kd = Ispravna();
            kd.Naziv = new string('a', 100);
            NemaGresaka(PravilaKategorijaDogadjaja.Proveri(kd));

            kd.Naziv = new string('a', 101);
            JednaGreska(PravilaKategorijaDogadjaja.Proveri(kd),
                nameof(KategorijaDogadjaja.Naziv), "Naziv kategorije događaja može imati najviše 100 znakova.");
        }

        [TestMethod]
        public void TestOpisObavezan()
        {
            KategorijaDogadjaja kd = Ispravna();
            kd.Opis = "";

            JednaGreska(PravilaKategorijaDogadjaja.Proveri(kd),
                nameof(KategorijaDogadjaja.Opis), "Opis kategorije događaja je obavezan.");
        }

        [TestMethod]
        public void TestOpisDuzina()
        {
            KategorijaDogadjaja kd = Ispravna();
            kd.Opis = new string('a', 500);
            NemaGresaka(PravilaKategorijaDogadjaja.Proveri(kd));

            kd.Opis = new string('a', 501);
            JednaGreska(PravilaKategorijaDogadjaja.Proveri(kd),
                nameof(KategorijaDogadjaja.Opis), "Opis kategorije događaja može imati najviše 500 znakova.");
        }
    }
}
