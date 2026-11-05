using Common.Domen;

namespace Common.Validacija
{
    public static class PravilaDogadjaj
    {
        // novi = true za unos: datum održavanja tada ne sme biti u prošlosti (pri izmeni se ne proverava).
        public static List<GreskaValidacije> Proveri(Dogadjaj d, bool novi)
        {
            ArgumentNullException.ThrowIfNull(d);
            var greske = new List<GreskaValidacije>();

            ProveraPolja.Tekst(greske, nameof(Dogadjaj.Naziv), d.Naziv, Ogranicenja.DogadjajNaziv,
                "Naziv događaja je obavezan.",
                $"Naziv događaja može imati najviše {Ogranicenja.DogadjajNaziv} znakova.");

            ProveraPolja.Tekst(greske, nameof(Dogadjaj.Mesto), d.Mesto, Ogranicenja.DogadjajMesto,
                "Mesto održavanja događaja je obavezno.",
                $"Mesto održavanja događaja može imati najviše {Ogranicenja.DogadjajMesto} znakova.");

            if (d.DatumOdrzavanja == default)
                greske.Add(new GreskaValidacije(nameof(Dogadjaj.DatumOdrzavanja),
                    "Datum održavanja događaja je obavezan."));
            else if (novi && d.DatumOdrzavanja.Date < DateTime.Today)
                greske.Add(new GreskaValidacije(nameof(Dogadjaj.DatumOdrzavanja),
                    "Datum održavanja ne može biti u prošlosti."));

            return greske;
        }
    }
}
