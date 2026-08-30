using Common.Domen;
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
            if (string.IsNullOrWhiteSpace(d.Naziv))
                throw new Exception("Naziv događaja je obavezan.");
            if (string.IsNullOrWhiteSpace(d.Mesto))
                throw new Exception("Mesto održavanja događaja je obavezno.");
            if (d.DatumOdrzavanja == default)
                throw new Exception("Datum održavanja događaja je obavezan.");
            if (d.DatumOdrzavanja.Date < DateTime.Today)
                throw new Exception("Datum održavanja ne može biti u prošlosti.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Add(d);
            Result = d;
        }
    }
}
