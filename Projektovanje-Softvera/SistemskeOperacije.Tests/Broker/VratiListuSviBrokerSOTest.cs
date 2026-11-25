using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuSviBrokerSOTest : IntegracioniTest
    {
        private VratiListuSviBrokerSO so = null!;

        [TestInitialize]
        public void SetUp()
        {
            TestPodaci.KreirajBrokera("lozinka123");
            TestPodaci.KreirajBrokera("lozinka123", 1, 2);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new VratiListuSviBrokerSO();

            so.ExecuteTemplate();

            Assert.AreEqual(3, ((List<Broker>)so.Result!).Count);
        }
    }
}
