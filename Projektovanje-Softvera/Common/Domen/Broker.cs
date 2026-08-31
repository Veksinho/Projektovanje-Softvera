using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        [JsonIgnore]
        public string SpecijalizacijePrikaz => string.Join(", ", Specijalizacije
            .Where(s => s.KategorijaDogadjaja != null)
            .Select(s => s.KategorijaDogadjaja.Naziv));

        public override string ToString() => $"{Ime} {Prezime} ({KorisnickoIme})";

        public string TableName => "Broker b";

        public string Join =>
            "LEFT JOIN BrKd bkd ON b.idBroker = bkd.idBroker " +
            "LEFT JOIN KategorijaDogadjaja kd ON bkd.idKategorijaDogadjaja = kd.idKategorijaDogadjaja";

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

                if (Specijalizacije != null
                    && Specijalizacije.Count > 0
                    && Specijalizacije[0].KategorijaDogadjaja != null
                    && Specijalizacije[0].KategorijaDogadjaja.IdKategorijaDogadjaja > 0)
                {
                    uslovi.Add($"b.idBroker IN (SELECT idBroker FROM BrKd " +
                        $"WHERE idKategorijaDogadjaja = " +
                        $"{Specijalizacije[0].KategorijaDogadjaja.IdKategorijaDogadjaja})");
                }

                return string.Join(" AND ", uslovi);
            }
        }

        public void SetId(object id)
        {
            IdBroker = Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var brokerMap = new Dictionary<int, Broker>();

            while (reader.Read())
            {
                int id = (int)reader["idBroker"];

                if (!brokerMap.TryGetValue(id, out Broker b))
                {
                    b = new Broker
                    {
                        IdBroker = id,
                        KorisnickoIme = (string)reader["korisnickoIme"],
                        Sifra = (string)reader["sifra"],
                        Ime = (string)reader["ime"],
                        Prezime = (string)reader["prezime"],
                        Telefon = (string)reader["telefon"]
                    };

                    brokerMap.Add(id, b);
                }

                if (reader["idKategorijaDogadjaja"] != DBNull.Value)
                {
                    b.Specijalizacije.Add(new BrKd
                    {
                        Broker = b,
                        KategorijaDogadjaja = new KategorijaDogadjaja
                        {
                            IdKategorijaDogadjaja = (int)reader["idKategorijaDogadjaja"],
                            Naziv = (string)reader["naziv"],
                            Opis = (string)reader["opis"]
                        },
                        DatumSpecijalizacije = (DateTime)reader["datumSpecijalizacije"]
                    });
                }
            }

            return brokerMap.Values.OrderBy(x => x.IdBroker).Cast<IEntity>().ToList();
        }
    }
}
