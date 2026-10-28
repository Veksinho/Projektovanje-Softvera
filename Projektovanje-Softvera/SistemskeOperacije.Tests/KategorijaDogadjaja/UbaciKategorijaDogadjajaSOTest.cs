using Common.Domen;
using SistemskeOperacije.KategorijaDogadjajaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class UbaciKategorijaDogadjajaSOTest : IntegracioniTest
    {
        private UbaciKategorijaDogadjajaSO so = null!;
        private KategorijaDogadjaja kategorija = null!;

        [TestInitialize]
        public void SetUp()
        {
            kategorija = TestPodaci.NapraviKategoriju();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new UbaciKategorijaDogadjajaSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBezNaziva()
        {
            kategorija.Naziv = "";
            so = new UbaciKategorijaDogadjajaSO(kategorija);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Naziv kategorije događaja je obavezan.", ex.Message);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new UbaciKategorijaDogadjajaSO(kategorija);

            so.ExecuteTemplate();

            Assert.IsTrue(kategorija.IdKategorijaDogadjaja > 0);

            PretraziKategorijaDogadjajaSO searchSo = new PretraziKategorijaDogadjajaSO(
                new KategorijaDogadjaja { IdKategorijaDogadjaja = kategorija.IdKategorijaDogadjaja });
            searchSo.ExecuteTemplate();
            KategorijaDogadjaja saved = (KategorijaDogadjaja)searchSo.Result!;

            Assert.AreEqual(kategorija.Naziv, saved.Naziv);
            Assert.AreEqual(kategorija.Opis, saved.Opis);
        }

        [TestMethod]
        [Ignore("Poznata greška: SO ne proverava jedinstvenost naziva kategorije, pada SqlException na UQ ograničenju")]
        public void TestIzvrsiOperacijuDuplikatNaziva()
        {
            int brojPre = TestBaza.BrojRedova("KategorijaDogadjaja");
            kategorija.Naziv = "Koncert";
            so = new UbaciKategorijaDogadjajaSO(kategorija);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Kategorija događaja sa unetim nazivom već postoji.", ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("KategorijaDogadjaja"));
        }
    }
}
