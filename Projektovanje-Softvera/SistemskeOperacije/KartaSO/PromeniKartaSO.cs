using Common.Domen;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KartaSO
{
    public class PromeniKartaSO : SOBase
    {
        private readonly Karta ka;
        private Karta? sacuvana;

        public PromeniKartaSO(Karta karta) => this.ka = karta;

        protected override void Validate()
        {
            if (ka == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Karta.");
            if (ka.IdKarta <= 0)
                throw new Exception("Nije prosleđen id karte.");
            if (ka.Konsignator == null || ka.Konsignator.IdKonsignator <= 0)
                throw new Exception("Konsignator karte je obavezan.");
            if (ka.Dogadjaj == null || ka.Dogadjaj.IdDogadjaj <= 0)
                throw new Exception("Događaj karte je obavezan.");
            if (string.IsNullOrWhiteSpace(ka.Sektor))
                throw new Exception("Sektor karte je obavezan.");
            if (string.IsNullOrWhiteSpace(ka.Red))
                throw new Exception("Red karte je obavezan.");
            if (string.IsNullOrWhiteSpace(ka.Sediste))
                throw new Exception("Sedište karte je obavezno.");
            if (ka.NominalnaCena < 0)
                throw new Exception("Nominalna cena karte ne može biti negativna.");
            if (!ka.Tip.HasValue)
                throw new Exception("Tip karte je obavezan.");
            if (!ka.Format.HasValue)
                throw new Exception("Format karte je obavezan.");
            if (!ka.Status.HasValue)
                throw new Exception("Status karte je obavezan.");

            ProveriPoreklo();
            ProveriZauzetostSedista();
        }

        private void ProveriPoreklo()
        {
            IEntity? found = dbBroker.GetById(new Karta { IdKarta = ka.IdKarta });
            if (found == null)
                throw new Exception("Sistem ne može da nađe kartu.");

            sacuvana = (Karta)found;

            if (sacuvana.Konsignator!.IdKonsignator != ka.Konsignator!.IdKonsignator)
                throw new Exception("Konsignator karte se ne može promeniti.");
            if (sacuvana.Dogadjaj!.IdDogadjaj != ka.Dogadjaj!.IdDogadjaj)
                throw new Exception("Događaj karte se ne može promeniti.");
        }

        private void ProveriZauzetostSedista()
        {
            if (ka.Sektor.Trim() == "-" || ka.Red.Trim() == "-" || ka.Sediste.Trim() == "-")
                return;

            string uslov = $"ka.idDogadjaj = {ka.Dogadjaj!.IdDogadjaj} " +
                           $"AND ka.sektor = '{Sql.Tekst(ka.Sektor)}' " +
                           $"AND ka.red = '{Sql.Tekst(ka.Red)}' " +
                           $"AND ka.sediste = '{Sql.Tekst(ka.Sediste)}' " +
                           $"AND ka.idKarta <> {ka.IdKarta}";

            if (dbBroker.GetByCondition(new Karta(), uslov).Count > 0)
                throw new Exception("Karta za to sedište na izabranom događaju već postoji.");
        }

        protected override void ExecuteConcreteOperation()
        {
            ka.Listing = sacuvana!.Listing;
            dbBroker.Edit(ka);
            Result = ka;
        }
    }
}
