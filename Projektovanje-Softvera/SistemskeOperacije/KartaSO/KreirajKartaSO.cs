using Common.Domen;
using Common.Domen.Enumeracije;
using Common.Helpers;
using Common.Validacija;
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

            ThrowIfInvalid(PravilaKarta.Proveri(ka, novi: true));

            ProveriZauzetostSedista();
        }

        private void ProveriZauzetostSedista()
        {
            if (ka.Sektor.Trim() == "-" || ka.Red.Trim() == "-" || ka.Sediste.Trim() == "-")
                return;

            string uslov = $"ka.idDogadjaj = {ka.Dogadjaj!.IdDogadjaj} " +
                           $"AND ka.sektor = N'{Sql.Tekst(ka.Sektor)}' " +
                           $"AND ka.red = N'{Sql.Tekst(ka.Red)}' " +
                           $"AND ka.sediste = N'{Sql.Tekst(ka.Sediste)}'";

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
