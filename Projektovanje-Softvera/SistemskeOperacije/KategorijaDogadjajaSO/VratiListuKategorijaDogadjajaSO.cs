using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KategorijaDogadjajaSO
{
    public class VratiListuKategorijaDogadjajaSO : SOBase
    {
        private readonly KategorijaDogadjaja kd;

        public VratiListuKategorijaDogadjajaSO(KategorijaDogadjaja kd)
        {
            this.kd = kd;
        }

        protected override void Validate()
        {
            if (kd == null)
            {
                throw new ArgumentException("Prosledjeni objekat nije instanca klase KategorijaDogadjaja.");
            }

            if (string.IsNullOrWhiteSpace(kd.SearchCondition))
            {
                throw new ArgumentException("Nije prosleđen kriterijum za pretragu.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetByCondition(new KategorijaDogadjaja(), kd.SearchCondition);

            Result = result.Cast<KategorijaDogadjaja>().ToList();
        }
    }
}
