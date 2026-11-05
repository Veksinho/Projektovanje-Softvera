using Common.Domen;
using Common.Helpers;
using Common.Validacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.KategorijaDogadjajaSO
{
    public class UbaciKategorijaDogadjajaSO : SOBase
    {
        private readonly KategorijaDogadjaja kd;

        public UbaciKategorijaDogadjajaSO(KategorijaDogadjaja category)
        {
            this.kd = category;
        }

        protected override void Validate()
        {
            if (kd == null)
            {
                throw new ArgumentException("Prosledjeni objekat nije instanca klase KategorijaDogadjaja.");
            }

            ThrowIfInvalid(PravilaKategorijaDogadjaja.Proveri(kd));

            List<IEntity> postojece = dbBroker.GetByCondition(
                new KategorijaDogadjaja(), $"kd.naziv = N'{Sql.Tekst(kd.Naziv)}'");

            if (postojece.Count > 0)
            {
                throw new Exception("Kategorija događaja sa unetim nazivom već postoji.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Add(kd);

            Result = kd;
        }
    }
}
