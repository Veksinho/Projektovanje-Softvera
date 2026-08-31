using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.BrokerSO
{
    public class PretraziBrokerSO : SOBase
    {
        private readonly Broker b;

        public PretraziBrokerSO(Broker broker) => this.b = broker;

        protected override void Validate()
        {
            if (b == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Broker.");
            if (b.IdBroker <= 0)
                throw new Exception("Nije prosleđen id brokera.");
        }

        protected override void ExecuteConcreteOperation()
        {
            IEntity? found = dbBroker.GetById(b);

            if (found == null)
                throw new Exception("Sistem ne može da nađe brokera.");

            Result = (Broker)found;
        }
    }
}
