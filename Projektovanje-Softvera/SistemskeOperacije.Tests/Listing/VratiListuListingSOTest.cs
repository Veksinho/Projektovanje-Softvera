using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.ListingSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuListingSOTest : IntegracioniTest
    {
        private Dogadjaj d2 = null!;
        private FizickoLice konsignatorL2 = null!;
        private Listing l1 = null!;
        private Listing l2 = null!;

        private VratiListuListingSO so = null!;

        [TestInitialize]
        public void SetUp()
        {
            Broker broker = TestPodaci.KreirajBrokera("lozinka123");
            Dogadjaj d1 = TestPodaci.KreirajDogadjaj();
            d2 = TestPodaci.KreirajDogadjaj();
            FizickoLice konsignatorL1 = TestPodaci.KreirajFizickoLice();
            konsignatorL2 = TestPodaci.KreirajFizickoLice();

            List<Karta> karteL1 = new List<Karta>
            {
                TestPodaci.KreirajKartu(d1, konsignatorL1, "Parter", "1", "1"),
                TestPodaci.KreirajKartu(d1, konsignatorL1, "Parter", "1", "2"),
                TestPodaci.KreirajKartu(d1, konsignatorL1, "Tribina", "1", "1")
            };
            l1 = TestPodaci.NapraviListing(broker, konsignatorL1, karteL1, StatusListinga.objavljen);
            l1.CenaPoKarti = 5000m;
            new KreirajListingSO(l1).ExecuteTemplate();

            List<Karta> karteL2 = new List<Karta>
            {
                TestPodaci.KreirajKartu(d2, konsignatorL2, "Tribina", "1", "1")
            };
            l2 = TestPodaci.NapraviListing(broker, konsignatorL2, karteL2, StatusListinga.nacrt);
            l2.CenaPoKarti = 9000m;
            new KreirajListingSO(l2).ExecuteTemplate();
        }

        private List<Listing> Izvrsi(Listing kriterijum)
        {
            so = new VratiListuListingSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Listing>)so.Result!;
        }

        [TestMethod]
        public void TestPredusloviPrazanKriterijum()
        {
            so = new VratiListuListingSO(new Listing());

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoStatusu()
        {
            List<Listing> result = Izvrsi(new Listing { Status = StatusListinga.objavljen });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(l1.IdListing, result[0].IdListing);
            Assert.AreEqual(3, result[0].Karte.Count);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoRasponuCene()
        {
            List<Listing> result = Izvrsi(new Listing { CenaOd = 6000m, CenaDo = 10000m });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(l2.IdListing, result[0].IdListing);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoRasponuDatumaObjave()
        {
            TestBaza.Execute($"UPDATE Listing SET datumObjave = '{DateTime.Today.AddDays(-10):yyyy-MM-dd}' WHERE idListing = {l2.IdListing}");

            List<Listing> result = Izvrsi(new Listing { DatumObjaveOd = DateTime.Today });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(l1.IdListing, result[0].IdListing);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoKonsignatoru()
        {
            List<Listing> result = Izvrsi(new Listing
            {
                Konsignator = new Konsignator { IdKonsignator = konsignatorL2.IdKonsignator }
            });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(l2.IdListing, result[0].IdListing);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoSektoruKarte()
        {
            List<Listing> result = Izvrsi(new Listing { KriterijumKarta = new Karta { Sektor = "Parter" } });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(l1.IdListing, result[0].IdListing);
            Assert.AreEqual(3, result[0].Karte.Count);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoDogadjaju()
        {
            List<Listing> result = Izvrsi(new Listing { KriterijumDogadjaj = new Dogadjaj { IdDogadjaj = d2.IdDogadjaj } });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(l2.IdListing, result[0].IdListing);
        }
    }
}
