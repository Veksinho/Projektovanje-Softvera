using Common.Domen;
using Common.Domen.Enumeracije;

namespace Common.Validacija
{
    public static class PravilaListing
    {
        // novi = true za kreiranje: datum isteka se tada poredi sa današnjim danom,
        // a status može biti samo nacrt ili objavljen.
        public static List<GreskaValidacije> Proveri(Listing l, bool novi)
        {
            ArgumentNullException.ThrowIfNull(l);
            var greske = new List<GreskaValidacije>();

            if (l.Broker == null || l.Broker.IdBroker <= 0)
                greske.Add(new GreskaValidacije(nameof(Listing.Broker), "Broker listinga je obavezan."));

            if (l.Konsignator == null || l.Konsignator.IdKonsignator <= 0)
                greske.Add(new GreskaValidacije(nameof(Listing.Konsignator), "Konsignator listinga je obavezan."));

            if (!l.Status.HasValue)
                greske.Add(new GreskaValidacije(nameof(Listing.Status), "Status listinga je obavezan."));
            else if (novi && l.Status != StatusListinga.nacrt && l.Status != StatusListinga.objavljen)
                greske.Add(new GreskaValidacije(nameof(Listing.Status), "Novi listing može biti samo nacrt ili objavljen."));

            if (!l.Split.HasValue)
                greske.Add(new GreskaValidacije(nameof(Listing.Split), "Način prodaje listinga je obavezan."));

            if (l.CenaPoKarti <= 0)
                greske.Add(new GreskaValidacije(nameof(Listing.CenaPoKarti),
                    "Cena po karti listinga mora biti veća od nule."));
            else
                ProveraPolja.Iznos(greske, nameof(Listing.CenaPoKarti), l.CenaPoKarti, "Cena po karti listinga");

            if (!Provera.JeUOpsegu(l.ProcenatProvizije, 0m, Ogranicenja.MaxProvizija))
                greske.Add(new GreskaValidacije(nameof(Listing.ProcenatProvizije),
                    "Procenat provizije listinga mora biti između 0 i 100."));
            else if (!Provera.ImaNajviseDecimala(l.ProcenatProvizije, Ogranicenja.BrojDecimala))
                greske.Add(new GreskaValidacije(nameof(Listing.ProcenatProvizije),
                    $"Procenat provizije listinga može imati najviše {Ogranicenja.BrojDecimala} decimale."));

            if (!Provera.UDuzini(l.Napomena, Ogranicenja.ListingNapomena))
                greske.Add(new GreskaValidacije(nameof(Listing.Napomena),
                    $"Napomena listinga može imati najviše {Ogranicenja.ListingNapomena} znakova."));

            ProveriDatumIsteka(l, novi, greske);

            if (l.Karte == null || l.Karte.Count == 0)
                greske.Add(new GreskaValidacije(nameof(Listing.Karte), "Listing mora imati bar jednu kartu."));
            else
                ProveriMinKolicinu(l, greske);

            return greske;
        }

        public static List<GreskaValidacije> ProveriKriterijum(Listing kriterijum)
        {
            ArgumentNullException.ThrowIfNull(kriterijum);
            var greske = new List<GreskaValidacije>();

            bool cenaOdIspravna = ProveriCenuKriterijuma(greske, nameof(Listing.CenaOd), kriterijum.CenaOd, "Cena od");
            bool cenaDoIspravna = ProveriCenuKriterijuma(greske, nameof(Listing.CenaDo), kriterijum.CenaDo, "Cena do");

            if (cenaOdIspravna && cenaDoIspravna
                && kriterijum.CenaOd > 0 && kriterijum.CenaDo > 0
                && kriterijum.CenaOd > kriterijum.CenaDo)
                greske.Add(new GreskaValidacije(nameof(Listing.CenaOd), "Cena od ne može biti veća od cene do."));

            if (kriterijum.DatumObjaveOd != default && kriterijum.DatumObjaveDo != default
                && kriterijum.DatumObjaveOd.Date > kriterijum.DatumObjaveDo.Date)
                greske.Add(new GreskaValidacije(nameof(Listing.DatumObjaveOd),
                    "Datum objave od ne može biti posle datuma objave do."));

            return greske;
        }

        private static void ProveriDatumIsteka(Listing l, bool novi, List<GreskaValidacije> greske)
        {
            if (l.DatumIsteka == default)
            {
                greske.Add(new GreskaValidacije(nameof(Listing.DatumIsteka), "Datum isteka listinga je obavezan."));
                return;
            }

            bool preObjave = novi
                ? l.DatumIsteka.Date <= DateTime.Today
                : l.DatumObjave != default && l.DatumIsteka.Date <= l.DatumObjave.Date;

            if (preObjave)
                greske.Add(new GreskaValidacije(nameof(Listing.DatumIsteka),
                    "Datum isteka listinga mora biti posle datuma objave."));
        }

        private static void ProveriMinKolicinu(Listing l, List<GreskaValidacije> greske)
        {
            int brojKarata = l.Karte.Count;

            string? poruka = null;

            if (l.MinKolicina <= 0)
                poruka = "Minimalna količina listinga mora biti veća od nule.";
            else if (l.MinKolicina > brojKarata)
                poruka = "Minimalna količina ne može biti veća od broja karata na listingu.";
            else
                poruka = l.Split switch
                {
                    TipSplita.bez_splita when l.MinKolicina != brojKarata =>
                        "Kod prodaje bez deljenja minimalna količina mora biti jednaka broju karata.",
                    TipSplita.bilo_koja_kolicina when l.MinKolicina != 1 =>
                        "Kod prodaje u bilo kojoj količini minimalna količina mora biti 1.",
                    TipSplita.parne_kolicine when brojKarata % 2 != 0 || l.MinKolicina % 2 != 0 =>
                        "Kod prodaje u parnim količinama broj karata i minimalna količina moraju biti parni.",
                    TipSplita.min_kolicina when l.MinKolicina <= 1 =>
                        "Kod prodaje sa minimalnom količinom ta količina mora biti veća od 1.",
                    TipSplita.izbegni_usamljenu when l.MinKolicina != 1 || brojKarata < 2 =>
                        "Kod izbegavanja usamljene karte listing mora imati bar dve karte, a minimalna količina mora biti 1.",
                    _ => null
                };

            if (poruka != null)
                greske.Add(new GreskaValidacije(nameof(Listing.MinKolicina), poruka));
        }

        // Vraća true ako je vrednost ispravna (0 znači da kriterijum nije unet).
        private static bool ProveriCenuKriterijuma(List<GreskaValidacije> greske, string polje,
            decimal vrednost, string naziv)
        {
            if (vrednost < 0)
            {
                greske.Add(new GreskaValidacije(polje, $"{naziv} ne može biti negativna."));
                return false;
            }

            return !ProveraPolja.Iznos(greske, polje, vrednost, naziv);
        }
    }
}
