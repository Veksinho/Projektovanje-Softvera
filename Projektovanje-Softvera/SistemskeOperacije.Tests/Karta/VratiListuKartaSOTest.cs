using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KartaSO;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuKartaSOTest : IntegracioniTest
    {
        private PravnoLice pravnoLice = null!;
        private Karta k1 = null!;
        private Karta k2 = null!;
        private Karta k3 = null!;

        private VratiListuKartaSO so = null!;

        [TestInitialize]
        public void SetUp()
        {
            Dogadjaj d1 = TestPodaci.NapraviDogadjaj();
            d1.Mesto = "Novi Sad";
            new UbaciDogadjajSO(d1).ExecuteTemplate();

            Dogadjaj d2 = TestPodaci.NapraviDogadjaj();
            d2.Mesto = "Beograd";
            new UbaciDogadjajSO(d2).ExecuteTemplate();

            FizickoLice fizickoLice = TestPodaci.NapraviFizickoLice();
            fizickoLice.Prezime = "Jovanović";
            new KreirajKonsignatorSO(fizickoLice).ExecuteTemplate();

            pravnoLice = TestPodaci.KreirajPravnoLice();

            k1 = Kreiraj(d1, fizickoLice, "Parter", TipKarte.stajaca);
            k2 = Kreiraj(d1, fizickoLice, "Tribina", TipKarte.sedeca);
            k3 = Kreiraj(d2, pravnoLice, "Parter", TipKarte.vip);

            TestPodaci.KreirajListing(TestPodaci.KreirajBrokera("lozinka123"), fizickoLice, new List<Karta> { k1 });
        }

        private static Karta Kreiraj(Dogadjaj dogadjaj, Konsignator konsignator, string sektor, TipKarte tip)
        {
            Karta karta = TestPodaci.NapraviKartu(dogadjaj, konsignator, sektor);
            karta.Tip = tip;
            new KreirajKartaSO(karta).ExecuteTemplate();
            return karta;
        }

        private List<int> Izvrsi(Karta kriterijum)
        {
            so = new VratiListuKartaSO(kriterijum);
            so.ExecuteTemplate();
            return ((List<Karta>)so.Result!).Select(k => k.IdKarta).ToList();
        }

        [TestMethod]
        public void TestPredusloviPrazanKriterijum()
        {
            so = new VratiListuKartaSO(new Karta());

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoSektoru()
        {
            List<int> result = Izvrsi(new Karta { Sektor = "Part" });

            CollectionAssert.AreEquivalent(new[] { k1.IdKarta, k3.IdKarta }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoTipu()
        {
            List<int> result = Izvrsi(new Karta { Tip = TipKarte.vip });

            CollectionAssert.AreEquivalent(new[] { k3.IdKarta }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoStatusu()
        {
            List<int> result = Izvrsi(new Karta { Status = StatusKarte.plasirana });

            CollectionAssert.AreEquivalent(new[] { k1.IdKarta }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoKonsignatoru()
        {
            List<int> result = Izvrsi(new Karta { Konsignator = new Konsignator { IdKonsignator = pravnoLice.IdKonsignator } });

            CollectionAssert.AreEquivalent(new[] { k3.IdKarta }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoNazivuKonsignatora()
        {
            List<int> result = Izvrsi(new Karta { Konsignator = new Konsignator { NazivKriterijum = "Jovan" } });

            CollectionAssert.AreEquivalent(new[] { k1.IdKarta, k2.IdKarta }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoMestuDogadjaja()
        {
            List<int> result = Izvrsi(new Karta { Dogadjaj = new Dogadjaj { Mesto = "Novi" } });

            CollectionAssert.AreEquivalent(new[] { k1.IdKarta, k2.IdKarta }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuSamoSlobodne()
        {
            List<int> result = Izvrsi(new Karta { SamoSlobodne = true });

            CollectionAssert.AreEquivalent(new[] { k2.IdKarta, k3.IdKarta }, result);
        }
    }
}
