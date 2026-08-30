using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class Dogadjaj : IEntity
    {
        public int IdDogadjaj { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public string Mesto { get; set; }

        public override string ToString() =>
            $"{Naziv} - {DatumOdrzavanja:dd.MM.yyyy}, {Mesto}";

        public string TableName => "Dogadjaj d";

        public string Join => "";

        public string InsertColumns => "naziv, datumOdrzavanja, mesto";

        public string InsertValues =>
            $"'{Naziv}', '{DatumOdrzavanja}', '{Mesto}'";

        public string UpdateValues =>
            $"naziv = '{Naziv}', " +
            $"datumOdrzavanja = '{DatumOdrzavanja}', " +
            $"mesto = '{Mesto}'";

        public string PrimaryKeyCondition => $"d.idDogadjaj = {IdDogadjaj}";

        public string SearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (IdDogadjaj > 0)
                    uslovi.Add($"d.idDogadjaj = {IdDogadjaj}");
                if (!string.IsNullOrWhiteSpace(Naziv))
                    uslovi.Add($"d.naziv LIKE '%{Naziv}%'");
                if (!string.IsNullOrWhiteSpace(Mesto))
                    uslovi.Add($"d.mesto LIKE '%{Mesto}%'");
                if (DatumOdrzavanja != default)
                    uslovi.Add($"CAST(d.datumOdrzavanja AS DATE) = '{DatumOdrzavanja:yyyy-MM-dd}'");

                return string.Join(" AND ", uslovi);
            }
        }

        public void SetId(object id)
        {
            IdDogadjaj = Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var lista = new List<IEntity>();

            while (reader.Read())
            {
                lista.Add(new Dogadjaj
                {
                    IdDogadjaj = (int)reader["idDogadjaj"],
                    Naziv = (string)reader["naziv"],
                    DatumOdrzavanja = (DateTime)reader["datumOdrzavanja"],
                    Mesto = (string)reader["mesto"]
                });
            }

            return lista;
        }
    }
}
