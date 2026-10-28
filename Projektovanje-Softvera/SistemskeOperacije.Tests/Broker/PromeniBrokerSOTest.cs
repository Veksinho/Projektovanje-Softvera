using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PromeniBrokerSOTest : IntegracioniTest
    {
        private const string Sifra = "lozinka123";

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

        private static List<int> KategorijeIzBaze(int idBroker) =>
            Pretrazi(idBroker).Specijalizacije.Select(s => s.KategorijaDogadjaja.IdKategorijaDogadjaja).ToList();

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PromeniBrokerSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
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
            Broker izmena = Pretrazi(broker.IdBroker);
            izmena.Ime = "Izmenjeno";
            so = new PromeniBrokerSO(izmena);

            so.ExecuteTemplate();

            PrijaviBrokerSO loginSo = new PrijaviBrokerSO(new Broker { KorisnickoIme = broker.KorisnickoIme, Sifra = Sifra });
            loginSo.ExecuteTemplate();
            Assert.AreEqual(broker.IdBroker, ((Broker)loginSo.Result!).IdBroker);
        }

        [TestMethod]
        [Ignore("Poznata greška: Broker.UpdateValues uvek upisuje šifru, pa Promeni bez nove šifre briše postojeću")]
        public void TestIzvrsiOperacijuBezNoveSifre()
        {
            Broker izmena = Pretrazi(broker.IdBroker);
            izmena.Sifra = null!;
            izmena.Ime = "Izmenjeno";
            so = new PromeniBrokerSO(izmena);

            so.ExecuteTemplate();

            PrijaviBrokerSO loginSo = new PrijaviBrokerSO(new Broker { KorisnickoIme = broker.KorisnickoIme, Sifra = Sifra });
            loginSo.ExecuteTemplate();
            Assert.AreEqual(broker.IdBroker, ((Broker)loginSo.Result!).IdBroker);
        }
    }
}
