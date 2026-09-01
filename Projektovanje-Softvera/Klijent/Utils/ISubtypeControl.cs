using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Utils
{
    internal interface ISubtypeControl
    {
        Konsignator NapraviPrazan();

        void Popuni(Konsignator k);

        void Procitaj(Konsignator k);

        void SrediFormu(FormMode mode);
    }
}
