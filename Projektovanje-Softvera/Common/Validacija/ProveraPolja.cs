using System.Globalization;

namespace Common.Validacija
{
    internal static class ProveraPolja
    {
        public static readonly string MaxIznosTekst =
            Ogranicenja.MaxIznos.ToString("N2", CultureInfo.GetCultureInfo("sr-Latn-RS"));

        // Obavezno tekstualno polje: prvo obaveznost, pa dužina.
        public static void Tekst(List<GreskaValidacije> greske, string polje, string? vrednost,
            int max, string porukaObavezno, string porukaDuzina)
        {
            if (string.IsNullOrWhiteSpace(vrednost))
                greske.Add(new GreskaValidacije(polje, porukaObavezno));
            else if (!Provera.UDuzini(vrednost, max))
                greske.Add(new GreskaValidacije(polje, porukaDuzina));
        }

        // Obavezno polje sa tačnim brojem cifara (JMBG, PIB, matični broj, broj lične karte).
        public static void Cifre(List<GreskaValidacije> greske, string polje, string? vrednost,
            int duzina, string porukaObavezno, string porukaFormat)
        {
            if (string.IsNullOrWhiteSpace(vrednost))
                greske.Add(new GreskaValidacije(polje, porukaObavezno));
            else if (!Provera.SamoCifre(vrednost, duzina))
                greske.Add(new GreskaValidacije(polje, porukaFormat));
        }

        // Obavezan telefon: opcioni + na prvom mestu, zatim 6–15 cifara.
        public static void Telefon(List<GreskaValidacije> greske, string polje, string? vrednost,
            string porukaObavezno, string porukaFormat)
        {
            if (string.IsNullOrWhiteSpace(vrednost))
                greske.Add(new GreskaValidacije(polje, porukaObavezno));
            else if (!Provera.JeIspravanTelefon(vrednost))
                greske.Add(new GreskaValidacije(polje, porukaFormat));
        }

        // Nenegativan iznos (bez provere znaka): broj decimala, pa gornja granica.
        // Vraća true ako je dodata greška.
        public static bool Iznos(List<GreskaValidacije> greske, string polje, decimal vrednost, string naziv)
        {
            if (!Provera.ImaNajviseDecimala(vrednost, Ogranicenja.BrojDecimala))
            {
                greske.Add(new GreskaValidacije(polje,
                    $"{naziv} može imati najviše {Ogranicenja.BrojDecimala} decimale."));
                return true;
            }

            if (vrednost > Ogranicenja.MaxIznos)
            {
                greske.Add(new GreskaValidacije(polje, $"{naziv} može biti najviše {MaxIznosTekst}."));
                return true;
            }

            return false;
        }
    }
}
