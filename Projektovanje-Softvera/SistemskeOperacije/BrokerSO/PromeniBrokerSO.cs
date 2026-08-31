using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.BrokerSO
{
    public class PromeniBrokerSO : SOBase
    {
        private readonly Broker b;

        public PromeniBrokerSO(Broker broker) => this.b = broker;

        protected override void Validate()
        {
            if (b == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Broker.");
            if (b.IdBroker <= 0)
                throw new Exception("Nije prosleđen id brokera.");
            if (string.IsNullOrWhiteSpace(b.KorisnickoIme))
                throw new Exception("Korisničko ime brokera je obavezno.");
            if (string.IsNullOrWhiteSpace(b.Ime))
                throw new Exception("Ime brokera je obavezno.");
            if (string.IsNullOrWhiteSpace(b.Prezime))
                throw new Exception("Prezime brokera je obavezno.");
            if (string.IsNullOrWhiteSpace(b.Telefon))
                throw new Exception("Telefon brokera je obavezan.");

            List<IEntity> postojeci = dbBroker.GetByCondition(new Broker(),
                $"b.korisnickoIme = '{b.KorisnickoIme}' AND b.idBroker <> {b.IdBroker}");

            if (postojeci.Count > 0)
                throw new Exception("Broker sa unetim korisničkim imenom već postoji.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Edit(b);

            List<IEntity> stare = dbBroker.GetByCondition(
                new BrKd(), $"bkd.idBroker = {b.IdBroker}");

            foreach (IEntity s in stare)
                dbBroker.Delete(s);

            foreach (BrKd s in b.Specijalizacije)
            {
                if (s.DatumSpecijalizacije == default)
                    s.DatumSpecijalizacije = DateTime.Today;

                dbBroker.Add(s);
            }

            Result = b;
        }
    }
}
