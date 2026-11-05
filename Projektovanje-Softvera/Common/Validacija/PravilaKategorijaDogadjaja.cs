using Common.Domen;

namespace Common.Validacija
{
    public static class PravilaKategorijaDogadjaja
    {
        public static List<GreskaValidacije> Proveri(KategorijaDogadjaja kd)
        {
            ArgumentNullException.ThrowIfNull(kd);
            var greske = new List<GreskaValidacije>();

            ProveraPolja.Tekst(greske, nameof(KategorijaDogadjaja.Naziv), kd.Naziv,
                Ogranicenja.KategorijaDogadjajaNaziv,
                "Naziv kategorije događaja je obavezan.",
                $"Naziv kategorije događaja može imati najviše {Ogranicenja.KategorijaDogadjajaNaziv} znakova.");

            ProveraPolja.Tekst(greske, nameof(KategorijaDogadjaja.Opis), kd.Opis,
                Ogranicenja.KategorijaDogadjajaOpis,
                "Opis kategorije događaja je obavezan.",
                $"Opis kategorije događaja može imati najviše {Ogranicenja.KategorijaDogadjajaOpis} znakova.");

            return greske;
        }
    }
}
