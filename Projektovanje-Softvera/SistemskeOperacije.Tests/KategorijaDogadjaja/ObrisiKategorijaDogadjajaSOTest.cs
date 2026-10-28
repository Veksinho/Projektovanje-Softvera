using Common.Domen;
using SistemskeOperacije.KategorijaDogadjajaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class ObrisiKategorijaDogadjajaSOTest : IntegracioniTest
    {
        private ObrisiKategorijaDogadjajaSO so = null!;
        private KategorijaDogadjaja kategorija = null!;

        [TestInitialize]
        public void SetUp()
        {
            kategorija = TestPodaci.KreirajKategoriju();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new ObrisiKategorijaDogadjajaSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviKategorijaDodeljenaBrokeru()
        {
            TestPodaci.KreirajBrokera("lozinka123", kategorija.IdKategorijaDogadjaja);
            so = new ObrisiKategorijaDogadjajaSO(
                new KategorijaDogadjaja { IdKategorijaDogadjaja = kategorija.IdKategorijaDogadjaja });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Kategorija događaja je dodeljena bar jednom brokeru i ne može se obrisati.", ex.Message);
            Assert.IsTrue(TestBaza.Postoji("KategorijaDogadjaja",
                $"idKategorijaDogadjaja = {kategorija.IdKategorijaDogadjaja}"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new ObrisiKategorijaDogadjajaSO(
                new KategorijaDogadjaja { IdKategorijaDogadjaja = kategorija.IdKategorijaDogadjaja });

            so.ExecuteTemplate();

            Assert.IsFalse(TestBaza.Postoji("KategorijaDogadjaja",
                $"idKategorijaDogadjaja = {kategorija.IdKategorijaDogadjaja}"));
        }
    }
}
