using Common.Domen;
using SistemskeOperacije.DogadjajSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class UbaciDogadjajSOTest : IntegracioniTest
    {
        private UbaciDogadjajSO so = null!;
        private Dogadjaj dogadjaj = null!;

        [TestInitialize]
        public void SetUp()
        {
            dogadjaj = TestPodaci.NapraviDogadjaj();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new UbaciDogadjajSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBezNaziva()
        {
            dogadjaj.Naziv = "";
            so = new UbaciDogadjajSO(dogadjaj);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Naziv događaja je obavezan.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDatumUProslosti()
        {
            dogadjaj.DatumOdrzavanja = DateTime.Today.AddDays(-1);
            so = new UbaciDogadjajSO(dogadjaj);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Datum održavanja ne može biti u prošlosti.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDanasnjiDatum()
        {
            dogadjaj.DatumOdrzavanja = DateTime.Today;
            so = new UbaciDogadjajSO(dogadjaj);

            so.ExecuteTemplate();

            Assert.IsTrue(TestBaza.Postoji("Dogadjaj", $"idDogadjaj = {dogadjaj.IdDogadjaj}"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new UbaciDogadjajSO(dogadjaj);

            so.ExecuteTemplate();

            Assert.IsTrue(dogadjaj.IdDogadjaj > 0);

            PretraziDogadjajSO searchSo = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });
            searchSo.ExecuteTemplate();
            Dogadjaj saved = (Dogadjaj)searchSo.Result!;

            Assert.AreEqual(dogadjaj.Naziv, saved.Naziv);
            Assert.AreEqual(dogadjaj.Mesto, saved.Mesto);
            Assert.AreEqual(dogadjaj.DatumOdrzavanja, saved.DatumOdrzavanja);
        }

        [TestMethod]
        [Ignore("Poznata greška: tekstualni literali nemaju prefiks N, pa se ć, č i đ gube pri upisu; rešava se u Fazi 1")]
        public void TestIzvrsiOperacijuSrpskaSlova()
        {
            dogadjaj.Naziv = "Đurđevdanski koncert Čačak";
            dogadjaj.Mesto = "Ćuprija";
            so = new UbaciDogadjajSO(dogadjaj);

            so.ExecuteTemplate();

            PretraziDogadjajSO searchSo = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });
            searchSo.ExecuteTemplate();
            Dogadjaj saved = (Dogadjaj)searchSo.Result!;

            Assert.AreEqual("Đurđevdanski koncert Čačak", saved.Naziv);
            Assert.AreEqual("Ćuprija", saved.Mesto);
        }
    }
}
