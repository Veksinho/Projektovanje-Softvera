using SistemskeOperacije.Bezbednost;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PasswordHasherTest
    {
        [TestMethod]
        public void TestHashFormat()
        {
            string sifra = "lozinka123";

            string hes = PasswordHasher.Hash(sifra);

            Assert.AreNotEqual(sifra, hes);
            Assert.AreEqual(60, hes.Length);
            Assert.StartsWith("$2", hes);
            Assert.AreEqual("$04$", hes.Substring(3, 4));
        }

        [TestMethod]
        public void TestHashRazliciteSoli()
        {
            string prvi = PasswordHasher.Hash("lozinka123");
            string drugi = PasswordHasher.Hash("lozinka123");

            Assert.AreNotEqual(prvi, drugi);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void TestHashPraznaSifra(string? sifra)
        {
            Assert.ThrowsExactly<ArgumentException>(() => PasswordHasher.Hash(sifra!));
        }

        [TestMethod]
        public void TestVerifyIspravnaIPogresnaSifra()
        {
            string hes = PasswordHasher.Hash("lozinka123");

            Assert.IsTrue(PasswordHasher.Verify("lozinka123", hes));
            Assert.IsFalse(PasswordHasher.Verify("lozinka124", hes));
        }

        [TestMethod]
        public void TestVerifyPosebniZnaci()
        {
            string sifra = "čćžšđ'ČĆŽŠĐ1";
            string hes = PasswordHasher.Hash(sifra);

            Assert.IsTrue(PasswordHasher.Verify(sifra, hes));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void TestVerifyPrazanHes(string? hes)
        {
            Assert.IsFalse(PasswordHasher.Verify("lozinka123", hes));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void TestVerifyPraznaSifra(string? sifra)
        {
            string hes = PasswordHasher.Hash("lozinka123");

            Assert.IsFalse(PasswordHasher.Verify(sifra, hes));
        }

        [TestMethod]
        public void TestVerifyVrednostKojaNijeHes()
        {
            Assert.IsFalse(PasswordHasher.Verify("Sifra#2026", "Sifra#2026"));
        }
    }
}
