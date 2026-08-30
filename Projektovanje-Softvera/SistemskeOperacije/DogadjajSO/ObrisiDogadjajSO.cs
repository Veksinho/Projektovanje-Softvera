using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.DogadjajSO
{
    public class ObrisiDogadjajSO : SOBase
    {
        private readonly Dogadjaj d;

        public ObrisiDogadjajSO(Dogadjaj dogadjaj) => this.d = dogadjaj;

        protected override void Validate()
        {
            if (d == null)
                throw new ArgumentException("Prosledjeni objekat nije instanca klase Dogadjaj.");
            if (d.IdDogadjaj <= 0)
                throw new Exception("Nije prosleđen id događaja.");

            string condition = $"ka.idDogadjaj = {d.IdDogadjaj}";
            List<IEntity> karte = dbBroker.GetByCondition(new Karta(), condition);

            if (karte.Count > 0)
                throw new Exception("Za događaj postoje unete karte i ne može se obrisati.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Delete(d);
            Result = d;
        }
    }
}
