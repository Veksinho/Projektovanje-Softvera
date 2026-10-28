using Common.Domen;
using SistemskeOperacije.KartaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PretraziKartaSOTest : IntegracioniTest
    {
        private Dogadjaj dogadjaj = null!;
        private FizickoLice konsignator = null!;

        private PretraziKartaSO so = null!;
        private Karta karta = null!;

        [TestInitialize]
        public void SetUp()
        {
            dogadjaj = TestPodaci.KreirajDogadjaj();
            konsignator = TestPodaci.KreirajFizickoLice();
            karta = TestPodaci.KreirajKartu(dogadjaj, konsignator);
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PretraziKartaSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new PretraziKartaSO(new Karta { IdKarta = karta.IdKarta });

            so.ExecuteTemplate();

            Karta found = (Karta)so.Result!;
            Assert.AreEqual(karta.IdKarta, found.IdKarta);
            Assert.IsNotNull(found.Dogadjaj);
            Assert.AreEqual(dogadjaj.Naziv, found.Dogadjaj.Naziv);
            Assert.IsInstanceOfType<FizickoLice>(found.Konsignator);
            Assert.AreEqual(konsignator.IdKonsignator, found.Konsignator!.IdKonsignator);
            Assert.IsNull(found.Listing);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuKartaNaListingu()
        {
            Broker broker = TestPodaci.KreirajBrokera("lozinka123");
            Listing listing = TestPodaci.KreirajListing(broker, konsignator, new List<Karta> { karta });
            so = new PretraziKartaSO(new Karta { IdKarta = karta.IdKarta });

            so.ExecuteTemplate();

            Karta found = (Karta)so.Result!;
            Assert.IsNotNull(found.Listing);
            Assert.AreEqual(listing.IdListing, found.Listing.IdListing);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNepostojecaKarta()
        {
            so = new PretraziKartaSO(new Karta { IdKarta = 999999 });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe kartu.", ex.Message);
        }
    }
}
