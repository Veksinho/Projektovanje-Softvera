using Common.Domen;
using Common.Domen.Enumeracije;
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
            if (l.Broker == null || l.Broker.IdBroker <= 0)
                throw new Exception("Broker listinga je obavezan.");
            if (l.Konsignator == null || l.Konsignator.IdKonsignator <= 0)
                throw new Exception("Konsignator listinga je obavezan.");
            if (!l.Status.HasValue)
                throw new Exception("Status listinga je obavezan.");
            if (!l.Split.HasValue)
                throw new Exception("Način prodaje listinga je obavezan.");
            if (l.CenaPoKarti < 0)
                throw new Exception("Cena po karti listinga ne može biti negativna.");
            if (l.ProcenatProvizije < 0 || l.ProcenatProvizije > 100)
                throw new Exception("Procenat provizije listinga mora biti između 0 i 100.");
            if (l.DatumIsteka == default)
                throw new Exception("Datum isteka listinga je obavezan.");
            if (l.Karte == null || l.Karte.Count == 0)
                throw new Exception("Listing mora imati bar jednu kartu.");

            ProveriPoreklo();
            ProveriDatumObjave();
            UcitajKarte();
            ProveriKarte();
            ProveriKolicine();
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

        private void ProveriKolicine()
        {
            int brojKarata = l.Karte.Count;

            if (l.MinKolicina <= 0)
                throw new Exception("Minimalna količina listinga mora biti veća od nule.");
            if (l.MinKolicina > brojKarata)
                throw new Exception("Minimalna količina ne može biti veća od broja karata na listingu.");
            if (l.Split == TipSplita.bez_splita && l.MinKolicina != brojKarata)
                throw new Exception("Kod prodaje bez deljenja minimalna količina mora biti jednaka broju karata.");
            if (l.Split == TipSplita.bilo_koja_kolicina && l.MinKolicina != 1)
                throw new Exception("Kod prodaje u bilo kojoj količini minimalna količina mora biti 1.");
            if (l.Split == TipSplita.parne_kolicine && (brojKarata % 2 != 0 || l.MinKolicina % 2 != 0))
                throw new Exception("Kod prodaje u parnim količinama broj karata i minimalna količina moraju biti parni.");
            if (l.Split == TipSplita.min_kolicina && l.MinKolicina <= 1)
                throw new Exception("Kod prodaje sa minimalnom količinom ta količina mora biti veća od 1.");
            if (l.Split == TipSplita.izbegni_usamljenu && (l.MinKolicina != 1 || brojKarata < 2))
                throw new Exception("Kod izbegavanja usamljene karte listing mora imati bar dve karte, a minimalna količina mora biti 1.");
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
