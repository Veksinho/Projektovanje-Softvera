using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class BrokerPosebniZnaciTest : IntegracioniTest
    {
        private const string PostojeceKorisnickoIme = "Broker sa unetim korisničkim imenom već postoji.";

        private static Broker Pretrazi(int id)
        {
            PretraziBrokerSO so = new PretraziBrokerSO(new Broker { IdBroker = id });
            so.ExecuteTemplate();
            return (Broker)so.Result!;
        }

        private static List<Broker> VratiListu(Broker kriterijum)
        {
            VratiListuBrokerSO so = new VratiListuBrokerSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Broker>)so.Result!;
        }

        private static Broker NapraviBrokera(string tekst)
        {
            Broker broker = TestPodaci.NapraviBrokera(tekst);
            broker.KorisnickoIme = tekst;
            broker.Ime = tekst;
            broker.Prezime = tekst;
            return broker;
        }

        private static void ProveriPolja(string tekst, Broker sacuvan)
        {
            Assert.AreEqual(tekst, sacuvan.KorisnickoIme);
            Assert.AreEqual(tekst, sacuvan.Sifra);
            Assert.AreEqual(tekst, sacuvan.Ime);
            Assert.AreEqual(tekst, sacuvan.Prezime);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuUpis(string tekst)
        {
            Broker broker = NapraviBrokera(tekst);

            new KreirajBrokerSO(broker).ExecuteTemplate();

            ProveriPolja(tekst, Pretrazi(broker.IdBroker));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuIzmena(string tekst)
        {
            Broker broker = TestPodaci.KreirajBrokera("lozinka123");
            broker.KorisnickoIme = tekst;
            broker.Sifra = tekst;
            broker.Ime = tekst;
            broker.Prezime = tekst;

            new PromeniBrokerSO(broker).ExecuteTemplate();

            ProveriPolja(tekst, Pretrazi(broker.IdBroker));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoKorisnickomImenu(string tekst)
        {
            TestPodaci.KreirajBrokera("lozinka123");
            Broker broker = NapraviBrokera(tekst);
            new KreirajBrokerSO(broker).ExecuteTemplate();

            List<Broker> rezultat = VratiListu(new Broker { KorisnickoIme = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(broker.IdBroker, rezultat[0].IdBroker);
            Assert.AreEqual(tekst, rezultat[0].KorisnickoIme);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoImenuIPrezimenu(string tekst)
        {
            TestPodaci.KreirajBrokera("lozinka123");
            Broker broker = NapraviBrokera(tekst);
            new KreirajBrokerSO(broker).ExecuteTemplate();
            string deo = TestPodaci.DeoTeksta(tekst);

            List<Broker> rezultat = VratiListu(new Broker { Ime = deo, Prezime = deo });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(broker.IdBroker, rezultat[0].IdBroker);
            Assert.AreEqual(tekst, rezultat[0].Ime);
            Assert.AreEqual(tekst, rezultat[0].Prezime);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPrijavaSifraSaPosebnimZnacima(string sifra)
        {
            Broker sacuvan = TestPodaci.KreirajBrokera(sifra);

            PrijaviBrokerSO so = new PrijaviBrokerSO(new Broker { KorisnickoIme = sacuvan.KorisnickoIme, Sifra = sifra });
            so.ExecuteTemplate();

            Broker prijavljen = (Broker)so.Result!;
            Assert.AreEqual(sacuvan.IdBroker, prijavljen.IdBroker);
            Assert.AreEqual(sifra, prijavljen.Sifra);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPrijavaSifraBezApostrofa()
        {
            Broker sacuvan = TestPodaci.KreirajBrokera("lo'zinka");

            PrijaviBrokerSO so = new PrijaviBrokerSO(new Broker { KorisnickoIme = sacuvan.KorisnickoIme, Sifra = "lozinka" });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Korisničko ime ili šifra nisu ispravni.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviKreirajPostojeceKorisnickoImeSaApostrofom()
        {
            Broker prvi = TestPodaci.NapraviBrokera("lozinka123");
            prvi.KorisnickoIme = "o'brien";
            new KreirajBrokerSO(prvi).ExecuteTemplate();
            int brojPre = TestBaza.BrojRedova("Broker");

            Broker drugi = TestPodaci.NapraviBrokera("lozinka123");
            drugi.KorisnickoIme = "o'brien";
            KreirajBrokerSO so = new KreirajBrokerSO(drugi);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(PostojeceKorisnickoIme, ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("Broker"));
        }

        [TestMethod]
        public void TestPredusloviKreirajKorisnickoImeBezApostrofa()
        {
            Broker prvi = TestPodaci.NapraviBrokera("lozinka123");
            prvi.KorisnickoIme = "o'brien";
            new KreirajBrokerSO(prvi).ExecuteTemplate();

            Broker drugi = TestPodaci.NapraviBrokera("lozinka123");
            drugi.KorisnickoIme = "obrien";
            new KreirajBrokerSO(drugi).ExecuteTemplate();

            Assert.AreEqual("obrien", Pretrazi(drugi.IdBroker).KorisnickoIme);
        }

        [TestMethod]
        public void TestPredusloviPromeniNaPostojeceKorisnickoImeSaApostrofom()
        {
            Broker prvi = TestPodaci.NapraviBrokera("lozinka123");
            prvi.KorisnickoIme = "o'brien";
            new KreirajBrokerSO(prvi).ExecuteTemplate();

            Broker drugi = TestPodaci.KreirajBrokera("lozinka123");
            string staroKorisnickoIme = drugi.KorisnickoIme;
            drugi.KorisnickoIme = "o'brien";
            PromeniBrokerSO so = new PromeniBrokerSO(drugi);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(PostojeceKorisnickoIme, ex.Message);
            Assert.AreEqual(staroKorisnickoIme, Pretrazi(drugi.IdBroker).KorisnickoIme);
        }
    }
}
