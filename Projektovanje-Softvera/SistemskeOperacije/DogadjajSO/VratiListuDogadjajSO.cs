using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.DogadjajSO
{
    public class VratiListuDogadjajSO : SOBase
    {
        private readonly Dogadjaj d;

        public VratiListuDogadjajSO(Dogadjaj kriterijum) => this.d = kriterijum;

        protected override void Validate()
        {
            if (d == null)
                throw new ArgumentException("Prosledjeni objekat nije instanca klase Dogadjaj.");
            if (string.IsNullOrWhiteSpace(d.SearchCondition))
                throw new ArgumentException("Nije prosleđen kriterijum za pretragu.");
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetByCondition(new Dogadjaj(), d.SearchCondition);

            Result = result.Cast<Dogadjaj>().ToList();
        }
    }
}
