using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen.Enumeracije
{
    public enum TipKonsignatora
    {
        [Description("Fizičko lice")] fizicko_lice,
        [Description("Pravno lice")] pravno_lice
    }
}
