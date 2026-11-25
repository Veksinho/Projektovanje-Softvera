using Common.Validacija;

namespace Common.Tests
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        [DataRow("pera@test.rs")]
        [DataRow("o'brien@test.rs")]
        [DataRow("pera.peric@mail.test.rs")]
        [DataRow("pera+prodaja@test.rs")]
        public void TestEmailIspravan(string email)
        {
            Assert.IsTrue(Provera.JeIspravanEmail(email));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("pera")]
        [DataRow("pera@")]
        [DataRow("@test.rs")]
        [DataRow("pera@@test.rs")]
        [DataRow("pera peric@test.rs")]
        [DataRow("Pera <pera@test.rs>")]
        [DataRow(" pera@test.rs")]
        [DataRow("pera@test.rs ")]
        public void TestEmailNeispravan(string? email)
        {
            Assert.IsFalse(Provera.JeIspravanEmail(email));
        }
    }
}
