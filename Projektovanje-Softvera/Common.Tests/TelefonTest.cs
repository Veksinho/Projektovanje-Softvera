using Common.Validacija;

namespace Common.Tests
{
    [TestClass]
    public class TelefonTest
    {
        [TestMethod]
        [DataRow("+381641234567")]
        [DataRow("0641234567")]
        [DataRow("123456")]
        [DataRow("+123456")]
        [DataRow("123456789012345")]
        [DataRow("+123456789012345")]
        public void TestTelefonIspravan(string telefon)
        {
            Assert.IsTrue(Provera.JeIspravanTelefon(telefon));
        }

        [TestMethod]
        [DataRow("064 123 4567")]
        [DataRow("064-1234567")]
        [DataRow("064/1234567")]
        [DataRow("06+41234567")]
        [DataRow("0641234567+")]
        [DataRow("+")]
        [DataRow("++381641234567")]
        [DataRow("12345")]
        [DataRow("+12345")]
        [DataRow("1234567890123456")]
        [DataRow("+1234567890123456")]
        [DataRow(" 0641234567")]
        [DataRow("064123456a")]
        [DataRow("٠٦٤١٢٣٤٥٦٧")]
        [DataRow("")]
        [DataRow(null)]
        public void TestTelefonNeispravan(string? telefon)
        {
            Assert.IsFalse(Provera.JeIspravanTelefon(telefon));
        }
    }
}
