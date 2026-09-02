using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen.Enumeracije
{
    public enum TipSplita
    {
        [Description("Bez deljenja")] bez_splita,
        [Description("Bilo koja količina")] bilo_koja_kolicina,
        [Description("Parne količine")] parne_kolicine,
        [Description("Minimalna količina")] min_kolicina,
        [Description("Izbegni usamljenu kartu")] izbegni_usamljenu
    }
}
