using Common.Domen;

namespace Common.Validacija
{
    public static class PravilaBroker
    {
        public static List<GreskaValidacije> Proveri(Broker b, bool sifraObavezna)
        {
            ArgumentNullException.ThrowIfNull(b);
            var greske = new List<GreskaValidacije>();

            ProveraPolja.Tekst(greske, nameof(Broker.KorisnickoIme), b.KorisnickoIme,
                Ogranicenja.BrokerKorisnickoIme,
                "Korisničko ime brokera je obavezno.",
                $"Korisničko ime brokera može imati najviše {Ogranicenja.BrokerKorisnickoIme} znakova.");

            if (string.IsNullOrWhiteSpace(b.Sifra))
            {
                if (sifraObavezna)
                    greske.Add(new GreskaValidacije(nameof(Broker.Sifra), "Šifra brokera je obavezna."));
            }
            else if (!Provera.JeIspravnaSifra(b.Sifra))
            {
                greske.Add(new GreskaValidacije(nameof(Broker.Sifra), "Šifra brokera nije u odgovarajućem formatu."));
            }

            ProveraPolja.Tekst(greske, nameof(Broker.Ime), b.Ime, Ogranicenja.BrokerIme,
                "Ime brokera je obavezno.",
                $"Ime brokera može imati najviše {Ogranicenja.BrokerIme} znakova.");

            ProveraPolja.Tekst(greske, nameof(Broker.Prezime), b.Prezime, Ogranicenja.BrokerPrezime,
                "Prezime brokera je obavezno.",
                $"Prezime brokera može imati najviše {Ogranicenja.BrokerPrezime} znakova.");

            ProveraPolja.Telefon(greske, nameof(Broker.Telefon), b.Telefon,
                "Telefon brokera je obavezan.",
                "Telefon brokera nije u odgovarajućem formatu.");

            return greske;
        }

        public static List<GreskaValidacije> ProveriPrijavu(Broker b)
        {
            ArgumentNullException.ThrowIfNull(b);
            var greske = new List<GreskaValidacije>();

            if (string.IsNullOrWhiteSpace(b.KorisnickoIme))
                greske.Add(new GreskaValidacije(nameof(Broker.KorisnickoIme), "Korisničko ime je obavezno."));

            if (string.IsNullOrWhiteSpace(b.Sifra))
                greske.Add(new GreskaValidacije(nameof(Broker.Sifra), "Šifra je obavezna."));

            return greske;
        }
    }
}
