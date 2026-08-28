using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KategorijaDogadjajaSO
{
    public class PromeniKategorijaDogadjajaSO : SOBase
    {
        private readonly KategorijaDogadjaja kd;

        public PromeniKategorijaDogadjajaSO(KategorijaDogadjaja kd)
        {
            this.kd = kd;
        }

        protected override void Validate()
        {
            if (kd == null)
            {
                throw new ArgumentException("Prosleđeni objekat nije instanca klase KategorijaDogadjaja.");
            }

            if (kd.IdKategorijaDogadjaja <= 0)
            {
                throw new Exception("Nije prosleđen id kategorije događaja.");
            }

            if (string.IsNullOrWhiteSpace(kd.Naziv))
            {
                throw new Exception("Naziv kategorije događaja je obavezan.");
            }

            if (string.IsNullOrWhiteSpace(kd.Opis))
            {
                throw new Exception("Opis kategorije događaja je obavezan.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Edit(kd);

            Result = kd;
        }
    }
}
