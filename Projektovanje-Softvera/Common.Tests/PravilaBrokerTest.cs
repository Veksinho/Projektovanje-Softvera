using Common.Domen;
using Common.Validacija;
using static Common.Tests.PomocPravila;

namespace Common.Tests
{
    [TestClass]
    public class PravilaBrokerTest
    {
        private static Broker Ispravan() => new Broker
        {
            KorisnickoIme = "pera",
            Sifra = "lozinka1",
            Ime = "Petar",
            Prezime = "Petrović",
            Telefon = "+381641234567"
        };

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestIspravanBrokerNemaGresaka(bool sifraObavezna)
        {
            NemaGresaka(PravilaBroker.Proveri(Ispravan(), sifraObavezna));
        }

        [TestMethod]
        public void TestPraznaSifraJeDozvoljenaKadNijeObavezna()
        {
            Broker b = Ispravan();
            b.Sifra = "";

            NemaGresaka(PravilaBroker.Proveri(b, false));
        }

        [TestMethod]
        public void TestNullBrokerBacaIzuzetak()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => PravilaBroker.Proveri(null!, true));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void TestKorisnickoImeObavezno(string vrednost)
        {
            Broker b = Ispravan();
            b.KorisnickoIme = vrednost;

            JednaGreska(PravilaBroker.Proveri(b, true),
                nameof(Broker.KorisnickoIme), "Korisničko ime brokera je obavezno.");
        }

        [TestMethod]
        public void TestKorisnickoImeDuzina()
        {
            Broker b = Ispravan();
            b.KorisnickoIme = new string('a', 50);
            NemaGresaka(PravilaBroker.Proveri(b, true));

            b.KorisnickoIme = new string('a', 51);
            JednaGreska(PravilaBroker.Proveri(b, true),
                nameof(Broker.KorisnickoIme), "Korisničko ime brokera može imati najviše 50 znakova.");
        }

        [TestMethod]
        public void TestSifraObavezna()
        {
            Broker b = Ispravan();
            b.Sifra = "";

            JednaGreska(PravilaBroker.Proveri(b, true),
                nameof(Broker.Sifra), "Šifra brokera je obavezna.");
        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestSifraFormat(bool sifraObavezna)
        {
            Broker b = Ispravan();
            b.Sifra = "lozinkaa";

            JednaGreska(PravilaBroker.Proveri(b, sifraObavezna),
                nameof(Broker.Sifra), "Šifra brokera nije u odgovarajućem formatu.");
        }

        [TestMethod]
        public void TestImeObavezno()
        {
            Broker b = Ispravan();
            b.Ime = "";

            JednaGreska(PravilaBroker.Proveri(b, true), nameof(Broker.Ime), "Ime brokera je obavezno.");
        }

        [TestMethod]
        public void TestImeDuzina()
        {
            Broker b = Ispravan();
            b.Ime = new string('a', 51);

            JednaGreska(PravilaBroker.Proveri(b, true),
                nameof(Broker.Ime), "Ime brokera može imati najviše 50 znakova.");
        }

        [TestMethod]
        public void TestPrezimeObavezno()
        {
            Broker b = Ispravan();
            b.Prezime = "";

            JednaGreska(PravilaBroker.Proveri(b, true), nameof(Broker.Prezime), "Prezime brokera je obavezno.");
        }

        [TestMethod]
        public void TestPrezimeDuzina()
        {
            Broker b = Ispravan();
            b.Prezime = new string('a', 51);

            JednaGreska(PravilaBroker.Proveri(b, true),
                nameof(Broker.Prezime), "Prezime brokera može imati najviše 50 znakova.");
        }

        [TestMethod]
        public void TestTelefonObavezan()
        {
            Broker b = Ispravan();
            b.Telefon = "";

            JednaGreska(PravilaBroker.Proveri(b, true), nameof(Broker.Telefon), "Telefon brokera je obavezan.");
        }

        [TestMethod]
        [DataRow("064 123 4567")]
        [DataRow("12345")]
        public void TestTelefonFormat(string telefon)
        {
            Broker b = Ispravan();
            b.Telefon = telefon;

            JednaGreska(PravilaBroker.Proveri(b, true), nameof(Broker.Telefon),
                "Telefon brokera nije u odgovarajućem formatu.");
        }

        [TestMethod]
        public void TestViseGresakaURedosleduPolja()
        {
            Broker b = new Broker { KorisnickoIme = "", Sifra = "", Ime = "", Prezime = "", Telefon = "" };

            CollectionAssert.AreEqual(
                new[] { nameof(Broker.KorisnickoIme), nameof(Broker.Sifra), nameof(Broker.Ime),
                        nameof(Broker.Prezime), nameof(Broker.Telefon) },
                PravilaBroker.Proveri(b, true).Select(g => g.Polje).ToArray());
        }

        [TestMethod]
        [DataRow("lozinkaa")]
        [DataRow("x")]
        public void TestPrijavaIspravnaNemaGresaka(string sifra)
        {
            NemaGresaka(PravilaBroker.ProveriPrijavu(new Broker { KorisnickoIme = "pera", Sifra = sifra }));
        }

        [TestMethod]
        public void TestPrijavaNullBrokerBacaIzuzetak()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => PravilaBroker.ProveriPrijavu(null!));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void TestPrijavaKorisnickoImeObavezno(string vrednost)
        {
            JednaGreska(PravilaBroker.ProveriPrijavu(new Broker { KorisnickoIme = vrednost, Sifra = "lozinka1" }),
                nameof(Broker.KorisnickoIme), "Korisničko ime je obavezno.");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void TestPrijavaSifraObavezna(string vrednost)
        {
            JednaGreska(PravilaBroker.ProveriPrijavu(new Broker { KorisnickoIme = "pera", Sifra = vrednost }),
                nameof(Broker.Sifra), "Šifra je obavezna.");
        }

        [TestMethod]
        public void TestPrijavaNeProveravaDuzinuNiFormat()
        {
            NemaGresaka(PravilaBroker.ProveriPrijavu(
                new Broker { KorisnickoIme = new string('a', 51), Sifra = "' OR 1=1 --" }));
        }

        [TestMethod]
        public void TestPrijavaObeGreskeURedosleduPolja()
        {
            CollectionAssert.AreEqual(
                new[] { nameof(Broker.KorisnickoIme), nameof(Broker.Sifra) },
                PravilaBroker.ProveriPrijavu(new Broker { KorisnickoIme = "", Sifra = "" })
                    .Select(g => g.Polje).ToArray());
        }
    }
}
