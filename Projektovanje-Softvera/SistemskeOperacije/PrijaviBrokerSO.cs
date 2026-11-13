using Common.Domen;
using Common.Helpers;
using Common.Validacija;
using SistemskeOperacije.Bezbednost;
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

            ThrowIfInvalid(PravilaBroker.ProveriPrijavu(b));
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> foundBrokers = dbBroker.GetByCondition(
                new Broker(), $"b.korisnickoIme = N'{Sql.Tekst(b.KorisnickoIme)}'");

            Broker? foundBroker = foundBrokers.Cast<Broker>().FirstOrDefault();

            if (!PasswordHasher.Verify(b.Sifra, foundBroker?.SifraHash))
                throw new Exception("Korisničko ime ili šifra nisu ispravni.");

            Result = foundBroker;
        }
    }
}
