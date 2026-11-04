using Common.Validacija;

namespace Common.Tests
{
    [TestClass]
    public class OgranicenjaTest
    {
        [TestMethod]
        [DataRow(Ogranicenja.TelefonMaxCifara + 1, Ogranicenja.TelefonMaxLength)]
        [DataRow(Ogranicenja.FizickoLiceJmbg, Ogranicenja.JmbgDuzina)]
        [DataRow(Ogranicenja.PravnoLicePib, Ogranicenja.PibDuzina)]
        [DataRow(Ogranicenja.PravnoLiceMaticniBroj, Ogranicenja.MaticniBrojDuzina)]
        public void TestPraviloJednakoOcekivanom(int ocekivano, int pravilo)
        {
            Assert.AreEqual(ocekivano, pravilo);
        }

        [TestMethod]
        [DataRow("Broker.telefon", Ogranicenja.TelefonMaxLength, Ogranicenja.BrokerTelefon)]
        [DataRow("Konsignator.telefon", Ogranicenja.TelefonMaxLength, Ogranicenja.KonsignatorTelefon)]
        [DataRow("FizickoLice.brojLicneKarte", Ogranicenja.BrojLicneKarteDuzina, Ogranicenja.FizickoLiceBrojLicneKarte)]
        [DataRow("Broker.sifra", Ogranicenja.SifraMax, Ogranicenja.BrokerSifra)]
        public void TestPraviloStajeUKolonu(string kolonaNaziv, int pravilo, int kolona)
        {
            Assert.IsLessThanOrEqualTo(kolona, pravilo, kolonaNaziv);
        }

        [TestMethod]
        [DataRow(Ogranicenja.FizickoLiceIme, Ogranicenja.FizickoLicePrezime, Ogranicenja.PravnoLiceNaziv,
            Ogranicenja.NazivKonsignatoraPretragaMaxLength)]
        public void TestNazivKonsignatoraPretragaJeNajduzaKolona(int ime, int prezime, int naziv, int maxLength)
        {
            Assert.AreEqual(Math.Max(naziv, Math.Max(ime, prezime)), maxLength);
        }
    }
}
