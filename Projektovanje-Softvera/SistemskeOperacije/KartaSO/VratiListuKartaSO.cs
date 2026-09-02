using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KartaSO
{
    public class VratiListuKartaSO : SOBase
    {
        private readonly Karta ka;

        public VratiListuKartaSO(Karta karta) => this.ka = karta;

        protected override void Validate()
        {
            if (ka == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Karta.");
            if (string.IsNullOrWhiteSpace(ka.SearchCondition))
                throw new ArgumentException("Nije prosleđen kriterijum za pretragu.");
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetByCondition(new Karta(), ka.SearchCondition);
            Result = result.Cast<Karta>().ToList();
        }
    }
}
