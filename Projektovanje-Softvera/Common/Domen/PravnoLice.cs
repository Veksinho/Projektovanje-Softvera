using Common.Domen.Enumeracije;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class PravnoLice : Konsignator, ISpecialization
    {
        public string Pib { get; set; }
        public string MaticniBroj { get; set; }
        public string NazivFirme { get; set; }

        [JsonIgnore]
        public override string Name => NazivFirme;

        [JsonIgnore] 
        public override string TipPrikaz => EnumHelper.Naziv(TipKonsignatora.pravno_lice);

        [JsonIgnore]
        public string SubtypeTableName => "PravnoLice";

        [JsonIgnore]
        public string SubtypeInsertColumns =>
            "idKonsignator, pib, maticniBroj, naziv";

        [JsonIgnore]
        public string SubtypeInsertValues =>
            $"{IdKonsignator}, N'{Sql.Tekst(Pib)}', " +
            $"N'{Sql.Tekst(MaticniBroj)}', N'{Sql.Tekst(NazivFirme)}'";

        [JsonIgnore]
        public string SubtypeUpdateValues =>
            $"pib = N'{Sql.Tekst(Pib)}', " +
            $"maticniBroj = N'{Sql.Tekst(MaticniBroj)}', " +
            $"naziv = N'{Sql.Tekst(NazivFirme)}'";

        [JsonIgnore]
        public string SubtypePrimaryKeyCondition => $"idKonsignator = {IdKonsignator}";

        protected override string AdditionalSearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (!string.IsNullOrWhiteSpace(Pib))
                    uslovi.Add($"pl.pib LIKE N'%{Sql.Tekst(Pib)}%'");
                if (!string.IsNullOrWhiteSpace(MaticniBroj))
                    uslovi.Add($"pl.maticniBroj LIKE N'%{Sql.Tekst(MaticniBroj)}%'");
                if (!string.IsNullOrWhiteSpace(NazivFirme))
                    uslovi.Add($"pl.naziv LIKE N'%{Sql.Tekst(NazivFirme)}%'");

                return string.Join(" AND ", uslovi);
            }
        }
    }
}
