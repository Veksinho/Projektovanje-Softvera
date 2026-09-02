using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen.Enumeracije
{
    public enum FormatKarte
    {
        [Description("Papirna")] papirna,
        [Description("PDF")] pdf,
        [Description("Mobilna")] mobilna,
        [Description("RFID")] rfid
    }
}
