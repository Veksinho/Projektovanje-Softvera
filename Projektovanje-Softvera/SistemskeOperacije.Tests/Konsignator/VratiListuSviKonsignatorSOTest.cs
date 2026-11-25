using Common.Domen;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuSviKonsignatorSOTest : IntegracioniTest
    {
        private VratiListuSviKonsignatorSO so = null!;
        private FizickoLice fizickoLice = null!;
        private PravnoLice pravnoLice = null!;

        [TestInitialize]
        public void SetUp()
        {
            fizickoLice = TestPodaci.KreirajFizickoLice();
            pravnoLice = TestPodaci.KreirajPravnoLice();
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new VratiListuSviKonsignatorSO();

            so.ExecuteTemplate();

            List<Konsignator> result = (List<Konsignator>)so.Result!;
            Assert.AreEqual(2, result.Count);
            Assert.IsInstanceOfType<FizickoLice>(result.Single(k => k.IdKonsignator == fizickoLice.IdKonsignator));
            Assert.IsInstanceOfType<PravnoLice>(result.Single(k => k.IdKonsignator == pravnoLice.IdKonsignator));
        }
    }
}
