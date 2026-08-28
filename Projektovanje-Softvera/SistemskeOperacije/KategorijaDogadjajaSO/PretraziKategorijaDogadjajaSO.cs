using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KategorijaDogadjajaSO
{
    public class PretraziKategorijaDogadjajaSO : SOBase
    {
        private readonly KategorijaDogadjaja kd;

        public PretraziKategorijaDogadjajaSO(KategorijaDogadjaja kd)
        {
            this.kd = kd;
        }

        protected override void Validate()
        {
            if (kd == null)
            {
                throw new ArgumentException("Prosledjeni objekat nije instanca klase KategorijaDogadjaja.");
            }

            if (kd.IdKategorijaDogadjaja <= 0)
            {
                throw new Exception("Nije prosledjen id kategorije događaja.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {
            IEntity? found = dbBroker.GetById(kd);

            if (found == null)
            {
                throw new Exception("Sistem ne može da nađe kategoriju događaja.");
            }

            Result = (KategorijaDogadjaja)found;
        }
    }
}
