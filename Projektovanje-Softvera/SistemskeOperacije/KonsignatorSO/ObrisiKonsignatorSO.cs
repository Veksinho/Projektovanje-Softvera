using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KonsignatorSO
{
    public class ObrisiKonsignatorSO : SOBase
    {
        private readonly Konsignator k;

        public ObrisiKonsignatorSO(Konsignator konsignator) => this.k = konsignator;

        protected override void Validate()
        {
            if (k == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Konsignator.");
            if (k.IdKonsignator <= 0)
                throw new Exception("Nije prosleđen id konsignatora.");

            if (k is not ISpecialization)
                throw new Exception("Konsignator mora biti fizičko ili pravno lice.");

            List<IEntity> karte = dbBroker.GetByCondition(
                new Karta(), $"ka.idKonsignator = {k.IdKonsignator}");

            if (karte.Count > 0)
                throw new Exception("Konsignator ima unete karte i ne može se obrisati.");

            List<IEntity> listinzi = dbBroker.GetByCondition(
                new Listing(), $"l.idKonsignator = {k.IdKonsignator}");

            if (listinzi.Count > 0)
                throw new Exception("Konsignator ima kreirane listinge i ne može se obrisati.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.DeleteSubtype((ISpecialization)k);
            dbBroker.Delete(k);

            Result = k;
        }
    }
}
