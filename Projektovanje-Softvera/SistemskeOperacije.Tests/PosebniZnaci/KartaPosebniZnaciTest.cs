using Common.Domen;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KartaSO;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class KartaPosebniZnaciTest : IntegracioniTest
    {
        private const string ZauzetoSediste = "Karta za to sedište na izabranom događaju već postoji.";

        private Dogadjaj dogadjaj = null!;
        private FizickoLice konsignator = null!;

        [TestInitialize]
        public void SetUp()
        {
            dogadjaj = TestPodaci.KreirajDogadjaj();
            konsignator = TestPodaci.KreirajFizickoLice();
        }

        private static Karta Pretrazi(int id)
        {
            PretraziKartaSO so = new PretraziKartaSO(new Karta { IdKarta = id });
            so.ExecuteTemplate();
            return (Karta)so.Result!;
        }

        private static List<Karta> VratiListu(Karta kriterijum)
        {
            VratiListuKartaSO so = new VratiListuKartaSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Karta>)so.Result!;
        }

        private static Karta Kreiraj(Dogadjaj dogadjaj, Konsignator konsignator, string tekst) =>
            TestPodaci.KreirajKartu(dogadjaj, konsignator,
                tekst, TestPodaci.Skrati(tekst, 10), TestPodaci.Skrati(tekst, 10));

        private static void ProveriPolja(string tekst, Karta sacuvana)
        {
            Assert.AreEqual(tekst, sacuvana.Sektor);
            Assert.AreEqual(TestPodaci.Skrati(tekst, 10), sacuvana.Red);
            Assert.AreEqual(TestPodaci.Skrati(tekst, 10), sacuvana.Sediste);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuUpis(string tekst)
        {
            Karta karta = Kreiraj(dogadjaj, konsignator, tekst);

            ProveriPolja(tekst, Pretrazi(karta.IdKarta));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuIzmena(string tekst)
        {
            Karta karta = Pretrazi(TestPodaci.KreirajKartu(dogadjaj, konsignator).IdKarta);
            karta.Sektor = tekst;
            karta.Red = TestPodaci.Skrati(tekst, 10);
            karta.Sediste = TestPodaci.Skrati(tekst, 10);

            new PromeniKartaSO(karta).ExecuteTemplate();

            ProveriPolja(tekst, Pretrazi(karta.IdKarta));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoSektoruRedu(string tekst)
        {
            TestPodaci.KreirajKartu(dogadjaj, konsignator);
            Karta karta = Kreiraj(dogadjaj, konsignator, tekst);

            List<Karta> rezultat = VratiListu(new Karta
            {
                Sektor = TestPodaci.DeoTeksta(tekst),
                Red = TestPodaci.DeoTeksta(TestPodaci.Skrati(tekst, 10)),
                Sediste = TestPodaci.DeoTeksta(TestPodaci.Skrati(tekst, 10))
            });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(karta.IdKarta, rezultat[0].IdKarta);
            ProveriPolja(tekst, rezultat[0]);
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

            TestPodaci.KreirajKartu(dogadjaj, konsignator);
            Karta karta = TestPodaci.KreirajKartu(dogadjaj, trazeni, "B");

            List<Karta> rezultat = VratiListu(new Karta
            {
                Konsignator = new Konsignator { NazivKriterijum = TestPodaci.DeoTeksta(tekst) }
            });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(karta.IdKarta, rezultat[0].IdKarta);
            Assert.AreEqual(tekst, ((FizickoLice)rezultat[0].Konsignator!).Ime);
            Assert.AreEqual(tekst, ((FizickoLice)rezultat[0].Konsignator!).Prezime);
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

            TestPodaci.KreirajKartu(dogadjaj, konsignator);
            Karta karta = TestPodaci.KreirajKartu(trazeni, konsignator);

            List<Karta> rezultat = VratiListu(new Karta
            {
                Dogadjaj = new Dogadjaj { Mesto = TestPodaci.DeoTeksta(tekst) }
            });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(karta.IdKarta, rezultat[0].IdKarta);
            Assert.AreEqual(tekst, rezultat[0].Dogadjaj!.Mesto);
        }

        [TestMethod]
        [DataRow("A'1")]
        [DataRow("Č")]
        public void TestPredusloviZauzetoSedisteSaPosebnimZnacima(string sektor)
        {
            TestPodaci.KreirajKartu(dogadjaj, konsignator, sektor, "1", "1");
            int brojPre = TestBaza.BrojRedova("Karta");

            KreirajKartaSO so = new KreirajKartaSO(TestPodaci.NapraviKartu(dogadjaj, konsignator, sektor, "1", "1"));

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(ZauzetoSediste, ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("Karta"));
        }

        [TestMethod]
        [DataRow("A1", "A'1")]
        [DataRow("C", "Č")]
        public void TestPredusloviSlobodnoSedisteSlicanSektor(string postojeci, string novi)
        {
            TestPodaci.KreirajKartu(dogadjaj, konsignator, postojeci, "1", "1");

            Karta karta = TestPodaci.KreirajKartu(dogadjaj, konsignator, novi, "1", "1");

            Assert.AreEqual(novi, Pretrazi(karta.IdKarta).Sektor);
        }

        [TestMethod]
        [DataRow("A'1")]
        [DataRow("Č")]
        public void TestPredusloviPromeniNaZauzetoSedisteSaPosebnimZnacima(string sektor)
        {
            TestPodaci.KreirajKartu(dogadjaj, konsignator, sektor, "1", "1");
            Karta karta = Pretrazi(TestPodaci.KreirajKartu(dogadjaj, konsignator, "B", "1", "1").IdKarta);
            karta.Sektor = sektor;
            PromeniKartaSO so = new PromeniKartaSO(karta);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(ZauzetoSediste, ex.Message);
            Assert.AreEqual("B", Pretrazi(karta.IdKarta).Sektor);
        }
    }
}
