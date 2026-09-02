using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen.Enumeracije
{
    public enum StatusKarte
    {
        [Description("U inventaru")] u_inventaru,
        [Description("Plasirana")] plasirana,
        [Description("Prodata")] prodata,
        [Description("Povučena")] povucena
    }
}
