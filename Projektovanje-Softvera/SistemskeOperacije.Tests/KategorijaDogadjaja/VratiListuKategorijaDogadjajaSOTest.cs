using Common.Domen;
using SistemskeOperacije.KategorijaDogadjajaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuKategorijaDogadjajaSOTest : IntegracioniTest
    {
        private VratiListuKategorijaDogadjajaSO so = null!;

        [TestMethod]
        public void TestPredusloviPrazanKriterijum()
        {
            so = new VratiListuKategorijaDogadjajaSO(new KategorijaDogadjaja());

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoNazivu()
        {
            so = new VratiListuKategorijaDogadjajaSO(new KategorijaDogadjaja { Naziv = "Sport" });

            so.ExecuteTemplate();

            List<KategorijaDogadjaja> result = (List<KategorijaDogadjaja>)so.Result!;
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Sportski događaj", result[0].Naziv);
        }
    }
}
