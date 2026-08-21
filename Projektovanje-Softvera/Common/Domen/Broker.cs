using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class Broker : IEntity
    {
        public int IdBroker { get; set; }
        public string KorisnickoIme { get; set; }
        public string Sifra { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }

        public List<BrKd> Specijalizacije { get; set; } = new List<BrKd>();

        public override string ToString() => $"{Ime} {Prezime}";

        public string TableName => "Broker b";

        public string Join => "";

        public string InsertColumns => "korisnickoIme, sifra, ime, prezime, telefon";

        public string InsertValues =>
            $"'{KorisnickoIme}', '{Sifra}', " +
            $"'{Ime}', '{Prezime}', '{Telefon}'";

        public string UpdateValues =>
            $"korisnickoIme = '{KorisnickoIme}', " +
            $"sifra = '{Sifra}', " +
            $"ime = '{Ime}', " +
            $"prezime = '{Prezime}', " +
            $"telefon = '{Telefon}'";

        public string PrimaryKeyCondition => $"b.idBroker = {IdBroker}";

        public string SearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (IdBroker > 0)
                    uslovi.Add($"b.idBroker = {IdBroker}");

                if (!string.IsNullOrWhiteSpace(Sifra))
                {
                    uslovi.Add($"b.korisnickoIme = '{KorisnickoIme}'");
                    uslovi.Add($"b.sifra = '{Sifra}'");
                }
                else if (!string.IsNullOrWhiteSpace(KorisnickoIme))
                {
                    uslovi.Add($"b.korisnickoIme LIKE '%{KorisnickoIme}%'");
                }

                if (!string.IsNullOrWhiteSpace(Ime))
                    uslovi.Add($"b.ime LIKE '%{Ime}%'");
                if (!string.IsNullOrWhiteSpace(Prezime))
                    uslovi.Add($"b.prezime LIKE '%{Prezime}%'");
                if (!string.IsNullOrWhiteSpace(Telefon))
                    uslovi.Add($"b.telefon LIKE '%{Telefon}%'");

                return string.Join(" AND ", uslovi);
            }
        }

        public void SetId(object id)
        {
            IdBroker = Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var lista = new List<IEntity>();

            while (reader.Read())
            {
                lista.Add(new Broker
                {
                    IdBroker = (int)reader["idBroker"],
                    KorisnickoIme = (string)reader["korisnickoIme"],
                    Sifra = (string)reader["sifra"],
                    Ime = (string)reader["ime"],
                    Prezime = (string)reader["prezime"],
                    Telefon = (string)reader["telefon"]
                });
            }

            return lista;
        }
    }
}
