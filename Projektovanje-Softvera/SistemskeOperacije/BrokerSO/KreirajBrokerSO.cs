using Common.Domen;
using Common.Helpers;
using Common.Validacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.BrokerSO
{
    public class KreirajBrokerSO : SOBase
    {
        private readonly Broker b;

        public KreirajBrokerSO(Broker broker) => this.b = broker;

        protected override void Validate()
        {
            if (b == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Broker.");

            ThrowIfInvalid(PravilaBroker.Proveri(b, sifraObavezna: true));

            List<IEntity> postojeci = dbBroker.GetByCondition(
                new Broker(), $"b.korisnickoIme = N'{Sql.Tekst(b.KorisnickoIme)}'");

            if (postojeci.Count > 0)
                throw new Exception("Broker sa unetim korisničkim imenom već postoji.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Add(b);

            foreach (BrokerKategorijaDogadjaja s in b.Specijalizacije)
            {
                if (s.DatumSpecijalizacije == default)
                    s.DatumSpecijalizacije = DateTime.Today;

                dbBroker.Add(s);
            }

            Result = b;
        }
    }
}
