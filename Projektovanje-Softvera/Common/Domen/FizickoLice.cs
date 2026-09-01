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
        public override string TipPrikaz => "Fizičko lice";

        [JsonIgnore]
        public string SubtypeTableName => "FizickoLice";

        [JsonIgnore]
        public string SubtypeInsertColumns =>
            "idKonsignator, jmbg, ime, prezime, brojLicneKarte";

        [JsonIgnore]
        public string SubtypeInsertValues =>
            $"{IdKonsignator}, '{Jmbg}', '{Ime}', " +
            $"'{Prezime}', '{BrojLicneKarte}'";

        [JsonIgnore]
        public string SubtypeUpdateValues =>
            $"jmbg = '{Jmbg}', " +
            $"ime = '{Ime}', " +
            $"prezime = '{Prezime}', " +
            $"brojLicneKarte = '{BrojLicneKarte}'";

        [JsonIgnore]
        public string SubtypePrimaryKeyCondition => $"idKonsignator = {IdKonsignator}";

        protected override string AdditionalSearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (!string.IsNullOrWhiteSpace(Jmbg))
                    uslovi.Add($"fl.jmbg LIKE '%{Jmbg}%'");
                if (!string.IsNullOrWhiteSpace(Ime))
                    uslovi.Add($"fl.ime LIKE '%{Ime}%'");
                if (!string.IsNullOrWhiteSpace(Prezime))
                    uslovi.Add($"fl.prezime LIKE '%{Prezime}%'");
                if (!string.IsNullOrWhiteSpace(BrojLicneKarte))
                    uslovi.Add($"fl.brojLicneKarte LIKE '%{BrojLicneKarte}%'");

                return string.Join(" AND ", uslovi);
            }
        }
    }
}
