using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.BrokerSO;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    internal static class TestPodaci
    {
        private static string UniqueSuffix() => Guid.NewGuid().ToString("N")[..8];

        public static Broker KreirajBrokera(string password)
        {
            Broker broker = new Broker
            {
                KorisnickoIme = "test_" + UniqueSuffix(),
                Sifra = password,
                Ime = "Test",
                Prezime = "Broker",
                Telefon = "0601234567"
            };

            new KreirajBrokerSO(broker).ExecuteTemplate();
            return broker;
        }

        public static Dogadjaj KreirajDogadjaj()
        {
            Dogadjaj dogadjaj = new Dogadjaj
            {
                Naziv = "Test događaj " + UniqueSuffix(),
                DatumOdrzavanja = DateTime.Today.AddMonths(1),
                Mesto = "Beograd"
            };

            new UbaciDogadjajSO(dogadjaj).ExecuteTemplate();
            return dogadjaj;
        }

        public static FizickoLice KreirajFizickoLice()
        {
            string s = UniqueSuffix();
            FizickoLice fl = new FizickoLice
            {
                Email = $"test_{s}@test.rs",
                Telefon = "0601234567",
                Adresa = "Test adresa 1",
                Jmbg = RandomNumbers(13),
                Ime = "Test",
                Prezime = "Konsignator",
                BrojLicneKarte = RandomNumbers(9)
            };
            new KreirajKonsignatorSO(fl).ExecuteTemplate();
            return fl;
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
    }
}
