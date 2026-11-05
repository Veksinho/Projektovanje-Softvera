using Common.Domen;
using Common.Helpers;
using Common.Validacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KonsignatorSO
{
    public class PromeniKonsignatorSO : SOBase
    {
        private readonly Konsignator k;

        public PromeniKonsignatorSO(Konsignator konsignator) => this.k = konsignator;

        protected override void Validate()
        {
            if (k == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Konsignator.");
            if (k is not ISpecialization)
                throw new Exception("Konsignator mora biti fizičko ili pravno lice.");
            if (k.IdKonsignator <= 0)
                throw new Exception("Nije prosleđen id konsignatora.");

            ThrowIfInvalid(PravilaKonsignator.Proveri(k));

            ProveriTip();
            ProveriJedinstvenostEmaila();

            if (k is FizickoLice fl)
                ValidirajFizickoLice(fl);
            else if (k is PravnoLice pl)
                ValidirajPravnoLice(pl);
        }

        private void ProveriTip()
        {
            IEntity? sacuvani = dbBroker.GetById(new Konsignator { IdKonsignator = k.IdKonsignator });

            if (sacuvani == null)
                throw new Exception("Sistem ne može da nađe konsignatora.");

            if (sacuvani.GetType() != k.GetType())
                throw new Exception("Tip konsignatora se ne može promeniti.");
        }

        private void ProveriJedinstvenostEmaila()
        {
            List<IEntity> postojeci = dbBroker.GetByCondition(new Konsignator(),
                $"k.email = N'{Sql.Tekst(k.Email)}' AND k.idKonsignator <> {k.IdKonsignator}");

            if (postojeci.Count > 0)
                throw new Exception("Konsignator sa unetim e-mailom već postoji.");
        }

        private void ValidirajFizickoLice(FizickoLice fl)
        {
            List<IEntity> postojeci = dbBroker.GetByCondition(new Konsignator(),
                $"fl.jmbg = N'{Sql.Tekst(fl.Jmbg)}' AND k.idKonsignator <> {fl.IdKonsignator}");

            if (postojeci.Count > 0)
                throw new Exception("Konsignator sa unetim JMBG-om već postoji.");
        }

        private void ValidirajPravnoLice(PravnoLice pl)
        {
            List<IEntity> postojeci = dbBroker.GetByCondition(new Konsignator(),
                $"pl.pib = N'{Sql.Tekst(pl.Pib)}' AND k.idKonsignator <> {pl.IdKonsignator}");

            if (postojeci.Count > 0)
                throw new Exception("Konsignator sa unetim PIB-om već postoji.");
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Edit(k);
            dbBroker.EditSubtype((ISpecialization)k);

            Result = k;
        }
    }
}
