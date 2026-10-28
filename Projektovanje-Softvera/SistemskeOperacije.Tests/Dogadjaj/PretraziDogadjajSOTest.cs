using Common.Domen;
using SistemskeOperacije.DogadjajSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PretraziDogadjajSOTest : IntegracioniTest
    {
        private PretraziDogadjajSO so = null!;
        private Dogadjaj dogadjaj = null!;

        [TestInitialize]
        public void SetUp()
        {
            dogadjaj = TestPodaci.KreirajDogadjaj();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PretraziDogadjajSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });

            so.ExecuteTemplate();

            Dogadjaj found = (Dogadjaj)so.Result!;
            Assert.AreEqual(dogadjaj.IdDogadjaj, found.IdDogadjaj);
            Assert.AreEqual(dogadjaj.Naziv, found.Naziv);
            Assert.AreEqual(dogadjaj.Mesto, found.Mesto);
            Assert.AreEqual(dogadjaj.DatumOdrzavanja, found.DatumOdrzavanja);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNepostojeciDogadjaj()
        {
            so = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = 999999 });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe događaj.", ex.Message);
        }
    }
}
