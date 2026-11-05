using Common.Domen;

namespace Common.Validacija
{
    public static class PravilaKarta
    {
        // novi = true za kreiranje: status se tada ne proverava, jer ga postavlja sistem.
        public static List<GreskaValidacije> Proveri(Karta ka, bool novi)
        {
            ArgumentNullException.ThrowIfNull(ka);
            var greske = new List<GreskaValidacije>();

            if (ka.Konsignator == null || ka.Konsignator.IdKonsignator <= 0)
                greske.Add(new GreskaValidacije(nameof(Karta.Konsignator), "Konsignator karte je obavezan."));

            if (ka.Dogadjaj == null || ka.Dogadjaj.IdDogadjaj <= 0)
                greske.Add(new GreskaValidacije(nameof(Karta.Dogadjaj), "Događaj karte je obavezan."));

            ProveraPolja.Tekst(greske, nameof(Karta.Sektor), ka.Sektor, Ogranicenja.KartaSektor,
                "Sektor karte je obavezan.",
                $"Sektor karte može imati najviše {Ogranicenja.KartaSektor} znakova.");

            ProveraPolja.Tekst(greske, nameof(Karta.Red), ka.Red, Ogranicenja.KartaRed,
                "Red karte je obavezan.",
                $"Red karte može imati najviše {Ogranicenja.KartaRed} znakova.");

            ProveraPolja.Tekst(greske, nameof(Karta.Sediste), ka.Sediste, Ogranicenja.KartaSediste,
                "Sedište karte je obavezno.",
                $"Sedište karte može imati najviše {Ogranicenja.KartaSediste} znakova.");

            if (ka.NominalnaCena < 0)
                greske.Add(new GreskaValidacije(nameof(Karta.NominalnaCena),
                    "Nominalna cena karte ne može biti negativna."));
            else
                ProveraPolja.Iznos(greske, nameof(Karta.NominalnaCena), ka.NominalnaCena, "Nominalna cena karte");

            if (!ka.Tip.HasValue)
                greske.Add(new GreskaValidacije(nameof(Karta.Tip), "Tip karte je obavezan."));

            if (!ka.Format.HasValue)
                greske.Add(new GreskaValidacije(nameof(Karta.Format), "Format karte je obavezan."));

            if (!novi && !ka.Status.HasValue)
                greske.Add(new GreskaValidacije(nameof(Karta.Status), "Status karte je obavezan."));

            return greske;
        }
    }
}
