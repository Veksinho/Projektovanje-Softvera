using Common.Domen;
using Common.Helpers;
using Common.Validacija;
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

            ThrowIfInvalid(PravilaKarta.Proveri(ka, novi: false));

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
                           $"AND ka.sektor = N'{Sql.Tekst(ka.Sektor)}' " +
                           $"AND ka.red = N'{Sql.Tekst(ka.Red)}' " +
                           $"AND ka.sediste = N'{Sql.Tekst(ka.Sediste)}' " +
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
