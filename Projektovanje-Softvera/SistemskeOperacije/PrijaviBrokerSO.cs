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

        protected override void Validate()
        {
            if (b == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Broker.");

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
                dbBroker.GetByCondition(b, b.SearchCondition);

            if (foundBrokers.Count == 0)
            {
                throw new Exception("Korisničko ime ili šifra nisu ispravni.");
            }

            Result = foundBrokers.Cast<Broker>().FirstOrDefault();
        }
    }
}
