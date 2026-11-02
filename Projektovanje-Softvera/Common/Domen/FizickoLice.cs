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
    public class FizickoLice : Konsignator, ISpecialization
    {
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojLicneKarte { get; set; }

        [JsonIgnore]
        public override string Name => $"{Ime} {Prezime}";

        [JsonIgnore] 
        public override string TipPrikaz => EnumHelper.Naziv(TipKonsignatora.fizicko_lice);

        [JsonIgnore]
        public string SubtypeTableName => "FizickoLice";

        [JsonIgnore]
        public string SubtypeInsertColumns =>
            "idKonsignator, jmbg, ime, prezime, brojLicneKarte";

        [JsonIgnore]
        public string SubtypeInsertValues =>
            $"{IdKonsignator}, N'{Sql.Tekst(Jmbg)}', N'{Sql.Tekst(Ime)}', " +
            $"N'{Sql.Tekst(Prezime)}', N'{Sql.Tekst(BrojLicneKarte)}'";

        [JsonIgnore]
        public string SubtypeUpdateValues =>
            $"jmbg = N'{Sql.Tekst(Jmbg)}', " +
            $"ime = N'{Sql.Tekst(Ime)}', " +
            $"prezime = N'{Sql.Tekst(Prezime)}', " +
            $"brojLicneKarte = N'{Sql.Tekst(BrojLicneKarte)}'";

        [JsonIgnore]
        public string SubtypePrimaryKeyCondition => $"idKonsignator = {IdKonsignator}";

        protected override string AdditionalSearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (!string.IsNullOrWhiteSpace(Jmbg))
                    uslovi.Add($"fl.jmbg LIKE N'%{Sql.Tekst(Jmbg)}%'");
                if (!string.IsNullOrWhiteSpace(Ime))
                    uslovi.Add($"fl.ime LIKE N'%{Sql.Tekst(Ime)}%'");
                if (!string.IsNullOrWhiteSpace(Prezime))
                    uslovi.Add($"fl.prezime LIKE N'%{Sql.Tekst(Prezime)}%'");
                if (!string.IsNullOrWhiteSpace(BrojLicneKarte))
                    uslovi.Add($"fl.brojLicneKarte LIKE N'%{Sql.Tekst(BrojLicneKarte)}%'");

                return string.Join(" AND ", uslovi);
            }
        }
    }
}
