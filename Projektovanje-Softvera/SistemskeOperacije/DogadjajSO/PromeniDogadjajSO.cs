using Common.Domen;
using Common.Validacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.DogadjajSO
{
    public class PromeniDogadjajSO : SOBase
    {
        private readonly Dogadjaj d;

        public PromeniDogadjajSO(Dogadjaj dogadjaj) => this.d = dogadjaj;

        protected override void Validate()
        {
            if (d == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Dogadjaj.");
            if (d.IdDogadjaj <= 0)
                throw new Exception("Nije prosleđen id događaja.");

            ThrowIfInvalid(PravilaDogadjaj.Proveri(d, novi: false));
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Edit(d);
            Result = d;
        }
    }
}
