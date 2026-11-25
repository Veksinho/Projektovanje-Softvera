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
    public class KreirajKonsignatorSO : SOBase
    {
        private readonly Konsignator k;

        public KreirajKonsignatorSO(Konsignator konsignator) => this.k = konsignator;

        protected override void Validate()
        {
            if (k == null)
                throw new ArgumentException("Prosleđeni objekat nije instanca klase Konsignator.");

            if (k is not ISpecialization)
                throw new Exception("Konsignator mora biti fizičko ili pravno lice.");

            ThrowIfInvalid(PravilaKonsignator.Proveri(k));

            ProveriJedinstvenostEmaila();

            if (k is FizickoLice fl)
                ValidirajFizickoLice(fl);
            else if (k is PravnoLice pl)
                ValidirajPravnoLice(pl);
        }

        private void ProveriJedinstvenostEmaila()
        {
            List<IEntity> postojeci = dbBroker.GetByCondition(
                new Konsignator(), $"k.email = N'{Sql.Tekst(k.Email)}'");

            if (postojeci.Count > 0)
                throw new Exception("Konsignator sa unetim e-mailom već postoji.");
        }

        private void ValidirajFizickoLice(FizickoLice fl)
        {
            List<IEntity> postojeci = dbBroker.GetByCondition(
                new Konsignator(), $"fl.jmbg = N'{Sql.Tekst(fl.Jmbg)}'");

            if (postojeci.Count > 0)
                throw new Exception("Konsignator sa unetim JMBG-om već postoji.");
        }

        private void ValidirajPravnoLice(PravnoLice pl)
        {
            List<IEntity> postojeci = dbBroker.GetByCondition(
                new Konsignator(), $"pl.pib = N'{Sql.Tekst(pl.Pib)}'");

            if (postojeci.Count > 0)
                throw new Exception("Konsignator sa unetim PIB-om već postoji.");
        }

        protected override void ExecuteConcreteOperation()
        {
            k.DatumRegistracije = DateTime.Today;

            dbBroker.Add(k);
            dbBroker.AddSubtype((ISpecialization)k);

            Result = k;
        }
    }
}
