using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KategorijaDogadjajaSO
{
    public class VratiListuSviKategorijaDogadjajaSO : SOBase
    {
        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetAll(new KategorijaDogadjaja());

            Result = result.Cast<KategorijaDogadjaja>().ToList();
        }
    }
}
