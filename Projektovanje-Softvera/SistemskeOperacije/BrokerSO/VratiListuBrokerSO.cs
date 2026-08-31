using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.BrokerSO
{
    public class VratiListuBrokerSO : SOBase
    {
        private readonly Broker b;

        public VratiListuBrokerSO(Broker kriterijum) => this.b = kriterijum;

        protected override void Validate()
        {
            if (b == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Broker.");
            if (string.IsNullOrWhiteSpace(b.SearchCondition))
                throw new ArgumentException("Nije prosleđen kriterijum za pretragu.");
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetByCondition(new Broker(), b.SearchCondition);

            Result = result.Cast<Broker>().ToList();
        }
    }
}
