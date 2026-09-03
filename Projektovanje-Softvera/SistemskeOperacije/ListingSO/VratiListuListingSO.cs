using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.ListingSO
{
    public class VratiListuListingSO : SOBase
    {
        private readonly Listing l;

        public VratiListuListingSO(Listing listing) => this.l = listing;

        protected override void Validate()
        {
            if (l == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Listing.");
            if (string.IsNullOrWhiteSpace(l.SearchCondition))
                throw new ArgumentException("Nije prosleđen kriterijum za pretragu.");
        }

        protected override void ExecuteConcreteOperation()
        {
            List<IEntity> rezultat = dbBroker.GetByCondition(new Listing(), l.SearchCondition);
            Result = rezultat.Cast<Listing>().ToList();
        }
    }
}
