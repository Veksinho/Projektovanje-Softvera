using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class KreirajBrokerSOTest : IntegracioniTest
    {
        private KreirajBrokerSO so = null!;
        private Broker broker = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.NapraviBrokera("lozinka123");
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new KreirajBrokerSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBezSifre()
        {
            broker.Sifra = "";
            so = new KreirajBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Šifra brokera je obavezna.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDuplikatKorisnickogImena()
        {
            int brojPre = TestBaza.BrojRedova("Broker");
            broker.KorisnickoIme = "vtonic";
            so = new KreirajBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Broker sa unetim korisničkim imenom već postoji.", ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("Broker"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new KreirajBrokerSO(broker);

            so.ExecuteTemplate();

            Assert.IsTrue(broker.IdBroker > 0);

            PretraziBrokerSO searchSo = new PretraziBrokerSO(new Broker { IdBroker = broker.IdBroker });
            searchSo.ExecuteTemplate();
            Broker saved = (Broker)searchSo.Result!;

            Assert.AreEqual(broker.KorisnickoIme, saved.KorisnickoIme);
            Assert.AreEqual(broker.Ime, saved.Ime);
            Assert.AreEqual(broker.Prezime, saved.Prezime);
            Assert.AreEqual(broker.Telefon, saved.Telefon);
            Assert.AreEqual(0, saved.Specijalizacije.Count);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuSaSpecijalizacijama()
        {
            foreach (int id in new[] { 1, 2 })
            {
                broker.Specijalizacije.Add(new BrokerKategorijaDogadjaja
                {
                    Broker = broker,
                    KategorijaDogadjaja = new KategorijaDogadjaja { IdKategorijaDogadjaja = id },
                    DatumSpecijalizacije = default
                });
            }
            so = new KreirajBrokerSO(broker);

            so.ExecuteTemplate();

            Assert.AreEqual(2, TestBaza.BrojRedova("BrKd", $"idBroker = {broker.IdBroker}"));
            Assert.AreEqual(2, TestBaza.BrojRedova("BrKd",
                $"idBroker = {broker.IdBroker} AND datumSpecijalizacije = '{DateTime.Today:yyyy-MM-dd}'"));
        }
    }
}
