using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KartaSO
{
    public class VratiListuSviKartaSO : SOBase
    {
        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetAll(new Karta());

            Result = result.Cast<Karta>().ToList();
        }
    }
}
