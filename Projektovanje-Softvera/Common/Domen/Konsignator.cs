using Common.Domen.Enumeracije;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Domen
{
    [JsonDerivedType(typeof(Konsignator), "K")]
    [JsonDerivedType(typeof(FizickoLice), "FL")]
    [JsonDerivedType(typeof(PravnoLice), "PL")]
    public class Konsignator : IEntity
    {
        public int IdKonsignator { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumRegistracije { get; set; }

        public TipKonsignatora? TipKriterijum { get; set; }
        public string? NazivKriterijum { get; set; }

        [JsonIgnore]
        public virtual string Name => "";

        [JsonIgnore]
        public virtual string TipPrikaz => "";

        public override string ToString() => Name;

        [JsonIgnore]
        public string TableName => "Konsignator k";

        [JsonIgnore]
        public string Join =>
            "LEFT JOIN PravnoLice pl ON pl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN FizickoLice fl ON fl.idKonsignator = k.idKonsignator";

        [JsonIgnore]
        public string InsertColumns => "email, telefon, adresa, datumRegistracije";

        [JsonIgnore]
        public string InsertValues =>
            $"'{Email}', '{Telefon}', " +
            $"'{Adresa}', '{DatumRegistracije:yyyy-MM-dd}'";

        [JsonIgnore]
        public string UpdateValues =>
            $"email = '{Email}', " +
            $"telefon = '{Telefon}', " +
            $"adresa = '{Adresa}'";

        [JsonIgnore]
        public string PrimaryKeyCondition => $"k.idKonsignator = {IdKonsignator}";

        [JsonIgnore]
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

                if (TipKriterijum == TipKonsignatora.fizicko_lice)
                    uslovi.Add("fl.idKonsignator IS NOT NULL");
                else if (TipKriterijum == TipKonsignatora.pravno_lice)
                    uslovi.Add("pl.idKonsignator IS NOT NULL");

                if (!string.IsNullOrWhiteSpace(NazivKriterijum))
                    uslovi.Add($"(fl.ime LIKE '%{NazivKriterijum}%' " +
                               $"OR fl.prezime LIKE '%{NazivKriterijum}%' " +
                               $"OR pl.naziv LIKE '%{NazivKriterijum}%')");

                string dodatni = AdditionalSearchCondition;
                if (!string.IsNullOrWhiteSpace(dodatni))
                    uslovi.Add(dodatni);

                return string.Join(" AND ", uslovi);
            }
        }

        protected virtual string AdditionalSearchCondition => "";

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
                else if (reader["jmbg"] != DBNull.Value)
                {
                    k = new FizickoLice
                    {
                        Jmbg = (string)reader["jmbg"],
                        Ime = (string)reader["ime"],
                        Prezime = (string)reader["prezime"],
                        BrojLicneKarte = (string)reader["brojLicneKarte"]
                    };
                }
                else
                {
                    throw new Exception(
                        $"Konsignator {reader["idKonsignator"]} nema podatke o tipu.");
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
