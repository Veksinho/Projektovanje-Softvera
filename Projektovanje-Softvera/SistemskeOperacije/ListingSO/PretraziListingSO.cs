using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.ListingSO
{
    public class PretraziListingSO : SOBase
    {
        private readonly Listing l;

        public PretraziListingSO(Listing listing) => this.l = listing;

        protected override void Validate()
        {
            if (l == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Listing.");
            if (l.IdListing <= 0)
                throw new Exception("Nije prosleđen id listinga.");
        }

        protected override void ExecuteConcreteOperation()
        {
            IEntity? nadjeni = dbBroker.GetById(new Listing { IdListing = l.IdListing });
            if (nadjeni == null)
                throw new Exception("Sistem ne može da nađe listing.");

            Result = (Listing)nadjeni;
        }
    }
}
