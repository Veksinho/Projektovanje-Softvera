using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Komunikacija
{
    public class Zahtev
    {
        public Operacija Operacija { get; set; }

        public object? Objekat { get; set; }

        public Zahtev()
        {
        }

        public Zahtev(Operacija operacija, object? objekat)
        {
            Operacija = operacija;
            Objekat = objekat;
        }
    }
}
