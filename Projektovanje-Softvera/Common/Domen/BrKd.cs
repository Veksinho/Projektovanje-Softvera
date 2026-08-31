using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class BrKd : IEntity
    {
        public Broker Broker { get; set; }
        public KategorijaDogadjaja KategorijaDogadjaja { get; set; }
        public DateTime DatumSpecijalizacije { get; set; }

        public override string ToString() =>
            $"{KategorijaDogadjaja?.Naziv} (od {DatumSpecijalizacije:dd.MM.yyyy.})";

        public string TableName => "BrKd bkd";

        public string Join =>
            "JOIN Broker b ON b.idBroker = bkd.idBroker " +
            "JOIN KategorijaDogadjaja kd ON kd.idKategorijaDogadjaja = bkd.idKategorijaDogadjaja";

        public string InsertColumns => "idBroker, idKategorijaDogadjaja, datumSpecijalizacije";

        public string InsertValues =>
            $"{Broker.IdBroker}, {KategorijaDogadjaja.IdKategorijaDogadjaja}, " +
            $"'{DatumSpecijalizacije:yyyy-MM-dd}'";

        public string UpdateValues =>
            $"datumSpecijalizacije = '{DatumSpecijalizacije:yyyy-MM-dd}'";

        public string PrimaryKeyCondition =>
            $"bkd.idBroker = {Broker.IdBroker} AND " +
            $"bkd.idKategorijaDogadjaja = {KategorijaDogadjaja.IdKategorijaDogadjaja}";

        public string SearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (Broker != null && Broker.IdBroker > 0)
                    uslovi.Add($"bkd.idBroker = {Broker.IdBroker}");
                if (KategorijaDogadjaja != null && KategorijaDogadjaja.IdKategorijaDogadjaja > 0)
                    uslovi.Add($"bkd.idKategorijaDogadjaja = {KategorijaDogadjaja.IdKategorijaDogadjaja}");

                return string.Join(" AND ", uslovi);
            }
        }

        public void SetId(object id) { }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var lista = new List<IEntity>();

            while (reader.Read())
            {
                lista.Add(new BrKd
                {
                    DatumSpecijalizacije = (DateTime)reader["datumSpecijalizacije"],

                    Broker = new Broker
                    {
                        IdBroker = (int)reader["idBroker"],
                        KorisnickoIme = (string)reader["korisnickoIme"],
                        Ime = (string)reader["ime"],
                        Prezime = (string)reader["prezime"],
                        Telefon = (string)reader["telefon"]
                    },

                    KategorijaDogadjaja = new KategorijaDogadjaja
                    {
                        IdKategorijaDogadjaja = (int)reader["idKategorijaDogadjaja"],
                        Naziv = (string)reader["naziv"],
                        Opis = (string)reader["opis"]
                    }
                });
            }

            return lista;
        }
    }
}
