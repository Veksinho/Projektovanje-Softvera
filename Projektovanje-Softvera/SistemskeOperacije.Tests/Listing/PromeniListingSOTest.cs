using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.KartaSO;
using SistemskeOperacije.ListingSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PromeniListingSOTest : IntegracioniTest
    {
        private Broker broker = null!;
        private Dogadjaj dogadjaj = null!;
        private FizickoLice konsignator = null!;
        private Karta kartaA = null!;
        private Karta kartaB = null!;
        private Karta kartaC = null!;
        private int idListing;

        private PromeniListingSO so = null!;
        private Listing listing = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.KreirajBrokera("lozinka123");
            dogadjaj = TestPodaci.KreirajDogadjaj();
            konsignator = TestPodaci.KreirajFizickoLice();
            List<Karta> karte = TestPodaci.KreirajKarte(dogadjaj, konsignator, 3);
            kartaA = karte[0];
            kartaB = karte[1];
            kartaC = karte[2];
            idListing = TestPodaci.KreirajListing(broker, konsignator, new List<Karta> { kartaA, kartaB }).IdListing;
            listing = Pretrazi(idListing);
        }

        private static Listing Pretrazi(int id)
        {
            PretraziListingSO searchSo = new PretraziListingSO(new Listing { IdListing = id });
            searchSo.ExecuteTemplate();
            return (Listing)searchSo.Result!;
        }

        private static Karta PretraziKartu(int id)
        {
            PretraziKartaSO searchSo = new PretraziKartaSO(new Karta { IdKarta = id });
            searchSo.ExecuteTemplate();
            return (Karta)searchSo.Result!;
        }

        private static void PostaviDatumObjave(int id, DateTime datum)
        {
            TestBaza.Execute($"UPDATE Listing SET datumObjave = '{datum:yyyy-MM-dd}' WHERE idListing = {id}");
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PromeniListingSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void TestPredusloviNeispravanId(int id)
        {
            listing.IdListing = id;
            so = new PromeniListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nije prosleđen id listinga.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviNepostojeciListing()
        {
            listing.IdListing = 999999;
            so = new PromeniListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe listing.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviPromenaBrokera()
        {
            listing.Broker = TestPodaci.KreirajBrokera("lozinka123");
            so = new PromeniListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Broker listinga se ne može promeniti.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviPromenaKonsignatora()
        {
            listing.Konsignator = TestPodaci.KreirajFizickoLice();
            so = new PromeniListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator listinga se ne može promeniti.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviKartaSaDrugogListinga()
        {
            Karta kartaD = TestPodaci.KreirajKartu(dogadjaj, konsignator, "D");
            TestPodaci.KreirajListing(broker, konsignator, new List<Karta> { kartaD });
            listing.Karte.Add(new Karta { IdKarta = kartaD.IdKarta });
            so = new PromeniListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Karta se već nalazi na drugom listingu.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDatumIstekaPreObjave()
        {
            PostaviDatumObjave(idListing, DateTime.Today.AddDays(-3));
            listing = Pretrazi(idListing);
            listing.DatumIsteka = DateTime.Today.AddDays(-3);
            so = new PromeniListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Datum isteka listinga mora biti posle datuma objave.", ex.Message);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuOslobadjaIzbaceneKarte()
        {
            listing.Karte = listing.Karte.Where(k => k.IdKarta == kartaA.IdKarta).ToList();
            so = new PromeniListingSO(listing);

            so.ExecuteTemplate();

            Karta savedB = PretraziKartu(kartaB.IdKarta);
            Assert.IsNull(savedB.Listing);
            Assert.AreEqual(StatusKarte.u_inventaru, savedB.Status);

            Karta savedA = PretraziKartu(kartaA.IdKarta);
            Assert.IsNotNull(savedA.Listing);
            Assert.AreEqual(idListing, savedA.Listing.IdListing);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuDodajeNoveKarte()
        {
            listing.Karte.Add(new Karta { IdKarta = kartaC.IdKarta });
            so = new PromeniListingSO(listing);

            so.ExecuteTemplate();

            Karta savedC = PretraziKartu(kartaC.IdKarta);
            Assert.IsNotNull(savedC.Listing);
            Assert.AreEqual(idListing, savedC.Listing.IdListing);
            Assert.AreEqual(StatusKarte.plasirana, savedC.Status);
        }

        [TestMethod]
        [DataRow(StatusListinga.realizovan, StatusKarte.prodata)]
        [DataRow(StatusListinga.povucen, StatusKarte.povucena)]
        [DataRow(StatusListinga.objavljen, StatusKarte.plasirana)]
        public void TestIzvrsiOperacijuStatusKarata(StatusListinga statusListinga, StatusKarte statusKarte)
        {
            listing.Status = statusListinga;
            so = new PromeniListingSO(listing);

            so.ExecuteTemplate();

            Assert.AreEqual(statusKarte, PretraziKartu(kartaA.IdKarta).Status);
            Assert.AreEqual(statusKarte, PretraziKartu(kartaB.IdKarta).Status);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuDatumObjavePriObjavljivanju()
        {
            int idNacrt = TestPodaci.KreirajListing(broker, konsignator, new List<Karta> { kartaC }, StatusListinga.nacrt).IdListing;
            PostaviDatumObjave(idNacrt, DateTime.Today.AddDays(-3));
            listing = Pretrazi(idNacrt);
            listing.Status = StatusListinga.objavljen;
            so = new PromeniListingSO(listing);

            so.ExecuteTemplate();

            Assert.AreEqual(DateTime.Today, Pretrazi(idNacrt).DatumObjave.Date);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuDatumObjaveBezObjavljivanja()
        {
            PostaviDatumObjave(idListing, DateTime.Today.AddDays(-3));
            listing = Pretrazi(idListing);
            listing.CenaPoKarti = 6500m;
            so = new PromeniListingSO(listing);

            so.ExecuteTemplate();

            Assert.AreEqual(DateTime.Today.AddDays(-3), Pretrazi(idListing).DatumObjave.Date);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            listing.CenaPoKarti = 7250.50m;
            listing.Napomena = "Izmenjena napomena";
            so = new PromeniListingSO(listing);

            so.ExecuteTemplate();

            Listing saved = Pretrazi(idListing);
            Assert.AreEqual(7250.50m, saved.CenaPoKarti);
            Assert.AreEqual("Izmenjena napomena", saved.Napomena);
        }
    }
}
