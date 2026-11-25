using Common.Domen;
using SistemskeOperacije.KategorijaDogadjajaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuSviKategorijaDogadjajaSOTest : IntegracioniTest
    {
        private VratiListuSviKategorijaDogadjajaSO so = null!;

        [TestInitialize]
        public void SetUp()
        {
            TestPodaci.KreirajKategoriju();
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new VratiListuSviKategorijaDogadjajaSO();

            so.ExecuteTemplate();

            List<KategorijaDogadjaja> result = (List<KategorijaDogadjaja>)so.Result!;
            Assert.AreEqual(TestBaza.BrojRedova("KategorijaDogadjaja"), result.Count);
            Assert.AreEqual(4, result.Count);
        }
    }
}
