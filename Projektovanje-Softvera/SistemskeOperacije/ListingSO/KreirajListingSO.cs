using Common.Domen;
using Common.Domen.Enumeracije;
using Common.Validacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.ListingSO
{
    public class KreirajListingSO : SOBase
    {
        private readonly Listing l;
        private List<Karta> karteIzBaze = new List<Karta>();

        public KreirajListingSO(Listing listing) => this.l = listing;

        protected override void Validate()
        {
            if (l == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Listing.");

            ThrowIfInvalid(PravilaListing.Proveri(l, novi: true));

            UcitajKarte();
            ProveriKarte();
        }

        private void UcitajKarte()
        {
            if (l.Karte.Any(k => k.IdKarta <= 0))
                throw new Exception("Sistem ne može da nađe karte listinga.");

            string identifikatori = string.Join(", ", l.Karte.Select(k => k.IdKarta).Distinct());

            karteIzBaze = dbBroker
                .GetByCondition(new Karta(), $"ka.idKarta IN ({identifikatori})")
                .Cast<Karta>()
                .ToList();

            if (karteIzBaze.Count != l.Karte.Count)
                throw new Exception("Sistem ne može da nađe karte listinga.");
        }

        private void ProveriKarte()
        {
            if (karteIzBaze.Any(k => k.Konsignator == null
                    || k.Konsignator.IdKonsignator != l.Konsignator!.IdKonsignator))
                throw new Exception("Sve karte listinga moraju pripadati istom konsignatoru.");

            if (karteIzBaze.Any(k => k.Dogadjaj == null)
                    || karteIzBaze.Select(k => k.Dogadjaj!.IdDogadjaj).Distinct().Count() > 1)
                throw new Exception("Sve karte listinga moraju biti za isti događaj.");

            if (karteIzBaze.Any(k => k.Listing != null))
                throw new Exception("Karta se već nalazi na drugom listingu.");

            if (l.DatumIsteka.Date > karteIzBaze[0].Dogadjaj!.DatumOdrzavanja.Date)
                throw new Exception("Datum isteka listinga ne može biti posle datuma održavanja događaja.");
        }

        protected override void ExecuteConcreteOperation()
        {
            l.DatumObjave = DateTime.Now;
            dbBroker.Add(l);

            foreach (Karta karta in karteIzBaze)
            {
                karta.Listing = l;
                karta.Status = StatusKarteZa(l.Status!.Value);
                dbBroker.Edit(karta);
            }

            l.Karte = karteIzBaze;
            Result = l;
        }

        private static StatusKarte StatusKarteZa(StatusListinga status) => status switch
        {
            StatusListinga.realizovan => StatusKarte.prodata,
            StatusListinga.povucen => StatusKarte.povucena,
            _ => StatusKarte.plasirana
        };
    }
}
