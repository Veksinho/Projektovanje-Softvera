using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Domen
{
    [JsonDerivedType(typeof(FizickoLice), "FL")]
    [JsonDerivedType(typeof(PravnoLice), "PL")]
    public abstract class Konsignator : IEntity
    {
        public int IdKonsignator { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumRegistracije { get; set; }

        public abstract string Name { get; }

        public override string ToString() => Name;

        public abstract string SubtypeTableName { get; }

        public abstract string SubtypeInsertColumns { get; }

        public abstract string SubtypeInsertValues { get; }

        public abstract string SubtypeUpdateValues { get; }

        public string TableName => "Konsignator k";

        public string Join =>
            "LEFT JOIN PravnoLice pl ON pl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN FizickoLice fl ON fl.idKonsignator = k.idKonsignator";

        public string InsertColumns => "email, telefon, adresa, datumRegistracije";

        public string InsertValues =>
            $"'{Email}', '{Telefon}', " +
            $"'{Adresa} ', ' {DatumRegistracije}'";

        public string UpdateValues =>
            $"email = '{Email}', " +
            $"telefon = '{Telefon}', " +
            $"adresa = '{Adresa}', " +
            $"datumRegistracije = '{DatumRegistracije}'";

        public string PrimaryKeyCondition => $"k.idKonsignator = {IdKonsignator}";

        public string SearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (IdKonsignator > 0)
                    uslovi.Add($"k.idKonsignator = {IdKonsignator}");
                if (!string.IsNullOrWhiteSpace(Email))
                    uslovi.Add($"k.email LIKE '%{Email}%'");
                if (!string.IsNullOrWhiteSpace(Telefon))
                    uslovi.Add($"k.telefon LIKE '%{Telefon}%'");
                if (!string.IsNullOrWhiteSpace(Adresa))
                    uslovi.Add($"k.adresa LIKE '%{Adresa}%'");

                string dodatni = AdditionalSearchCondition;
                if (!string.IsNullOrWhiteSpace(dodatni))
                    uslovi.Add(dodatni);

                return string.Join(" AND ", uslovi);
            }
        }

        protected abstract string AdditionalSearchCondition { get; }

        public void SetId(object id)
        {
            IdKonsignator = Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var lista = new List<IEntity>();

            while (reader.Read())
            {
                Konsignator k;

                if (reader["pib"] != DBNull.Value)
                {
                    k = new PravnoLice
                    {
                        Pib = (string)reader["pib"],
                        MaticniBroj = (string)reader["maticniBroj"],
                        NazivFirme = (string)reader["naziv"]
                    };
                }
                else
                {
                    k = new FizickoLice
                    {
                        Jmbg = (string)reader["jmbg"],
                        Ime = (string)reader["ime"],
                        Prezime = (string)reader["prezime"],
                        BrojLicneKarte = (string)reader["brojLicneKarte"]
                    };
                }

                k.IdKonsignator = (int)reader["idKonsignator"];
                k.Email = (string)reader["email"];
                k.Telefon = (string)reader["telefon"];
                k.Adresa = (string)reader["adresa"];
                k.DatumRegistracije = (DateTime)reader["datumRegistracije"];

                lista.Add(k);
            }

            return lista;
        }
    }
}
