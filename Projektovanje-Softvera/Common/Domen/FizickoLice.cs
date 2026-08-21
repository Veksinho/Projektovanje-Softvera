using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class FizickoLice : Konsignator
    {
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojLicneKarte { get; set; }

        public override string Name => $"{Ime} {Prezime}";

        public override string SubtypeTableName => "FizickoLice";

        public override string SubtypeInsertColumns =>
            "idKonsignator, jmbg, ime, prezime, brojLicneKarte";

        public override string SubtypeInsertValues =>
            $"{IdKonsignator}, '{Jmbg}', '{Ime}', " +
            $"'{Prezime}', '{BrojLicneKarte}'";

        public override string SubtypeUpdateValues =>
            $"jmbg = '{Jmbg}', " +
            $"ime = '{Ime}', " +
            $"prezime = '{Prezime}', " +
            $"brojLicneKarte = '{BrojLicneKarte}'";

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
