using Common.Domen;
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
            if (string.IsNullOrWhiteSpace(d.Naziv))
                throw new Exception("Naziv događaja je obavezan.");
            if (string.IsNullOrWhiteSpace(d.Mesto))
                throw new Exception("Mesto održavanja događaja je obavezno.");
            if (d.DatumOdrzavanja == default)
                throw new Exception("Datum održavanja događaja je obavezan.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Edit(d);
            Result = d;
        }
    }
}
