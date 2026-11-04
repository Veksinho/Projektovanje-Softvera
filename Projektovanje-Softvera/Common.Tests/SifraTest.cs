using Common.Validacija;

namespace Common.Tests
{
    [TestClass]
    public class SifraTest
    {
        [TestMethod]
        [DataRow("lozinka1")]
        [DataRow("Šifra123")]
        [DataRow("čćžšđ123")]
        [DataRow("1234567a")]
        [DataRow("lozinka 1")]
        [DataRow("loz'inka1")]
        public void TestSifraIspravna(string sifra)
        {
            Assert.IsTrue(Provera.JeIspravnaSifra(sifra));
        }

        [TestMethod]
        [DataRow("lozinkaa")]
        [DataRow("12345678")]
        [DataRow("loz1")]
        [DataRow("!@#$%^&*")]
        [DataRow("lozinka١")]
        [DataRow("")]
        [DataRow(null)]
        public void TestSifraNeispravna(string? sifra)
        {
            Assert.IsFalse(Provera.JeIspravnaSifra(sifra));
        }

        [TestMethod]
        [DataRow(Ogranicenja.SifraMin - 1, false)]
        [DataRow(Ogranicenja.SifraMin, true)]
        [DataRow(Ogranicenja.SifraMin + 1, true)]
        [DataRow(Ogranicenja.SifraMax - 1, true)]
        [DataRow(Ogranicenja.SifraMax, true)]
        [DataRow(Ogranicenja.SifraMax + 1, false)]
        public void TestSifraGraniceDuzine(int duzina, bool ispravna)
        {
            string sifra = "a1" + new string('x', duzina - 2);

            Assert.AreEqual(ispravna, Provera.JeIspravnaSifra(sifra));
        }
    }
}
