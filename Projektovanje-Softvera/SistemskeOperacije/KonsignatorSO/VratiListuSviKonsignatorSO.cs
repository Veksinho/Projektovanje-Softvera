using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KonsignatorSO
{
    public class VratiListuSviKonsignatorSO : SOBase
    {
        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetAll(new Konsignator());

            Result = result.Cast<Konsignator>().ToList();
        }
    }
}
