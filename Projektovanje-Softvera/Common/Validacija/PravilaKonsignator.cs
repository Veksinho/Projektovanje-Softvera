using Common.Domen;

namespace Common.Validacija
{
    public static class PravilaKonsignator
    {
        public static List<GreskaValidacije> Proveri(Konsignator k)
        {
            ArgumentNullException.ThrowIfNull(k);
            // Klijent uvek šalje podtip; drugačiji objekat je programska greška, ne greška unosa.
            if (k is not FizickoLice and not PravnoLice)
                throw new ArgumentException("Konsignator mora biti fizičko ili pravno lice.", nameof(k));

            var greske = new List<GreskaValidacije>();

            if (string.IsNullOrWhiteSpace(k.Email))
                greske.Add(new GreskaValidacije(nameof(Konsignator.Email), "E-mail konsignatora je obavezna."));
            else if (!Provera.UDuzini(k.Email, Ogranicenja.KonsignatorEmail))
                greske.Add(new GreskaValidacije(nameof(Konsignator.Email),
                    $"E-mail konsignatora može imati najviše {Ogranicenja.KonsignatorEmail} znakova."));
            else if (!Provera.JeIspravanEmail(k.Email))
                greske.Add(new GreskaValidacije(nameof(Konsignator.Email),
                    "E-mail konsignatora nije u odgovarajućem formatu."));

            ProveraPolja.Telefon(greske, nameof(Konsignator.Telefon), k.Telefon,
                "Telefon konsignatora je obavezan.",
                "Telefon konsignatora nije u odgovarajućem formatu.");

            ProveraPolja.Tekst(greske, nameof(Konsignator.Adresa), k.Adresa, Ogranicenja.KonsignatorAdresa,
                "Adresa konsignatora je obavezna.",
                $"Adresa konsignatora može imati najviše {Ogranicenja.KonsignatorAdresa} znakova.");

            if (k is FizickoLice fl)
                ProveriFizickoLice(fl, greske);
            else if (k is PravnoLice pl)
                ProveriPravnoLice(pl, greske);

            return greske;
        }

        private static void ProveriFizickoLice(FizickoLice fl, List<GreskaValidacije> greske)
        {
            ProveraPolja.Cifre(greske, nameof(FizickoLice.Jmbg), fl.Jmbg, Ogranicenja.JmbgDuzina,
                "JMBG fizičkog lica je obavezan.",
                $"JMBG fizičkog lica mora imati tačno {Ogranicenja.JmbgDuzina} cifara.");

            ProveraPolja.Tekst(greske, nameof(FizickoLice.Ime), fl.Ime, Ogranicenja.FizickoLiceIme,
                "Ime fizičkog lica je obavezno.",
                $"Ime fizičkog lica može imati najviše {Ogranicenja.FizickoLiceIme} znakova.");

            ProveraPolja.Tekst(greske, nameof(FizickoLice.Prezime), fl.Prezime, Ogranicenja.FizickoLicePrezime,
                "Prezime fizičkog lica je obavezno.",
                $"Prezime fizičkog lica može imati najviše {Ogranicenja.FizickoLicePrezime} znakova.");

            ProveraPolja.Cifre(greske, nameof(FizickoLice.BrojLicneKarte), fl.BrojLicneKarte,
                Ogranicenja.BrojLicneKarteDuzina,
                "Broj lične karte fizičkog lica je obavezan.",
                $"Broj lične karte fizičkog lica mora imati tačno {Ogranicenja.BrojLicneKarteDuzina} cifara.");
        }

        private static void ProveriPravnoLice(PravnoLice pl, List<GreskaValidacije> greske)
        {
            ProveraPolja.Tekst(greske, nameof(PravnoLice.NazivFirme), pl.NazivFirme, Ogranicenja.PravnoLiceNaziv,
                "Naziv pravnog lica je obavezan.",
                $"Naziv pravnog lica može imati najviše {Ogranicenja.PravnoLiceNaziv} znakova.");

            ProveraPolja.Cifre(greske, nameof(PravnoLice.Pib), pl.Pib, Ogranicenja.PibDuzina,
                "PIB pravnog lica je obavezan.",
                $"PIB pravnog lica mora imati tačno {Ogranicenja.PibDuzina} cifara.");

            ProveraPolja.Cifre(greske, nameof(PravnoLice.MaticniBroj), pl.MaticniBroj, Ogranicenja.MaticniBrojDuzina,
                "Matični broj pravnog lica je obavezan.",
                $"Matični broj pravnog lica mora imati tačno {Ogranicenja.MaticniBrojDuzina} cifara.");
        }
    }
}
