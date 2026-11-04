using Common.Validacija;

namespace Common.Tests
{
    [TestClass]
    public class DuzinaTest
    {
        [TestMethod]
        [DataRow(Ogranicenja.BrokerKorisnickoIme - 1, Ogranicenja.BrokerKorisnickoIme, true)]
        [DataRow(Ogranicenja.BrokerKorisnickoIme, Ogranicenja.BrokerKorisnickoIme, true)]
        [DataRow(Ogranicenja.BrokerKorisnickoIme + 1, Ogranicenja.BrokerKorisnickoIme, false)]
        [DataRow(Ogranicenja.KartaRed - 1, Ogranicenja.KartaRed, true)]
        [DataRow(Ogranicenja.KartaRed, Ogranicenja.KartaRed, true)]
        [DataRow(Ogranicenja.KartaRed + 1, Ogranicenja.KartaRed, false)]
        [DataRow(Ogranicenja.KonsignatorAdresa - 1, Ogranicenja.KonsignatorAdresa, true)]
        [DataRow(Ogranicenja.KonsignatorAdresa, Ogranicenja.KonsignatorAdresa, true)]
        [DataRow(Ogranicenja.KonsignatorAdresa + 1, Ogranicenja.KonsignatorAdresa, false)]
        [DataRow(Ogranicenja.ListingNapomena - 1, Ogranicenja.ListingNapomena, true)]
        [DataRow(Ogranicenja.ListingNapomena, Ogranicenja.ListingNapomena, true)]
        [DataRow(Ogranicenja.ListingNapomena + 1, Ogranicenja.ListingNapomena, false)]
        public void TestUDuziniGranice(int duzina, int max, bool uDuzini)
        {
            Assert.AreEqual(uDuzini, Provera.UDuzini(new string('č', duzina), max));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void TestUDuziniPrazno(string? tekst)
        {
            Assert.IsTrue(Provera.UDuzini(tekst, Ogranicenja.BrokerIme));
        }
    }
}
