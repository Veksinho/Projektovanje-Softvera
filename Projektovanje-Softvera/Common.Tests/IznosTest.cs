using Common.Validacija;
using System.Globalization;

namespace Common.Tests
{
    [TestClass]
    public class IznosTest
    {
        private static decimal D(string vrednost) => decimal.Parse(vrednost, CultureInfo.InvariantCulture);

        [TestMethod]
        [DataRow("100", true)]
        [DataRow("100.5", true)]
        [DataRow("100.55", true)]
        [DataRow("100.555", false)]
        [DataRow("100.500", true)]
        [DataRow("0.001", false)]
        [DataRow("99999999.99", true)]
        public void TestImaNajviseDecimala(string vrednost, bool ispravno)
        {
            Assert.AreEqual(ispravno, Provera.ImaNajviseDecimala(D(vrednost), Ogranicenja.BrojDecimala));
        }

        [TestMethod]
        [DataRow("0", true)]
        [DataRow("-0.01", false)]
        [DataRow("0.01", true)]
        [DataRow("99999999.98", true)]
        [DataRow("99999999.99", true)]
        [DataRow("100000000", false)]
        public void TestIznosUOpsegu(string vrednost, bool uOpsegu)
        {
            Assert.AreEqual(uOpsegu, Provera.JeUOpsegu(D(vrednost), 0m, Ogranicenja.MaxIznos));
        }

        [TestMethod]
        [DataRow("-0.01", false)]
        [DataRow("0", true)]
        [DataRow("99.99", true)]
        [DataRow("100", true)]
        [DataRow("100.01", false)]
        public void TestProvizijaUOpsegu(string vrednost, bool uOpsegu)
        {
            Assert.AreEqual(uOpsegu, Provera.JeUOpsegu(D(vrednost), 0m, Ogranicenja.MaxProvizija));
        }
    }
}
