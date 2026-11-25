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
    public class PromeniListingSO : SOBase
    {
        private readonly Listing l;
        private Listing? sacuvani;
        private bool objavljujeSe;
        private List<Karta> karteIzBaze = new List<Karta>();

        public PromeniListingSO(Listing listing) => this.l = listing;

        protected override void Validate()
        {
            if (l == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Listing.");
            if (l.IdListing <= 0)
                throw new Exception("Nije prosleđen id listinga.");

            ThrowIfInvalid(PravilaListing.Proveri(l, novi: false));

            ProveriPoreklo();
            ProveriDatumObjave();
            UcitajKarte();
            ProveriKarte();
        }

        private void ProveriPoreklo()
        {
            IEntity? nadjeni = dbBroker.GetById(new Listing { IdListing = l.IdListing });
            if (nadjeni == null)
                throw new Exception("Sistem ne može da nađe listing.");

            sacuvani = (Listing)nadjeni;

            if (sacuvani.Broker!.IdBroker != l.Broker!.IdBroker)
                throw new Exception("Broker listinga se ne može promeniti.");
            if (sacuvani.Konsignator!.IdKonsignator != l.Konsignator!.IdKonsignator)
                throw new Exception("Konsignator listinga se ne može promeniti.");

            objavljujeSe = sacuvani.Status != StatusListinga.objavljen
                && l.Status == StatusListinga.objavljen;
        }

        private void ProveriDatumObjave()
        {
            DateTime datumObjave = objavljujeSe ? DateTime.Today : sacuvani!.DatumObjave.Date;

            if (l.DatumIsteka.Date <= datumObjave)
                throw new Exception("Datum isteka listinga mora biti posle datuma objave.");
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

            if (karteIzBaze.Any(k => k.Listing != null && k.Listing.IdListing != l.IdListing))
                throw new Exception("Karta se već nalazi na drugom listingu.");

            if (l.DatumIsteka.Date > karteIzBaze[0].Dogadjaj!.DatumOdrzavanja.Date)
                throw new Exception("Datum isteka listinga ne može biti posle datuma održavanja događaja.");
        }

        protected override void ExecuteConcreteOperation()
        {
            l.DatumObjave = objavljujeSe ? DateTime.Now : sacuvani!.DatumObjave;
            dbBroker.Edit(l);

            foreach (IEntity entitet in dbBroker.GetByCondition(new Karta(), $"ka.idListing = {l.IdListing}"))
            {
                Karta stara = (Karta)entitet;
                stara.Listing = null;
                stara.Status = StatusKarte.u_inventaru;
                dbBroker.Edit(stara);
            }

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
