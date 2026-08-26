using Common.Domen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije
{
    public class PrijaviBrokerSO(Broker b) : SOBase
    {
        private readonly Broker b = b;
        public Broker? Result { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(b.KorisnickoIme))
            {
                throw new Exception("Korisničko ime je obavezno.");
            }

            if (string.IsNullOrWhiteSpace(b.Sifra))
            {
                throw new Exception("Šifra je obavezna.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {

            List<IEntity> foundBrokers =
                broker.GetByCondition(b, b.SearchCondition);

            Result = foundBrokers.Cast<Broker>().FirstOrDefault();

            if (Result == null)
            {
                throw new Exception("Pogrešno korisničko ime ili šifra.");
            }
        }
    }
}
