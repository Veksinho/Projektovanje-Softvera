using Common.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Utils
{
    internal class StavkaTipKonsignatora
    {

        public TipKonsignatora Tip { get; set; }
        public string Naziv { get; set; } = "";

        public override string ToString() => Naziv;

        public static List<StavkaTipKonsignatora> GetAll() => new List<StavkaTipKonsignatora>
        {
            new StavkaTipKonsignatora { Tip = TipKonsignatora.fizicko_lice, Naziv = "Fizičko lice" },
            new StavkaTipKonsignatora { Tip = TipKonsignatora.pravno_lice,  Naziv = "Pravno lice"  }
        };
    }
}
