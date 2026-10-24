using Common.Domen;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KartaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class ObrisiDogadjajSOTest : IntegracioniTest
    {
        private Konsignator konsignator = null!;

        private ObrisiDogadjajSO so = null!;
        private Dogadjaj dogadjaj = null!;

        [TestInitialize]
        public void SetUp()
        {
            konsignator = TestPodaci.KreirajFizickoLice();
            dogadjaj = TestPodaci.KreirajDogadjaj();
        }

        [TestMethod]
        public void TestPredusloviValidan()
        {
            so = new ObrisiDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });

            so.ExecuteTemplate();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new ObrisiDogadjajSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-5)]
        public void TestPredusloviNeispravanId(int id)
        {
            so = new ObrisiDogadjajSO(new Dogadjaj { IdDogadjaj = id });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nije prosleđen id događaja.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDogadjajSaKartama()
        {
            new KreirajKartaSO(TestPodaci.NapraviKartu(dogadjaj, konsignator)).ExecuteTemplate();
            so = new ObrisiDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Za događaj postoje unete karte i ne može se obrisati.", ex.Message);

            PretraziDogadjajSO searchSo = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });
            searchSo.ExecuteTemplate();
            Assert.IsNotNull(searchSo.Result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new ObrisiDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });

            so.ExecuteTemplate();

            PretraziDogadjajSO searchSo = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });
            Exception ex = Assert.Throws<Exception>(() => searchSo.ExecuteTemplate());
            Assert.AreEqual("Sistem ne može da nađe događaj.", ex.Message);
        }
    }
}
