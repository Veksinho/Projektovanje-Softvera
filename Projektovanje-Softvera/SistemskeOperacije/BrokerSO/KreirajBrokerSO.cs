using Common.Domen;
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
            if (string.IsNullOrWhiteSpace(b.KorisnickoIme))
                throw new Exception("Korisničko ime brokera je obavezno.");
            if (string.IsNullOrWhiteSpace(b.Sifra))
                throw new Exception("Šifra brokera je obavezna.");
            if (string.IsNullOrWhiteSpace(b.Ime))
                throw new Exception("Ime brokera je obavezno.");
            if (string.IsNullOrWhiteSpace(b.Prezime))
                throw new Exception("Prezime brokera je obavezno.");
            if (string.IsNullOrWhiteSpace(b.Telefon))
                throw new Exception("Telefon brokera je obavezan.");

            List<IEntity> postojeci = dbBroker.GetByCondition(
                new Broker(), $"b.korisnickoIme = '{b.KorisnickoIme}'");

            if (postojeci.Count > 0)
                throw new Exception("Broker sa unetim korisničkim imenom već postoji.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Add(b);

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
