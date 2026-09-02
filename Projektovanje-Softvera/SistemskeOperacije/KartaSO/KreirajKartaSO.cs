using Common.Domen;
using Common.Domen.Enumeracije;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KartaSO
{
    public class KreirajKartaSO : SOBase
    {
        private readonly Karta ka;

        public KreirajKartaSO(Karta karta) => this.ka = karta;

        protected override void Validate()
        {
            if (ka == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Karta.");
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

            ProveriZauzetostSedista();
        }

        private void ProveriZauzetostSedista()
        {
            if (ka.Sektor.Trim() == "-" || ka.Red.Trim() == "-" || ka.Sediste.Trim() == "-")
                return;

            string uslov = $"ka.idDogadjaj = {ka.Dogadjaj!.IdDogadjaj} " +
                           $"AND ka.sektor = '{Sql.Tekst(ka.Sektor)}' " +
                           $"AND ka.red = '{Sql.Tekst(ka.Red)}' " +
                           $"AND ka.sediste = '{Sql.Tekst(ka.Sediste)}'";

            if (dbBroker.GetByCondition(new Karta(), uslov).Count > 0)
                throw new Exception("Karta za to sedište na izabranom događaju već postoji.");
        }

        protected override void ExecuteConcreteOperation()
        {
            ka.Status = StatusKarte.u_inventaru;
            ka.Listing = null;
            dbBroker.Add(ka);
            Result = ka;
        }
    }
}
