using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class EnumHelper
    {
        private static readonly ConcurrentDictionary<Enum, string> kes = new ConcurrentDictionary<Enum, string>();

        public static string Naziv(Enum? vrednost)
        {
            if (vrednost == null)
                return "";

            return kes.GetOrAdd(vrednost, v =>
            {
                FieldInfo? polje = v.GetType().GetField(v.ToString());
                DescriptionAttribute? opis = polje?.GetCustomAttribute<DescriptionAttribute>();
                return opis != null ? opis.Description : v.ToString();
            });
        }
    }
}
