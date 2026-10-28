using Common.Domen;
using SistemskeOperacije.DogadjajSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PromeniDogadjajSOTest : IntegracioniTest
    {
        private PromeniDogadjajSO so = null!;
        private Dogadjaj dogadjaj = null!;

        [TestInitialize]
        public void SetUp()
        {
            dogadjaj = TestPodaci.KreirajDogadjaj();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PromeniDogadjajSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void TestPredusloviNeispravanId(int id)
        {
            dogadjaj.IdDogadjaj = id;
            so = new PromeniDogadjajSO(dogadjaj);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nije prosleđen id događaja.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDatumUProslostiDozvoljen()
        {
            dogadjaj.DatumOdrzavanja = DateTime.Today.AddDays(-10);
            so = new PromeniDogadjajSO(dogadjaj);

            so.ExecuteTemplate();

            PretraziDogadjajSO searchSo = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });
            searchSo.ExecuteTemplate();
            Assert.AreEqual(DateTime.Today.AddDays(-10), ((Dogadjaj)searchSo.Result!).DatumOdrzavanja);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            dogadjaj.Naziv = "Izmenjen naziv";
            dogadjaj.Mesto = "Novi Sad";
            so = new PromeniDogadjajSO(dogadjaj);

            so.ExecuteTemplate();

            PretraziDogadjajSO searchSo = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = dogadjaj.IdDogadjaj });
            searchSo.ExecuteTemplate();
            Dogadjaj saved = (Dogadjaj)searchSo.Result!;

            Assert.AreEqual("Izmenjen naziv", saved.Naziv);
            Assert.AreEqual("Novi Sad", saved.Mesto);
        }
    }
}
