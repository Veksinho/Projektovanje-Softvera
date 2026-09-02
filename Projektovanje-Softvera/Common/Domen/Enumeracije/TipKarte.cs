using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen.Enumeracije
{
    public enum TipKarte
    {
        [Description("Stajaća")] stajaca,
        [Description("Sedeća")] sedeca,
        [Description("VIP")] vip,
        [Description("Sky box")] sky_box
    }
}
