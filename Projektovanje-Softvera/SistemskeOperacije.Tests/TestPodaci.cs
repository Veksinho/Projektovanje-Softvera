using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.BrokerSO;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KartaSO;
using SistemskeOperacije.KategorijaDogadjajaSO;
using SistemskeOperacije.KonsignatorSO;
using SistemskeOperacije.ListingSO;

namespace SistemskeOperacije.Tests
{
    internal static class TestPodaci
    {
        private static string UniqueSuffix() => Guid.NewGuid().ToString("N")[..8];

        public static Broker NapraviBrokera(string password) => new Broker
        {
            KorisnickoIme = "test_" + UniqueSuffix(),
            Sifra = password,
            Ime = "Test",
            Prezime = "Broker",
            Telefon = "0601234567"
        };

        public static Broker KreirajBrokera(string password) => KreirajBrokera(password, Array.Empty<int>());

        public static Broker KreirajBrokera(string password, params int[] idKategorija)
        {
            Broker broker = NapraviBrokera(password);

            foreach (int id in idKategorija)
            {
                broker.Specijalizacije.Add(new BrokerKategorijaDogadjaja
                {
                    Broker = broker,
                    KategorijaDogadjaja = new KategorijaDogadjaja { IdKategorijaDogadjaja = id },
                    DatumSpecijalizacije = default
                });
            }

            new KreirajBrokerSO(broker).ExecuteTemplate();
            return broker;
        }

        public static Dogadjaj NapraviDogadjaj(DateTime? datum = null) => new Dogadjaj
        {
            Naziv = "Test dogadjaj " + UniqueSuffix(),
            DatumOdrzavanja = datum ?? DateTime.Today.AddMonths(1),
            Mesto = "Beograd"
        };

        public static Dogadjaj KreirajDogadjaj(DateTime? datum = null)
        {
            Dogadjaj dogadjaj = NapraviDogadjaj(datum);

            new UbaciDogadjajSO(dogadjaj).ExecuteTemplate();
            return dogadjaj;
        }

        public static FizickoLice NapraviFizickoLice()
        {
            string s = UniqueSuffix();
            return new FizickoLice
            {
                Email = $"test_{s}@test.rs",
                Telefon = "0601234567",
                Adresa = "Test adresa 1",
                Jmbg = RandomNumbers(13),
                Ime = "Test",
                Prezime = "Konsignator",
                BrojLicneKarte = RandomNumbers(9)
            };
        }

        public static FizickoLice KreirajFizickoLice()
        {
            FizickoLice fl = NapraviFizickoLice();
            new KreirajKonsignatorSO(fl).ExecuteTemplate();
            return fl;
        }

        public static PravnoLice NapraviPravnoLice()
        {
            string s = UniqueSuffix();
            return new PravnoLice
            {
                Email = $"test_{s}@test.rs",
                Telefon = "0111234567",
                Adresa = "Test adresa 2",
                Pib = RandomNumbers(9),
                MaticniBroj = RandomNumbers(8),
                NazivFirme = "Test firma " + s
            };
        }

        public static PravnoLice KreirajPravnoLice()
        {
            PravnoLice pl = NapraviPravnoLice();
            new KreirajKonsignatorSO(pl).ExecuteTemplate();
            return pl;
        }

        public static KategorijaDogadjaja NapraviKategoriju()
        {
            string s = UniqueSuffix();
            return new KategorijaDogadjaja
            {
                Naziv = "Test kategorija " + s,
                Opis = "Test opis " + s
            };
        }

        public static KategorijaDogadjaja KreirajKategoriju()
        {
            KategorijaDogadjaja kategorija = NapraviKategoriju();
            new UbaciKategorijaDogadjajaSO(kategorija).ExecuteTemplate();
            return kategorija;
        }

        private static string RandomNumbers(int n) => string.Concat(Enumerable.Range(0, n).Select(_ => Random.Shared.Next(10)));

        public static Karta NapraviKartu(Dogadjaj dogadjaj, Konsignator konsignator,
            string sektor = "A", string red = "1", string sediste = "1") => new Karta
        {
            Sektor = sektor,
            Red = red,
            Sediste = sediste,
            NominalnaCena = 5000m,
            Tip = Enum.GetValues<TipKarte>()[0],
            Format = Enum.GetValues<FormatKarte>()[0],
            Dogadjaj = dogadjaj,
            Konsignator = konsignator
        };

        public static Karta KreirajKartu(Dogadjaj dogadjaj, Konsignator konsignator,
            string sektor = "A", string red = "1", string sediste = "1")
        {
            Karta karta = NapraviKartu(dogadjaj, konsignator, sektor, red, sediste);
            new KreirajKartaSO(karta).ExecuteTemplate();
            return karta;
        }

        public static List<Karta> KreirajKarte(Dogadjaj dogadjaj, Konsignator konsignator, int broj)
        {
            string sektor = "S" + UniqueSuffix();

            return Enumerable.Range(1, broj)
                .Select(i => KreirajKartu(dogadjaj, konsignator, sektor, "1", i.ToString()))
                .ToList();
        }

        public static Listing NapraviListing(Broker broker, Konsignator konsignator, List<Karta> karte,
            StatusListinga status = StatusListinga.objavljen,
            TipSplita split = TipSplita.bilo_koja_kolicina,
            int minKolicina = 1) => new Listing
        {
            DatumIsteka = DateTime.Today.AddDays(7),
            Status = status,
            CenaPoKarti = 6000m,
            Split = split,
            MinKolicina = minKolicina,
            ProcenatProvizije = 10m,
            Broker = broker,
            Konsignator = konsignator,
            Karte = karte.Select(k => new Karta { IdKarta = k.IdKarta }).ToList()
        };

        public static Listing KreirajListing(Broker broker, Konsignator konsignator, List<Karta> karte,
            StatusListinga status = StatusListinga.objavljen,
            TipSplita split = TipSplita.bilo_koja_kolicina,
            int minKolicina = 1)
        {
            Listing listing = NapraviListing(broker, konsignator, karte, status, split, minKolicina);
            new KreirajListingSO(listing).ExecuteTemplate();
            return listing;
        }
    }
}
