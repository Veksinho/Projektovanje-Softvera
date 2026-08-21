using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class KategorijaDogadjaja : IEntity
    {
        public int IdKategorijaDogadjaja { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public override string ToString() => Naziv;

        public string TableName => "KategorijaDogadjaja kd";

        public string Join => "";

        public string InsertColumns => "naziv, opis";

        public string InsertValues => $"'{Naziv}', '{Opis}'";

        public string UpdateValues => $"naziv = '{Naziv}', opis = '{Opis}'";

        public string PrimaryKeyCondition => $"kd.idKategorijaDogadjaja = {IdKategorijaDogadjaja}";

        public string SearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (IdKategorijaDogadjaja > 0)
                    uslovi.Add($"kd.idKategorijaDogadjaja = {IdKategorijaDogadjaja}");
                if (!string.IsNullOrWhiteSpace(Naziv))
                    uslovi.Add($"kd.naziv LIKE '%{Naziv}%'");
                if (!string.IsNullOrWhiteSpace(Opis))
                    uslovi.Add($"kd.opis LIKE '%{Opis}%'");

                return string.Join(" AND ", uslovi);
            }
        }

        public void SetId(object id)
        {
            IdKategorijaDogadjaja = System.Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var lista = new List<IEntity>();

            while (reader.Read())
            {
                lista.Add(new KategorijaDogadjaja
                {
                    IdKategorijaDogadjaja = (int)reader["idKategorijaDogadjaja"],
                    Naziv = (string)reader["naziv"],
                    Opis = (string)reader["opis"]
                });
            }

            return lista;
        }
    }
}
