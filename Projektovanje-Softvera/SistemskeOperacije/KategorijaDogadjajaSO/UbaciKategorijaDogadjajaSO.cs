using Common.Domen;
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

            if (string.IsNullOrWhiteSpace(kd.Naziv))
            {
                throw new Exception("Naziv kategorije događaja je obavezan.");
            }

            if (string.IsNullOrWhiteSpace(kd.Opis))
            {
                throw new Exception("Opis kategorije događaja je obavezan.");
            }
        }

        protected override void ExecuteConcreteOperation()
        {
            dbBroker.Add(kd);

            Result = kd;
        }
    }
}
