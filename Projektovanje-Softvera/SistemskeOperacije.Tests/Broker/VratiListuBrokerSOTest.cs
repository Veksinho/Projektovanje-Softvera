using Common.Domen;
using SistemskeOperacije.BrokerSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuBrokerSOTest : IntegracioniTest
    {
        private VratiListuBrokerSO so = null!;

        private List<Broker> Izvrsi(Broker kriterijum)
        {
            so = new VratiListuBrokerSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Broker>)so.Result!;
        }

        [TestMethod]
        public void TestPredusloviPrazanKriterijum()
        {
            so = new VratiListuBrokerSO(new Broker());

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoImenu()
        {
            Broker marko = TestPodaci.NapraviBrokera("lozinka123");
            marko.Ime = "Marko";
            new KreirajBrokerSO(marko).ExecuteTemplate();
            TestPodaci.KreirajBrokera("lozinka123");

            List<Broker> result = Izvrsi(new Broker { Ime = "Mark" });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(marko.IdBroker, result[0].IdBroker);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoKategoriji()
        {
            Broker broker = TestPodaci.KreirajBrokera("lozinka123", 1, 2);
            Broker kriterijum = new Broker
            {
                Specijalizacije = new List<BrokerKategorijaDogadjaja>
                {
                    new BrokerKategorijaDogadjaja
                    {
                        KategorijaDogadjaja = new KategorijaDogadjaja { IdKategorijaDogadjaja = 2 }
                    }
                }
            };

            List<Broker> result = Izvrsi(kriterijum);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(broker.IdBroker, result[0].IdBroker);
            CollectionAssert.AreEquivalent(new[] { 1, 2 },
                result[0].Specijalizacije.Select(s => s.KategorijaDogadjaja.IdKategorijaDogadjaja).ToList());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuBezDuplikata()
        {
            Broker broker = TestPodaci.KreirajBrokera("lozinka123", 1, 2);

            List<Broker> result = Izvrsi(new Broker { KorisnickoIme = broker.KorisnickoIme });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(broker.IdBroker, result[0].IdBroker);
        }
    }
}
