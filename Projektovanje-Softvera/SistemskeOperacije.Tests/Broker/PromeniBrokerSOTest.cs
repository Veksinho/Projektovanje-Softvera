using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PromeniBrokerSOTest : IntegracioniTest
    {
        private const string Sifra = "lozinka123";
        private const string NovaSifra = "novaLozinka456";

        private PromeniBrokerSO so = null!;
        private Broker broker = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.KreirajBrokera(Sifra);
        }

        private static Broker Pretrazi(int id)
        {
            PretraziBrokerSO searchSo = new PretraziBrokerSO(new Broker { IdBroker = id });
            searchSo.ExecuteTemplate();
            return (Broker)searchSo.Result!;
        }

        private static Broker Prijavi(string korisnickoIme, string sifra)
        {
            PrijaviBrokerSO loginSo = new PrijaviBrokerSO(new Broker { KorisnickoIme = korisnickoIme, Sifra = sifra });
            loginSo.ExecuteTemplate();
            return (Broker)loginSo.Result!;
        }

        private static List<int> KategorijeIzBaze(int idBroker) =>
            Pretrazi(idBroker).Specijalizacije.Select(s => s.KategorijaDogadjaja.IdKategorijaDogadjaja).ToList();

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PromeniBrokerSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow("lozink1")]
        [DataRow("lozinkaa")]
        [DataRow("12345678")]
        public void TestPredusloviNeispravnaSifra(string sifra)
        {
            int brojPre = TestBaza.BrojRedova("Broker");
            string? hesPre = TestBaza.SifraHash(broker.IdBroker);
            broker.Sifra = sifra;
            so = new PromeniBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Šifra brokera nije u odgovarajućem formatu.", ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("Broker"));
            Assert.AreEqual(hesPre, TestBaza.SifraHash(broker.IdBroker));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void TestPredusloviNeispravanId(int id)
        {
            broker.IdBroker = id;
            so = new PromeniBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nije prosleđen id brokera.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDuplikatKorisnickogImena()
        {
            broker.KorisnickoIme = "vtonic";
            so = new PromeniBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Broker sa unetim korisničkim imenom već postoji.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviIstoKorisnickoImeSopstvenogZapisa()
        {
            broker.Ime = "Izmenjeno";
            so = new PromeniBrokerSO(broker);

            so.ExecuteTemplate();

            Assert.AreEqual("Izmenjeno", Pretrazi(broker.IdBroker).Ime);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuZamenaSpecijalizacija()
        {
            broker = TestPodaci.KreirajBrokera(Sifra, 1, 2);
            broker.Specijalizacije = new[] { 2, 3 }
                .Select(id => new BrokerKategorijaDogadjaja
                {
                    Broker = broker,
                    KategorijaDogadjaja = new KategorijaDogadjaja { IdKategorijaDogadjaja = id }
                })
                .ToList();
            so = new PromeniBrokerSO(broker);

            so.ExecuteTemplate();

            CollectionAssert.AreEquivalent(new[] { 2, 3 }, KategorijeIzBaze(broker.IdBroker));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuZadrzavaDatumSpecijalizacije()
        {
            broker = TestPodaci.KreirajBrokera(Sifra, 1);
            DateTime datum = DateTime.Today.AddDays(-30);
            TestBaza.Execute($"UPDATE BrKd SET datumSpecijalizacije = '{datum:yyyy-MM-dd}' WHERE idBroker = {broker.IdBroker}");
            so = new PromeniBrokerSO(Pretrazi(broker.IdBroker));

            so.ExecuteTemplate();

            Broker saved = Pretrazi(broker.IdBroker);
            Assert.AreEqual(1, saved.Specijalizacije.Count);
            Assert.AreEqual(datum, saved.Specijalizacije[0].DatumSpecijalizacije);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuSifraIzPretrage()
        {
            string? hesPre = TestBaza.SifraHash(broker.IdBroker);
            Broker izmena = Pretrazi(broker.IdBroker);
            Assert.IsNull(izmena.Sifra);
            izmena.Ime = "Izmenjeno";
            so = new PromeniBrokerSO(izmena);

            so.ExecuteTemplate();

            Assert.AreEqual(hesPre, TestBaza.SifraHash(broker.IdBroker));
            Assert.AreEqual(broker.IdBroker, Prijavi(broker.KorisnickoIme, Sifra).IdBroker);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuBezNoveSifre()
        {
            string? hesPre = TestBaza.SifraHash(broker.IdBroker);
            Broker izmena = Pretrazi(broker.IdBroker);
            izmena.Sifra = null!;
            izmena.Ime = "Izmenjeno";
            so = new PromeniBrokerSO(izmena);

            so.ExecuteTemplate();

            Assert.AreEqual(hesPre, TestBaza.SifraHash(broker.IdBroker));
            Assert.AreEqual(broker.IdBroker, Prijavi(broker.KorisnickoIme, Sifra).IdBroker);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNovaSifra()
        {
            string? hesPre = TestBaza.SifraHash(broker.IdBroker);
            Broker izmena = Pretrazi(broker.IdBroker);
            izmena.Sifra = NovaSifra;
            so = new PromeniBrokerSO(izmena);

            so.ExecuteTemplate();

            Assert.AreEqual(broker.IdBroker, Prijavi(broker.KorisnickoIme, NovaSifra).IdBroker);
            Exception ex = Assert.Throws<Exception>(() => Prijavi(broker.KorisnickoIme, Sifra));
            Assert.AreEqual("Korisničko ime ili šifra nisu ispravni.", ex.Message);
            Assert.AreNotEqual(hesPre, TestBaza.SifraHash(broker.IdBroker));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNeVracaSifru()
        {
            broker.Sifra = NovaSifra;
            so = new PromeniBrokerSO(broker);

            so.ExecuteTemplate();

            Assert.IsNull(((Broker)so.Result!).Sifra);
        }
    }
}
