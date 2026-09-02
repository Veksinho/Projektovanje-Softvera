using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Utils
{
    internal class StavkaEnuma<T> where T : struct, Enum
    {
        public T Vrednost { get; set; }

        public override string ToString() => EnumHelper.Naziv(Vrednost);

        public static List<StavkaEnuma<T>> GetAll()
        {
            List<StavkaEnuma<T>> stavke = new List<StavkaEnuma<T>>();

            foreach (T vrednost in Enum.GetValues<T>())
                stavke.Add(new StavkaEnuma<T> { Vrednost = vrednost });

            return stavke;
        }
    }
}
