using Common.Domen;
using Common.Helpers;
using SistemskeOperacije.KategorijaDogadjajaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PromeniKategorijaDogadjajaSOTest : IntegracioniTest
    {
        private PromeniKategorijaDogadjajaSO so = null!;
        private KategorijaDogadjaja kategorija = null!;

        [TestInitialize]
        public void SetUp()
        {
            kategorija = TestPodaci.KreirajKategoriju();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PromeniKategorijaDogadjajaSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow("Koncert")]
        [DataRow("koncert")]
        public void TestPredusloviDuplikatNaziva(string naziv)
        {
            int brojPre = TestBaza.BrojRedova("KategorijaDogadjaja");
            kategorija.Naziv = naziv;
            so = new PromeniKategorijaDogadjajaSO(kategorija);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Kategorija događaja sa unetim nazivom već postoji.", ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("KategorijaDogadjaja"));
            Assert.AreEqual(1, TestBaza.BrojRedova("KategorijaDogadjaja", $"naziv = N'{Sql.Tekst(naziv)}'"));
        }

        [TestMethod]
        public void TestPredusloviIstiNazivSopstvenogZapisa()
        {
            kategorija.Opis = "Izmenjen opis";
            so = new PromeniKategorijaDogadjajaSO(kategorija);

            so.ExecuteTemplate();

            Assert.IsTrue(TestBaza.Postoji("KategorijaDogadjaja",
                $"idKategorijaDogadjaja = {kategorija.IdKategorijaDogadjaja} AND opis = N'Izmenjen opis'"));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void TestPredusloviNeispravanId(int id)
        {
            kategorija.IdKategorijaDogadjaja = id;
            so = new PromeniKategorijaDogadjajaSO(kategorija);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nije prosleđen id kategorije događaja.", ex.Message);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            kategorija.Opis = "Izmenjen opis";
            so = new PromeniKategorijaDogadjajaSO(kategorija);

            so.ExecuteTemplate();

            PretraziKategorijaDogadjajaSO searchSo = new PretraziKategorijaDogadjajaSO(
                new KategorijaDogadjaja { IdKategorijaDogadjaja = kategorija.IdKategorijaDogadjaja });
            searchSo.ExecuteTemplate();
            KategorijaDogadjaja saved = (KategorijaDogadjaja)searchSo.Result!;

            Assert.AreEqual("Izmenjen opis", saved.Opis);
        }
    }
}
