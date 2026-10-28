using Common.Domen;
using SistemskeOperacije.KategorijaDogadjajaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PretraziKategorijaDogadjajaSOTest : IntegracioniTest
    {
        private PretraziKategorijaDogadjajaSO so = null!;
        private KategorijaDogadjaja kategorija = null!;

        [TestInitialize]
        public void SetUp()
        {
            kategorija = TestPodaci.KreirajKategoriju();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PretraziKategorijaDogadjajaSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new PretraziKategorijaDogadjajaSO(
                new KategorijaDogadjaja { IdKategorijaDogadjaja = kategorija.IdKategorijaDogadjaja });

            so.ExecuteTemplate();

            KategorijaDogadjaja found = (KategorijaDogadjaja)so.Result!;
            Assert.AreEqual(kategorija.IdKategorijaDogadjaja, found.IdKategorijaDogadjaja);
            Assert.AreEqual(kategorija.Naziv, found.Naziv);
            Assert.AreEqual(kategorija.Opis, found.Opis);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNepostojecaKategorija()
        {
            so = new PretraziKategorijaDogadjajaSO(new KategorijaDogadjaja { IdKategorijaDogadjaja = 999999 });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe kategoriju događaja.", ex.Message);
        }
    }
}
