using System.Net;
using System.Net.Sockets;
using Common.Domen;
using Common.Domen.Enumeracije;
using Common.Komunikacija;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class JsonNetworkSerializerTest
    {
        private static T Posalji<T>(Operacija operacija, object objekat) where T : class
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            try
            {
                using Socket klijent = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                klijent.Connect((IPEndPoint)listener.LocalEndpoint);
                using Socket server = listener.AcceptSocket();

                JsonNetworkSerializer posiljalac = new JsonNetworkSerializer(klijent);
                JsonNetworkSerializer primalac = new JsonNetworkSerializer(server);

                posiljalac.Send(new Zahtev(operacija, objekat));
                Zahtev zahtev = primalac.Receive<Zahtev>();
                T rezultat = primalac.ReadType<T>(zahtev.Objekat)!;

                posiljalac.Close();
                primalac.Close();
                return rezultat;
            }
            finally
            {
                listener.Stop();
            }
        }

        private static FizickoLice NapraviFizickoLice() => new FizickoLice
        {
            IdKonsignator = 7,
            Email = "petar@test.rs",
            Telefon = "0601234567",
            Adresa = "Adresa 1",
            DatumRegistracije = DateTime.Today,
            Jmbg = "0101990710001",
            Ime = "Petar",
            Prezime = "Jovanović",
            BrojLicneKarte = "123456789"
        };

        [TestMethod]
        public void TestFizickoLiceKaoKonsignator()
        {
            FizickoLice poslato = NapraviFizickoLice();

            Konsignator primljeno = Posalji<Konsignator>(Operacija.KreirajKonsignator, poslato);

            Assert.IsInstanceOfType<FizickoLice>(primljeno);
            FizickoLice fl = (FizickoLice)primljeno;
            Assert.AreEqual(poslato.IdKonsignator, fl.IdKonsignator);
            Assert.AreEqual(poslato.Email, fl.Email);
            Assert.AreEqual(poslato.Telefon, fl.Telefon);
            Assert.AreEqual(poslato.Adresa, fl.Adresa);
            Assert.AreEqual(poslato.DatumRegistracije, fl.DatumRegistracije);
            Assert.AreEqual(poslato.Jmbg, fl.Jmbg);
            Assert.AreEqual(poslato.Ime, fl.Ime);
            Assert.AreEqual(poslato.Prezime, fl.Prezime);
            Assert.AreEqual(poslato.BrojLicneKarte, fl.BrojLicneKarte);
        }

        [TestMethod]
        public void TestPravnoLiceKaoKonsignator()
        {
            PravnoLice poslato = new PravnoLice
            {
                IdKonsignator = 8,
                Email = "firma@test.rs",
                Telefon = "0111234567",
                Adresa = "Adresa 2",
                Pib = "123456789",
                MaticniBroj = "12345678",
                NazivFirme = "Jovanović DOO"
            };

            Konsignator primljeno = Posalji<Konsignator>(Operacija.KreirajKonsignator, poslato);

            Assert.IsInstanceOfType<PravnoLice>(primljeno);
            PravnoLice pl = (PravnoLice)primljeno;
            Assert.AreEqual(poslato.Pib, pl.Pib);
            Assert.AreEqual(poslato.MaticniBroj, pl.MaticniBroj);
            Assert.AreEqual(poslato.NazivFirme, pl.NazivFirme);
        }

        [TestMethod]
        public void TestListaKarataSaUgnjezdenimObjektima()
        {
            Dogadjaj dogadjaj = new Dogadjaj
            {
                IdDogadjaj = 3,
                Naziv = "Exit festival",
                Mesto = "Novi Sad",
                DatumOdrzavanja = DateTime.Today.AddDays(30)
            };
            FizickoLice konsignator = NapraviFizickoLice();
            List<Karta> poslato = new List<Karta>
            {
                new Karta
                {
                    IdKarta = 1, Sektor = "Parter", Red = "1", Sediste = "1", NominalnaCena = 12.50m,
                    Tip = TipKarte.stajaca, Format = FormatKarte.pdf, Status = StatusKarte.u_inventaru,
                    Dogadjaj = dogadjaj, Konsignator = konsignator
                },
                new Karta
                {
                    IdKarta = 2, Sektor = "Tribina", Red = "2", Sediste = "5", NominalnaCena = 4000m,
                    Tip = TipKarte.sedeca, Format = FormatKarte.mobilna, Status = StatusKarte.plasirana,
                    Dogadjaj = dogadjaj, Konsignator = konsignator
                }
            };

            List<Karta> primljeno = Posalji<List<Karta>>(Operacija.VratiListuKarta, poslato);

            Assert.AreEqual(poslato.Count, primljeno.Count);
            for (int i = 0; i < poslato.Count; i++)
            {
                Assert.AreEqual(poslato[i].IdKarta, primljeno[i].IdKarta);
                Assert.AreEqual(poslato[i].Sektor, primljeno[i].Sektor);
                Assert.AreEqual(poslato[i].NominalnaCena, primljeno[i].NominalnaCena);
                Assert.AreEqual(poslato[i].Tip, primljeno[i].Tip);
                Assert.AreEqual(poslato[i].Format, primljeno[i].Format);
                Assert.AreEqual(poslato[i].Status, primljeno[i].Status);
                Assert.AreEqual(dogadjaj.IdDogadjaj, primljeno[i].Dogadjaj!.IdDogadjaj);
                Assert.AreEqual(dogadjaj.Naziv, primljeno[i].Dogadjaj!.Naziv);
                Assert.AreEqual(dogadjaj.DatumOdrzavanja, primljeno[i].Dogadjaj!.DatumOdrzavanja);
                Assert.IsInstanceOfType<FizickoLice>(primljeno[i].Konsignator);
                Assert.AreEqual(konsignator.Jmbg, ((FizickoLice)primljeno[i].Konsignator!).Jmbg);
            }
        }

        [TestMethod]
        public void TestBrokerSaSpecijalizacijama()
        {
            Broker poslato = new Broker
            {
                IdBroker = 5,
                KorisnickoIme = "mmarkovic",
                Sifra = "lozinka123",
                Ime = "Marko",
                Prezime = "Marković",
                Telefon = "0601234567"
            };
            poslato.Specijalizacije.Add(new BrokerKategorijaDogadjaja
            {
                Broker = poslato,
                KategorijaDogadjaja = new KategorijaDogadjaja { IdKategorijaDogadjaja = 1, Naziv = "Koncert", Opis = "Opis 1" },
                DatumSpecijalizacije = DateTime.Today.AddDays(-30)
            });
            poslato.Specijalizacije.Add(new BrokerKategorijaDogadjaja
            {
                Broker = poslato,
                KategorijaDogadjaja = new KategorijaDogadjaja { IdKategorijaDogadjaja = 3, Naziv = "Pozorišna predstava", Opis = "Opis 3" },
                DatumSpecijalizacije = DateTime.Today
            });

            Broker primljeno = Posalji<Broker>(Operacija.KreirajBroker, poslato);

            Assert.AreEqual(poslato.IdBroker, primljeno.IdBroker);
            Assert.AreEqual(poslato.KorisnickoIme, primljeno.KorisnickoIme);
            Assert.AreEqual(2, primljeno.Specijalizacije.Count);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(poslato.Specijalizacije[i].KategorijaDogadjaja.IdKategorijaDogadjaja,
                    primljeno.Specijalizacije[i].KategorijaDogadjaja.IdKategorijaDogadjaja);
                Assert.AreEqual(poslato.Specijalizacije[i].KategorijaDogadjaja.Naziv,
                    primljeno.Specijalizacije[i].KategorijaDogadjaja.Naziv);
                Assert.AreEqual(poslato.Specijalizacije[i].DatumSpecijalizacije,
                    primljeno.Specijalizacije[i].DatumSpecijalizacije);
                Assert.AreSame(primljeno, primljeno.Specijalizacije[i].Broker);
            }
        }
    }
}
