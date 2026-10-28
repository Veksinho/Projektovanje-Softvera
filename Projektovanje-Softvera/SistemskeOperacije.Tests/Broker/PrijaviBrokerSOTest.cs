using Common.Domen;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PrijaviBrokerSOTest : IntegracioniTest
    {
        private const string FailedLoginMessage = "Korisničko ime ili šifra nisu ispravni.";
        private const string CorrectPassword = "lozinka123";

        private Broker savedBroker = null!;

        private PrijaviBrokerSO so = null!;
        private Broker broker = null!;

        [TestInitialize]
        public void SetUp()
        {
            savedBroker = TestPodaci.KreirajBrokera(CorrectPassword);
            broker = new Broker();
        }

        [TestMethod]
        public void TestPredusloviIspravan()
        {
            broker.KorisnickoIme = savedBroker.KorisnickoIme;
            broker.Sifra = CorrectPassword;
            so = new PrijaviBrokerSO(broker);

            so.ExecuteTemplate();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PrijaviBrokerSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void TestPredusloviPraznoKorisnickoIme(string username)
        {
            broker.KorisnickoIme = username;
            broker.Sifra = CorrectPassword;
            so = new PrijaviBrokerSO(broker);

            Assert.Throws<Exception>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void TestPredusloviPraznaSifra(string password)
        {
            broker.KorisnickoIme = savedBroker.KorisnickoIme;
            broker.Sifra = password;
            so = new PrijaviBrokerSO(broker);

            Assert.Throws<Exception>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuUspesno()
        {
            broker.KorisnickoIme = savedBroker.KorisnickoIme;
            broker.Sifra = CorrectPassword;
            so = new PrijaviBrokerSO(broker);

            so.ExecuteTemplate();

            Assert.IsNotNull(so.Result);
            Broker loggedIn = (Broker)so.Result;
            Assert.AreEqual(savedBroker.IdBroker, loggedIn.IdBroker);
            Assert.AreEqual(savedBroker.KorisnickoIme, loggedIn.KorisnickoIme);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPogresnaSifra()
        {
            broker.KorisnickoIme = savedBroker.KorisnickoIme;
            broker.Sifra = "pogresna";
            so = new PrijaviBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(FailedLoginMessage, ex.Message);
            Assert.IsNull(so.Result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNepostojeceKorisnickoIme()
        {
            broker.KorisnickoIme = "nepostojeci_" + Guid.NewGuid().ToString("N")[..8];
            broker.Sifra = CorrectPassword;
            so = new PrijaviBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(FailedLoginMessage, ex.Message);
            Assert.IsNull(so.Result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuSqlInjection()
        {
            broker.KorisnickoIme = "' OR 1=1 --";
            broker.Sifra = "bilo sta";
            so = new PrijaviBrokerSO(broker);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(FailedLoginMessage, ex.Message);
            Assert.IsNull(so.Result);
        }
    }
}
