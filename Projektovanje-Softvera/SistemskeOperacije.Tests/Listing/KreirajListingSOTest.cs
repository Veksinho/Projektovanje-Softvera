using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.KartaSO;
using SistemskeOperacije.ListingSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class KreirajListingSOTest : IntegracioniTest
    {
        private Broker broker = null!;
        private Dogadjaj dogadjaj = null!;
        private FizickoLice konsignator = null!;
        private List<Karta> karte = null!;

        private KreirajListingSO so = null!;
        private Listing listing = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.KreirajBrokera("lozinka123");
            dogadjaj = TestPodaci.KreirajDogadjaj();
            konsignator = TestPodaci.KreirajFizickoLice();
            karte = TestPodaci.KreirajKarte(dogadjaj, konsignator, 2);
            listing = TestPodaci.NapraviListing(broker, konsignator, karte);
        }

        private static Karta PretraziKartu(int id)
        {
            PretraziKartaSO searchSo = new PretraziKartaSO(new Karta { IdKarta = id });
            searchSo.ExecuteTemplate();
            return (Karta)searchSo.Result!;
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new KreirajListingSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBezKarata()
        {
            listing.Karte = new List<Karta>();
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Listing mora imati bar jednu kartu.", ex.Message);
        }

        [TestMethod]
        [DataRow(StatusListinga.povucen)]
        [DataRow(StatusListinga.realizovan)]
        public void TestPredusloviNedozvoljenStatus(StatusListinga status)
        {
            listing.Status = status;
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Novi listing može biti samo nacrt ili objavljen.", ex.Message);
        }

        [TestMethod]
        [DataRow(-1.0)]
        [DataRow(101.0)]
        public void TestPredusloviNeispravnaProvizija(double provizija)
        {
            listing.ProcenatProvizije = (decimal)provizija;
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Procenat provizije listinga mora biti između 0 i 100.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDatumIstekaDanas()
        {
            listing.DatumIsteka = DateTime.Today;
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Datum isteka listinga mora biti posle datuma objave.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDatumIstekaPosleDogadjaja()
        {
            listing.DatumIsteka = dogadjaj.DatumOdrzavanja.AddDays(1);
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Datum isteka listinga ne može biti posle datuma održavanja događaja.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviNepostojecaKarta()
        {
            listing.Karte.Add(new Karta { IdKarta = 999999 });
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe karte listinga.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviKartaDrugogKonsignatora()
        {
            Karta tudja = TestPodaci.KreirajKartu(dogadjaj, TestPodaci.KreirajFizickoLice(), "Tudja");
            listing.Karte.Add(new Karta { IdKarta = tudja.IdKarta });
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sve karte listinga moraju pripadati istom konsignatoru.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviKarteRazlicitihDogadjaja()
        {
            Karta drugiDogadjaj = TestPodaci.KreirajKartu(TestPodaci.KreirajDogadjaj(), konsignator);
            listing.Karte.Add(new Karta { IdKarta = drugiDogadjaj.IdKarta });
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sve karte listinga moraju biti za isti događaj.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviKartaNaDrugomListingu()
        {
            TestPodaci.KreirajListing(broker, konsignator, new List<Karta> { karte[0] });
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Karta se već nalazi na drugom listingu.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviMinKolicinaNula()
        {
            listing.MinKolicina = 0;
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Minimalna količina listinga mora biti veća od nule.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviMinKolicinaVecaOdBrojaKarata()
        {
            listing.MinKolicina = 3;
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Minimalna količina ne može biti veća od broja karata na listingu.", ex.Message);
        }

        [TestMethod]
        [DataRow(TipSplita.bez_splita, 2, 1,
            "Kod prodaje bez deljenja minimalna količina mora biti jednaka broju karata.")]
        [DataRow(TipSplita.bilo_koja_kolicina, 2, 2,
            "Kod prodaje u bilo kojoj količini minimalna količina mora biti 1.")]
        [DataRow(TipSplita.parne_kolicine, 3, 2,
            "Kod prodaje u parnim količinama broj karata i minimalna količina moraju biti parni.")]
        [DataRow(TipSplita.min_kolicina, 2, 1,
            "Kod prodaje sa minimalnom količinom ta količina mora biti veća od 1.")]
        [DataRow(TipSplita.izbegni_usamljenu, 1, 1,
            "Kod izbegavanja usamljene karte listing mora imati bar dve karte, a minimalna količina mora biti 1.")]
        public void TestPredusloviPraviloSplita(TipSplita split, int brojKarata, int minKolicina, string poruka)
        {
            List<Karta> karteSplita = TestPodaci.KreirajKarte(dogadjaj, konsignator, brojKarata);
            listing = TestPodaci.NapraviListing(broker, konsignator, karteSplita, StatusListinga.objavljen, split, minKolicina);
            so = new KreirajListingSO(listing);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(poruka, ex.Message);
        }

        [TestMethod]
        public void TestPredusloviIspravanSplit()
        {
            listing = TestPodaci.NapraviListing(broker, konsignator, karte, StatusListinga.objavljen, TipSplita.parne_kolicine, 2);
            so = new KreirajListingSO(listing);

            so.ExecuteTemplate();

            Assert.IsTrue(TestBaza.Postoji("Listing", $"idListing = {listing.IdListing}"));
        }

        [TestMethod]
        [DataRow(StatusListinga.nacrt)]
        [DataRow(StatusListinga.objavljen)]
        public void TestIzvrsiOperacijuUspesno(StatusListinga status)
        {
            listing.Status = status;
            so = new KreirajListingSO(listing);

            so.ExecuteTemplate();

            Assert.IsTrue(listing.IdListing > 0);

            PretraziListingSO searchSo = new PretraziListingSO(new Listing { IdListing = listing.IdListing });
            searchSo.ExecuteTemplate();
            Assert.AreEqual(DateTime.Today, ((Listing)searchSo.Result!).DatumObjave.Date);

            foreach (Karta karta in karte)
            {
                Karta saved = PretraziKartu(karta.IdKarta);
                Assert.IsNotNull(saved.Listing);
                Assert.AreEqual(listing.IdListing, saved.Listing.IdListing);
                Assert.AreEqual(StatusKarte.plasirana, saved.Status);
            }
        }
    }
}
