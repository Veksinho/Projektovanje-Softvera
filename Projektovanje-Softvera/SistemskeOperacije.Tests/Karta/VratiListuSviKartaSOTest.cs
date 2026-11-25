using Common.Domen;
using SistemskeOperacije.KartaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuSviKartaSOTest : IntegracioniTest
    {
        private VratiListuSviKartaSO so = null!;

        [TestInitialize]
        public void SetUp()
        {
            TestPodaci.KreirajKarte(TestPodaci.KreirajDogadjaj(), TestPodaci.KreirajFizickoLice(), 3);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new VratiListuSviKartaSO();

            so.ExecuteTemplate();

            Assert.AreEqual(3, ((List<Karta>)so.Result!).Count);
        }
    }
}
