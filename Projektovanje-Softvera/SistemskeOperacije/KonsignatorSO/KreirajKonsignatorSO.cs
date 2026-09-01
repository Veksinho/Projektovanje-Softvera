using Common.Domen;
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

            if (string.IsNullOrWhiteSpace(k.Email))
                throw new Exception("E-mail konsignatora je obavezna.");
            if (!k.Email.Contains('@'))
                throw new Exception("E-mail konsignatora nije u odgovarajućem formatu.");
            if (string.IsNullOrWhiteSpace(k.Telefon))
                throw new Exception("Telefon konsignatora je obavezan.");
            if (string.IsNullOrWhiteSpace(k.Adresa))
                throw new Exception("Adresa konsignatora je obavezna.");

            if (k is FizickoLice fl)
                ValidirajFizickoLice(fl);
            else if (k is PravnoLice pl)
                ValidirajPravnoLice(pl);
        }

        private void ValidirajFizickoLice(FizickoLice fl)
        {
            if (string.IsNullOrWhiteSpace(fl.Jmbg))
                throw new Exception("JMBG fizičkog lica je obavezan.");
            if (string.IsNullOrWhiteSpace(fl.Ime))
                throw new Exception("Ime fizičkog lica je obavezno.");
            if (string.IsNullOrWhiteSpace(fl.Prezime))
                throw new Exception("Prezime fizičkog lica je obavezno.");
            if (string.IsNullOrWhiteSpace(fl.BrojLicneKarte))
                throw new Exception("Broj lične karte fizičkog lica je obavezan.");

            List<IEntity> postojeci = dbBroker.GetByCondition(
                new Konsignator(), $"fl.jmbg = '{fl.Jmbg}'");

            if (postojeci.Count > 0)
                throw new Exception("Konsignator sa unetim JMBG-om već postoji.");
        }

        private void ValidirajPravnoLice(PravnoLice pl)
        {
            if (string.IsNullOrWhiteSpace(pl.NazivFirme))
                throw new Exception("Naziv pravnog lica je obavezan.");
            if (string.IsNullOrWhiteSpace(pl.Pib))
                throw new Exception("PIB pravnog lica je obavezan.");
            if (string.IsNullOrWhiteSpace(pl.MaticniBroj))
                throw new Exception("Matični broj pravnog lica je obavezan.");

            List<IEntity> postojeci = dbBroker.GetByCondition(
                new Konsignator(), $"pl.pib = '{pl.Pib}'");

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
