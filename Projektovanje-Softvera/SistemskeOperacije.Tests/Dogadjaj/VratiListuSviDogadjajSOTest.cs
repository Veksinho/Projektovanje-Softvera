using Common.Domen;
using SistemskeOperacije.DogadjajSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuSviDogadjajSOTest : IntegracioniTest
    {
        private VratiListuSviDogadjajSO so = null!;

        [TestInitialize]
        public void SetUp()
        {
            TestPodaci.KreirajDogadjaj();
            TestPodaci.KreirajDogadjaj();
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new VratiListuSviDogadjajSO();

            so.ExecuteTemplate();

            Assert.AreEqual(2, ((List<Dogadjaj>)so.Result!).Count);
        }
    }
}
