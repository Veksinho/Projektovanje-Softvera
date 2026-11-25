using Common.Domen;
using SistemskeOperacije.KartaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PromeniKartaSOTest : IntegracioniTest
    {
        private Dogadjaj dogadjaj = null!;
        private FizickoLice konsignator = null!;

        private PromeniKartaSO so = null!;
        private Karta karta = null!;

        [TestInitialize]
        public void SetUp()
        {
            dogadjaj = TestPodaci.KreirajDogadjaj();
            konsignator = TestPodaci.KreirajFizickoLice();
            karta = Pretrazi(TestPodaci.KreirajKartu(dogadjaj, konsignator, "A", "1", "1").IdKarta);
        }

        private static Karta Pretrazi(int id)
        {
            PretraziKartaSO searchSo = new PretraziKartaSO(new Karta { IdKarta = id });
            searchSo.ExecuteTemplate();
            return (Karta)searchSo.Result!;
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PromeniKartaSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void TestPredusloviNeispravanId(int id)
        {
            karta.IdKarta = id;
            so = new PromeniKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nije prosleđen id karte.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviBezStatusa()
        {
            karta.Status = null;
            so = new PromeniKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Status karte je obavezan.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviNepostojecaKarta()
        {
            karta.IdKarta = 999999;
            so = new PromeniKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe kartu.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviPromenaKonsignatora()
        {
            karta.Konsignator = TestPodaci.KreirajFizickoLice();
            so = new PromeniKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator karte se ne može promeniti.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviPromenaDogadjaja()
        {
            karta.Dogadjaj = TestPodaci.KreirajDogadjaj();
            so = new PromeniKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Događaj karte se ne može promeniti.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviZauzetoSediste()
        {
            Karta druga = Pretrazi(TestPodaci.KreirajKartu(dogadjaj, konsignator, "A", "1", "2").IdKarta);
            druga.Sediste = "1";
            so = new PromeniKartaSO(druga);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Karta za to sedište na izabranom događaju već postoji.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviIstoSedisteSopstvenogZapisa()
        {
            karta.NominalnaCena = 7000m;
            so = new PromeniKartaSO(karta);

            so.ExecuteTemplate();

            Assert.AreEqual(7000m, Pretrazi(karta.IdKarta).NominalnaCena);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            karta.Sektor = "B";
            karta.NominalnaCena = 8500.75m;
            so = new PromeniKartaSO(karta);

            so.ExecuteTemplate();

            Karta saved = Pretrazi(karta.IdKarta);
            Assert.AreEqual("B", saved.Sektor);
            Assert.AreEqual(8500.75m, saved.NominalnaCena);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuZadrzavaListing()
        {
            Broker broker = TestPodaci.KreirajBrokera("lozinka123");
            Listing listing = TestPodaci.KreirajListing(broker, konsignator, new List<Karta> { karta });
            karta = Pretrazi(karta.IdKarta);
            karta.Listing = null;
            so = new PromeniKartaSO(karta);

            so.ExecuteTemplate();

            Karta saved = Pretrazi(karta.IdKarta);
            Assert.IsNotNull(saved.Listing);
            Assert.AreEqual(listing.IdListing, saved.Listing.IdListing);
        }
    }
}
