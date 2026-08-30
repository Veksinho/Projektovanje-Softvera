using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.DogadjajSO
{
    public class PretraziDogadjajSO : SOBase
    {
        private readonly Dogadjaj d;

        public PretraziDogadjajSO(Dogadjaj dogadjaj) => this.d = dogadjaj;

        protected override void Validate()
        {
            if (d == null)
                throw new ArgumentException("Prosledjeni objekat nije instanca klase Dogadjaj.");
            if (d.IdDogadjaj <= 0)
                throw new Exception("Nije prosleđen id događaja.");
        }

        protected override void ExecuteConcreteOperation()
        {
            IEntity? found = dbBroker.GetById(d);

            if (found == null)
                throw new Exception("Sistem ne može da nađe događaj.");

            Result = (Dogadjaj)found;
        }
    }
}
