using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.KartaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class KreirajKartaSOTest
    {
        private static Dogadjaj dogadjaj = null!;
        private static Konsignator konsignator = null!;

        private KreirajKartaSO so = null!;
        private Karta karta = null!;

        [ClassInitialize]
        public static void ClassSetUp(TestContext context)
        {
            konsignator = TestPodaci.VratiPostojecegKonsignatora();
            dogadjaj = TestPodaci.KreirajDogadjaj();
        }

        [ClassCleanup]
        public static void ClassTearDown()
        {
            TestPodaci.ObrisiDogadjaj(dogadjaj.IdDogadjaj);
        }

        [TestInitialize]
        public void SetUp()
        {
            karta = TestPodaci.NapraviKartu(dogadjaj, konsignator);
        }

        [TestCleanup]
        public void TearDown()
        {
            TestPodaci.ObrisiKarteDogadjaja(dogadjaj.IdDogadjaj);
            so = null!;
            karta = null!;
        }

        [TestMethod]
        public void TestPredusloviIspravan()
        {
            so = new KreirajKartaSO(karta);

            so.ExecuteTemplate();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new KreirajKartaSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBezKonsignatora()
        {
            karta.Konsignator = null;
            so = new KreirajKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator karte je obavezan.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviBezDogadjaja()
        {
            karta.Dogadjaj = null;
            so = new KreirajKartaSO(karta);

            Assert.Throws<Exception>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow(-0.01)]
        [DataRow(-1500.0)]
        public void TestPredusloviNegativnaCena(double price)
        {
            karta.NominalnaCena = (decimal)price;
            so = new KreirajKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nominalna cena karte ne može biti negativna.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviBezTipa()
        {
            karta.Tip = null;
            so = new KreirajKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Tip karte je obavezan.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviZauzetoSediste()
        {
            new KreirajKartaSO(karta).ExecuteTemplate();

            Karta duplicate = TestPodaci.NapraviKartu(dogadjaj, konsignator, karta.Sektor, karta.Red, karta.Sediste);
            so = new KreirajKartaSO(duplicate);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Karta za to sedište na izabranom događaju već postoji.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviStajaceKarteBezProvereSedista()
        {
            Karta first = TestPodaci.NapraviKartu(dogadjaj, konsignator, "-", "-", "-");
            Karta second = TestPodaci.NapraviKartu(dogadjaj, konsignator, "-", "-", "-");

            new KreirajKartaSO(first).ExecuteTemplate();
            new KreirajKartaSO(second).ExecuteTemplate();

            Assert.IsTrue(first.IdKarta > 0);
            Assert.IsTrue(second.IdKarta > 0);
            Assert.AreNotEqual(first.IdKarta, second.IdKarta);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new KreirajKartaSO(karta);

            so.ExecuteTemplate();

            Assert.IsNotNull(so.Result);
            Karta created = (Karta)so.Result;
            Assert.IsTrue(created.IdKarta > 0);

            PretraziKartaSO searchSo = new PretraziKartaSO(new Karta { IdKarta = created.IdKarta });
            searchSo.ExecuteTemplate();
            Karta saved = (Karta)searchSo.Result!;

            Assert.AreEqual(karta.Sektor, saved.Sektor);
            Assert.AreEqual(karta.NominalnaCena, saved.NominalnaCena);
            Assert.AreEqual(dogadjaj.IdDogadjaj, saved.Dogadjaj!.IdDogadjaj);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuServerPostavljaStatusIListing()
        {
            karta.Status = StatusKarte.plasirana;
            karta.Listing = new Listing { IdListing = 1 };
            so = new KreirajKartaSO(karta);

            so.ExecuteTemplate();

            PretraziKartaSO searchSo = new PretraziKartaSO(new Karta { IdKarta = karta.IdKarta });
            searchSo.ExecuteTemplate();
            Karta saved = (Karta)searchSo.Result!;

            Assert.AreEqual(StatusKarte.u_inventaru, saved.Status);
            Assert.IsNull(saved.Listing);
        }
    }
}
