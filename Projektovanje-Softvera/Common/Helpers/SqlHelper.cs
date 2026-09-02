using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class Sql
    {
        public static string Tekst(string? vrednost) => vrednost == null ? "" : vrednost.Replace("'", "''");
        public static string Broj(decimal vrednost) => vrednost.ToString(CultureInfo.InvariantCulture);
        public static string Broj(decimal? vrednost) => vrednost.HasValue ? Broj(vrednost.Value) : "NULL";
    }
}
