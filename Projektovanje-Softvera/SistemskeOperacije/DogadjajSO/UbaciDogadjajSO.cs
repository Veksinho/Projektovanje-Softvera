using Common.Domen;
using Common.Validacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.DogadjajSO
{
    public class UbaciDogadjajSO : SOBase
    {
        private readonly Dogadjaj d;

        public UbaciDogadjajSO(Dogadjaj dogadjaj) => this.d = dogadjaj;

        protected override void Validate()
        {
            if (d == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Dogadjaj.");

            ThrowIfInvalid(PravilaDogadjaj.Proveri(d, novi: true));
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Add(d);
            Result = d;
        }
    }
}
