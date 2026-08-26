using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Komunikacija
{
    public class Odgovor
    {
        public bool Uspesno { get; set; }

        public string? Greska { get; set; }

        public object? Objekat { get; set; }

        public Odgovor()
        {
            Uspesno = true;
        }

        public static Odgovor Uspeh(object? objekat)
        {
            return new Odgovor
            {
                Uspesno = true,
                Greska = null,
                Objekat = objekat
            };
        }

        public static Odgovor Neuspeh(string greska)
        {
            return new Odgovor
            {
                Uspesno = false,
                Greska = greska,
                Objekat = null
            };
        }
    }
}
