using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class PravnoLice : Konsignator
    {
        public string Pib { get; set; }
        public string MaticniBroj { get; set; }
        public string NazivFirme { get; set; }

        public override string Name => NazivFirme;

        public override string SubtypeTableName => "PravnoLice";

        public override string SubtypeInsertColumns =>
            "idKonsignator, pib, maticniBroj, naziv";

        public override string SubtypeInsertValues =>
            $"{IdKonsignator}, '{Pib}', " +
            $"'{MaticniBroj}', '{NazivFirme}'";

        public override string SubtypeUpdateValues =>
            $"pib = '{Pib}', " +
            $"maticniBroj = '{MaticniBroj}', " +
            $"naziv = '{NazivFirme}'";

        protected override string AdditionalSearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (!string.IsNullOrWhiteSpace(Pib))
                    uslovi.Add($"pl.pib LIKE '%{Pib}%'");
                if (!string.IsNullOrWhiteSpace(MaticniBroj))
                    uslovi.Add($"pl.maticniBroj LIKE '%{MaticniBroj}%'");
                if (!string.IsNullOrWhiteSpace(NazivFirme))
                    uslovi.Add($"pl.naziv LIKE '%{NazivFirme}%'");

                return string.Join(" AND ", uslovi);
            }
        }
    }
}
