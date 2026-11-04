using Common.Validacija;

namespace Common.Tests
{
    [TestClass]
    public class SamoCifreTest
    {
        [TestMethod]
        [DataRow("0101990710006")]
        [DataRow("0000000000000")]
        [DataRow("9999999999999")]
        public void TestJmbgIspravan(string jmbg)
        {
            Assert.IsTrue(Provera.SamoCifre(jmbg, Ogranicenja.JmbgDuzina));
        }

        [TestMethod]
        [DataRow("010199071000A")]
        [DataRow("0101990 710006")]
        [DataRow("010199071000 ")]
        [DataRow("010199071000")]
        [DataRow("01019907100061")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("-101990710006")]
        [DataRow("+101990710006")]
        [DataRow("١٢٣٤٥٦٧٨٩٠١٢٣")]
        [DataRow("０１０１９９０７１０００６")]
        public void TestJmbgNeispravan(string? jmbg)
        {
            Assert.IsFalse(Provera.SamoCifre(jmbg, Ogranicenja.JmbgDuzina));
        }

        [TestMethod]
        [DataRow("12345678", false)]
        [DataRow("123456789", true)]
        [DataRow("1234567890", false)]
        [DataRow("12345678A", false)]
        public void TestPib(string pib, bool ispravan)
        {
            Assert.AreEqual(ispravan, Provera.SamoCifre(pib, Ogranicenja.PibDuzina));
        }

        [TestMethod]
        [DataRow("1234567", false)]
        [DataRow("12345678", true)]
        [DataRow("123456789", false)]
        [DataRow("1234 678", false)]
        public void TestMaticniBroj(string maticniBroj, bool ispravan)
        {
            Assert.AreEqual(ispravan, Provera.SamoCifre(maticniBroj, Ogranicenja.MaticniBrojDuzina));
        }

        [TestMethod]
        [DataRow("12345678", false)]
        [DataRow("123456789", true)]
        [DataRow("1234567890", false)]
        [DataRow("12345678-", false)]
        public void TestBrojLicneKarte(string brojLicneKarte, bool ispravan)
        {
            Assert.AreEqual(ispravan, Provera.SamoCifre(brojLicneKarte, Ogranicenja.BrojLicneKarteDuzina));
        }
    }
}
