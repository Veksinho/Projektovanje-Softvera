using Common.Domen;
using SistemskeOperacije.ListingSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PretraziListingSOTest : IntegracioniTest
    {
        private Broker broker = null!;
        private FizickoLice konsignator = null!;

        private PretraziListingSO so = null!;
        private Listing listing = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.KreirajBrokera("lozinka123");
            Dogadjaj dogadjaj = TestPodaci.KreirajDogadjaj();
            konsignator = TestPodaci.KreirajFizickoLice();
            listing = TestPodaci.KreirajListing(broker, konsignator, TestPodaci.KreirajKarte(dogadjaj, konsignator, 2));
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PretraziListingSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new PretraziListingSO(new Listing { IdListing = listing.IdListing });

            so.ExecuteTemplate();

            Listing found = (Listing)so.Result!;
            Assert.AreEqual(2, found.Karte.Count);
            Assert.IsNotNull(found.Broker);
            Assert.AreEqual(broker.IdBroker, found.Broker.IdBroker);
            Assert.IsInstanceOfType<FizickoLice>(found.Konsignator);
            Assert.AreEqual(konsignator.IdKonsignator, found.Konsignator!.IdKonsignator);
            Assert.IsTrue(found.Karte.All(k => k.Dogadjaj != null && k.Dogadjaj.IdDogadjaj > 0));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNepostojeciListing()
        {
            so = new PretraziListingSO(new Listing { IdListing = 999999 });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe listing.", ex.Message);
        }
    }
}
