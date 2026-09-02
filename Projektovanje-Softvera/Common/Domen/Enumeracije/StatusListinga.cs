using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen.Enumeracije
{
    public enum StatusListinga
    {
        [Description("Nacrt")] nacrt,
        [Description("Objavljen")] objavljen,
        [Description("Povučen")] povucen,
        [Description("Realizovan")] realizovan
    }
}
