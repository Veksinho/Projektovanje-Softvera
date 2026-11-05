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
    public class PromeniKategorijaDogadjajaSO : SOBase
    {
        private readonly KategorijaDogadjaja kd;

        public PromeniKategorijaDogadjajaSO(KategorijaDogadjaja kd)
        {
            this.kd = kd;
        }

        protected override void Validate()
        {
            if (kd == null)
            {
                throw new ArgumentException("Prosleđeni objekat nije instanca klase KategorijaDogadjaja.");
            }

            if (kd.IdKategorijaDogadjaja <= 0)
            {
                throw new Exception("Nije prosleđen id kategorije događaja.");
            }

            ThrowIfInvalid(PravilaKategorijaDogadjaja.Proveri(kd));

            List<IEntity> postojece = dbBroker.GetByCondition(new KategorijaDogadjaja(),
                $"kd.naziv = N'{Sql.Tekst(kd.Naziv)}' AND kd.idKategorijaDogadjaja <> {kd.IdKategorijaDogadjaja}");

            if (postojece.Count > 0)
            {
                throw new Exception("Kategorija događaja sa unetim nazivom već postoji.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Edit(kd);

            Result = kd;
        }
    }
}
