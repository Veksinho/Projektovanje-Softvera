using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.BrokerSO
{
    public class VratiListuSviBrokerSO : SOBase
    {
        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetAll(new Broker());

            Result = result.Cast<Broker>().ToList();
        }
    }
}
