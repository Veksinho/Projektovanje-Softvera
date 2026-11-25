using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class ObrisiBrokerSOTest : IntegracioniTest
    {
        private ObrisiBrokerSO so = null!;
        private Broker broker = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.KreirajBrokera("lozinka123", 1, 2);
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new ObrisiBrokerSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBrokerSaListingom()
        {
            Dogadjaj dogadjaj = TestPodaci.KreirajDogadjaj();
            FizickoLice konsignator = TestPodaci.KreirajFizickoLice();
            TestPodaci.KreirajListing(broker, konsignator, TestPodaci.KreirajKarte(dogadjaj, konsignator, 2));
            so = new ObrisiBrokerSO(new Broker { IdBroker = broker.IdBroker });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Broker ima kreirane listinge i ne može se obrisati.", ex.Message);
            Assert.IsTrue(TestBaza.Postoji("Broker", $"idBroker = {broker.IdBroker}"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            so = new ObrisiBrokerSO(new Broker { IdBroker = broker.IdBroker });

            so.ExecuteTemplate();

            Assert.IsFalse(TestBaza.Postoji("Broker", $"idBroker = {broker.IdBroker}"));
            Assert.IsFalse(TestBaza.Postoji("BrKd", $"idBroker = {broker.IdBroker}"));
        }
    }
}
