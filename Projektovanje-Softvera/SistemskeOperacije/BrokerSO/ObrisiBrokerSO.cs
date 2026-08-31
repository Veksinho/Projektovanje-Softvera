using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.BrokerSO
{
    public class ObrisiBrokerSO : SOBase
    {
        private readonly Broker b;

        public ObrisiBrokerSO(Broker broker) => this.b = broker;

        protected override void Validate()
        {
            if (b == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Broker.");
            if (b.IdBroker <= 0)
                throw new Exception("Nije prosleđen id brokera.");

            List<IEntity> listinzi = dbBroker.GetByCondition(
                new Listing(), $"l.idBroker = {b.IdBroker}");

            if (listinzi.Count > 0)
                throw new Exception("Broker ima kreirane listinge i ne može se obrisati.");
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> specijalizacije = dbBroker.GetByCondition(
                new BrKd(), $"bkd.idBroker = {b.IdBroker}");

            foreach (IEntity s in specijalizacije)
                dbBroker.Delete(s);

            dbBroker.Delete(b);

            Result = b;
        }
    }
}
