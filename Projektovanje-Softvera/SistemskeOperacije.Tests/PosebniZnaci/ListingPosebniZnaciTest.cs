using Common.Domen;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KonsignatorSO;
using SistemskeOperacije.ListingSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class ListingPosebniZnaciTest : IntegracioniTest
    {
        private Broker broker = null!;
        private Dogadjaj dogadjaj = null!;
        private FizickoLice konsignator = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.KreirajBrokera("lozinka123");
            dogadjaj = TestPodaci.KreirajDogadjaj();
            konsignator = TestPodaci.KreirajFizickoLice();
        }

        private static Listing Pretrazi(int id)
        {
            PretraziListingSO so = new PretraziListingSO(new Listing { IdListing = id });
            so.ExecuteTemplate();
            return (Listing)so.Result!;
        }

        private static List<Listing> VratiListu(Listing kriterijum)
        {
            VratiListuListingSO so = new VratiListuListingSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Listing>)so.Result!;
        }

        // kontrolni listing sa običnim tekstom, koji pretraga ne sme da vrati
        private void KreirajKontrolniListing()
        {
            TestPodaci.KreirajListing(broker, konsignator, TestPodaci.KreirajKarte(dogadjaj, konsignator, 1));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuUpis(string tekst)
        {
            Listing listing = TestPodaci.NapraviListing(broker, konsignator, TestPodaci.KreirajKarte(dogadjaj, konsignator, 1));
            listing.Napomena = tekst;

            new KreirajListingSO(listing).ExecuteTemplate();

            Assert.AreEqual(tekst, Pretrazi(listing.IdListing).Napomena);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuIzmena(string tekst)
        {
            int id = TestPodaci.KreirajListing(broker, konsignator, TestPodaci.KreirajKarte(dogadjaj, konsignator, 1)).IdListing;
            Listing listing = Pretrazi(id);
            listing.Napomena = tekst;

            new PromeniListingSO(listing).ExecuteTemplate();

            Assert.AreEqual(tekst, Pretrazi(id).Napomena);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoImenuKonsignatora(string tekst)
        {
            FizickoLice trazeni = TestPodaci.NapraviFizickoLice();
            trazeni.Ime = tekst;
            trazeni.Prezime = tekst;
            new KreirajKonsignatorSO(trazeni).ExecuteTemplate();

            KreirajKontrolniListing();
            Listing listing = TestPodaci.KreirajListing(broker, trazeni, TestPodaci.KreirajKarte(dogadjaj, trazeni, 1));

            List<Listing> rezultat = VratiListu(new Listing
            {
                Konsignator = new Konsignator { NazivKriterijum = TestPodaci.DeoTeksta(tekst) }
            });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(listing.IdListing, rezultat[0].IdListing);
            Assert.AreEqual($"{tekst} {tekst}", rezultat[0].Konsignator!.Name);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoSektoruKarte(string tekst)
        {
            KreirajKontrolniListing();
            Karta karta = TestPodaci.KreirajKartu(dogadjaj, konsignator, tekst);
            Listing listing = TestPodaci.KreirajListing(broker, konsignator, new List<Karta> { karta });

            List<Listing> rezultat = VratiListu(new Listing
            {
                KriterijumKarta = new Karta { Sektor = TestPodaci.DeoTeksta(tekst) }
            });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(listing.IdListing, rezultat[0].IdListing);
            Assert.AreEqual(tekst, rezultat[0].Karte[0].Sektor);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoMestuDogadjaja(string tekst)
        {
            Dogadjaj trazeni = TestPodaci.NapraviDogadjaj();
            trazeni.Mesto = tekst;
            new UbaciDogadjajSO(trazeni).ExecuteTemplate();

            KreirajKontrolniListing();
            Listing listing = TestPodaci.KreirajListing(broker, konsignator, TestPodaci.KreirajKarte(trazeni, konsignator, 1));

            List<Listing> rezultat = VratiListu(new Listing
            {
                KriterijumDogadjaj = new Dogadjaj { Mesto = TestPodaci.DeoTeksta(tekst) }
            });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(listing.IdListing, rezultat[0].IdListing);
            Assert.AreEqual(tekst, rezultat[0].Karte[0].Dogadjaj!.Mesto);
        }
    }
}
