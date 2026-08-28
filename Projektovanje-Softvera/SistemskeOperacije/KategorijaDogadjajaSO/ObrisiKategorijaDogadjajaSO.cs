using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KategorijaDogadjajaSO
{
    public class ObrisiKategorijaDogadjajaSO : SOBase
    {
        private readonly KategorijaDogadjaja kd;

        public ObrisiKategorijaDogadjajaSO(KategorijaDogadjaja kd)
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

            string condition = $"bkd.idKategorijaDogadjaja = {kd.IdKategorijaDogadjaja}";

            List<IEntity> specializations = dbBroker.GetByCondition(new BrKd(), condition);

            if (specializations.Count > 0)
            {
                throw new Exception(
                    "Kategorija događaja je dodeljena bar jednom brokeru i ne može se obrisati.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Delete(kd);

            Result = kd;
        }
    }
}
