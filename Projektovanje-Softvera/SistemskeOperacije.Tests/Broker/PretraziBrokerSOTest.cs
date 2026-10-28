using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PretraziBrokerSOTest : IntegracioniTest
    {
        private PretraziBrokerSO so = null!;
        private Broker broker = null!;

        [TestInitialize]
        public void SetUp()
        {
            broker = TestPodaci.KreirajBrokera("lozinka123", 1, 3);
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PretraziBrokerSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesnoSaSpecijalizacijama()
        {
            so = new PretraziBrokerSO(new Broker { IdBroker = broker.IdBroker });

            so.ExecuteTemplate();

            Broker found = (Broker)so.Result!;
            Assert.AreEqual(broker.IdBroker, found.IdBroker);
            CollectionAssert.AreEquivalent(
                new[] { "Koncert", "Pozorišna predstava" },
                found.Specijalizacije.Select(s => s.KategorijaDogadjaja.Naziv).ToList());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNepostojeciBroker()
        {
            so = new PretraziBrokerSO(new Broker { IdBroker = 999999 });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe brokera.", ex.Message);
        }
    }
}
