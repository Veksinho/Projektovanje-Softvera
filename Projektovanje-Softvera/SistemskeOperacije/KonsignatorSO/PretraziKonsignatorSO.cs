using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KonsignatorSO
{
    public class PretraziKonsignatorSO : SOBase
    {
        private readonly Konsignator k;

        public PretraziKonsignatorSO(Konsignator konsignator) => this.k = konsignator;

        protected override void Validate()
        {
            if (k == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Konsignator.");
            if (k.IdKonsignator <= 0)
                throw new Exception("Nije prosleđen id konsignatora.");
        }

        protected override void ExecuteConcreteOperation()
        {
            IEntity? found = dbBroker.GetById(new Konsignator { IdKonsignator = k.IdKonsignator });

            if (found == null)
                throw new Exception("Sistem ne može da nađe konsignatora.");

            Result = (Konsignator)found;
        }
    }
}
