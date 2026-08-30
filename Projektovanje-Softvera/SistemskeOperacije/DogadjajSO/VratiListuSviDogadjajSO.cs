using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.DogadjajSO
{
    public class VratiListuSviDogadjajSO : SOBase
    {
        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetAll(new Dogadjaj());

            Result = result.Cast<Dogadjaj>().ToList();
        }
    }
}
