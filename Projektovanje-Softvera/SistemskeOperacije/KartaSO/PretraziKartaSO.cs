using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KartaSO
{
    public class PretraziKartaSO : SOBase
    {
        private readonly Karta ka;

        public PretraziKartaSO(Karta karta) => this.ka = karta;

        protected override void Validate()
        {
            if (ka == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Karta.");
            if (ka.IdKarta <= 0)
                throw new Exception("Nije prosleđen id karte.");
        }

        protected override void ExecuteConcreteOperation()
        {
            IEntity? found = dbBroker.GetById(new Karta { IdKarta = ka.IdKarta });
            if (found == null)
                throw new Exception("Sistem ne može da nađe kartu.");

            Result = (Karta)found;
        }
    }
}
