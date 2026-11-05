using Common.Domen;
using Common.Validacija;
using static Common.Tests.PomocPravila;

namespace Common.Tests
{
    [TestClass]
    public class PravilaDogadjajTest
    {
        private static Dogadjaj Ispravan() => new Dogadjaj
        {
            Naziv = "Koncert",
            Mesto = "Beogradska arena",
            DatumOdrzavanja = DateTime.Today.AddDays(30)
        };

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestIspravanDogadjajNemaGresaka(bool novi)
        {
            NemaGresaka(PravilaDogadjaj.Proveri(Ispravan(), novi));
        }

        [TestMethod]
        public void TestNoviDogadjajSaJucerasnjimDatumom()
        {
            Dogadjaj d = Ispravan();
            d.DatumOdrzavanja = DateTime.Today.AddDays(-1);

            JednaGreska(PravilaDogadjaj.Proveri(d, true),
                nameof(Dogadjaj.DatumOdrzavanja), "Datum održavanja ne može biti u prošlosti.");
        }

        [TestMethod]
        public void TestNoviDogadjajSaDanasnjimDatumom()
        {
            Dogadjaj d = Ispravan();
            d.DatumOdrzavanja = DateTime.Today;

            NemaGresaka(PravilaDogadjaj.Proveri(d, true));
        }

        [TestMethod]
        public void TestIzmenaDogadjajaSaJucerasnjimDatumom()
        {
            Dogadjaj d = Ispravan();
            d.DatumOdrzavanja = DateTime.Today.AddDays(-1);

            NemaGresaka(PravilaDogadjaj.Proveri(d, false));
        }

        [TestMethod]
        public void TestNazivObavezan()
        {
            Dogadjaj d = Ispravan();
            d.Naziv = "";

            JednaGreska(PravilaDogadjaj.Proveri(d, true), nameof(Dogadjaj.Naziv), "Naziv događaja je obavezan.");
        }

        [TestMethod]
        public void TestNazivDuzina()
        {
            Dogadjaj d = Ispravan();
            d.Naziv = new string('a', 150);
            NemaGresaka(PravilaDogadjaj.Proveri(d, true));

            d.Naziv = new string('a', 151);
            JednaGreska(PravilaDogadjaj.Proveri(d, true),
                nameof(Dogadjaj.Naziv), "Naziv događaja može imati najviše 150 znakova.");
        }

        [TestMethod]
        public void TestMestoObavezno()
        {
            Dogadjaj d = Ispravan();
            d.Mesto = "";

            JednaGreska(PravilaDogadjaj.Proveri(d, true),
                nameof(Dogadjaj.Mesto), "Mesto održavanja događaja je obavezno.");
        }

        [TestMethod]
        public void TestMestoDuzina()
        {
            Dogadjaj d = Ispravan();
            d.Mesto = new string('a', 151);

            JednaGreska(PravilaDogadjaj.Proveri(d, true),
                nameof(Dogadjaj.Mesto), "Mesto održavanja događaja može imati najviše 150 znakova.");
        }

        [TestMethod]
        public void TestDatumOdrzavanjaObavezan()
        {
            Dogadjaj d = Ispravan();
            d.DatumOdrzavanja = default;

            JednaGreska(PravilaDogadjaj.Proveri(d, true),
                nameof(Dogadjaj.DatumOdrzavanja), "Datum održavanja događaja je obavezan.");
        }
    }
}
