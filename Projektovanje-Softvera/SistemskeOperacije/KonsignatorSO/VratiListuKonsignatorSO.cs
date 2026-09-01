using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KonsignatorSO
{
    public class VratiListuKonsignatorSO : SOBase
    {
        private readonly Konsignator k;

        public VratiListuKonsignatorSO(Konsignator kriterijum) => this.k = kriterijum;

        protected override void Validate()
        {
            if (k == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Konsignator.");
            if (string.IsNullOrWhiteSpace(k.SearchCondition))
                throw new ArgumentException("Nije prosleđen kriterijum za pretragu.");
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> result = dbBroker.GetByCondition(new Konsignator(), k.SearchCondition);

            Result = result.Cast<Konsignator>().ToList();
        }
    }
}
