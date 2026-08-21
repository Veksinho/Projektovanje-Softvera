using Common.Domen.Enumeracije;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class Karta : IEntity
    {
        public int IdKarta { get; set; }
        public string Sektor { get; set; }
        public string Red { get; set; }
        public string Sediste { get; set; }
        public decimal NominalnaCena { get; set; }
        public TipKarte? Tip { get; set; }
        public FormatKarte? Format { get; set; }
        public StatusKarte? Status { get; set; }

        public Listing? Listing { get; set; }
        public Konsignator Konsignator { get; set; }
        public Dogadjaj Dogadjaj { get; set; }

        public override string ToString() =>
            $"Sektor {Sektor}, red {Red}, sediste {Sediste}";

        public string TableName => "Karta ka";

        public string Join =>
            "LEFT JOIN Listing l ON l.idListing = ka.idListing " +
            "JOIN Konsignator k ON k.idKonsignator = ka.idKonsignator " +
            "LEFT JOIN PravnoLice pl ON pl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN FizickoLice fl ON fl.idKonsignator = k.idKonsignator " +
            "JOIN Dogadjaj d ON d.idDogadjaj = ka.idDogadjaj";

        public string InsertColumns =>
            "sektor, red, sediste, nominalnaCena, tip, format, status, " +
            "idListing, idKonsignator, idDogadjaj";

        public string InsertValues =>
            $"'{Sektor}', '{Red}', '{Sediste}', " +
            $"{NominalnaCena}, '{Tip}', '{Format}', '{Status}', " +
            $"{ListingIdOrNull}, {Konsignator.IdKonsignator}, {Dogadjaj.IdDogadjaj}";

        public string UpdateValues =>
            $"sektor = '{Sektor}', " +
            $"red = '{Red}', " +
            $"sediste = '{Sediste}', " +
            $"nominalnaCena = {NominalnaCena}, " +
            $"tip = '{Tip}', " +
            $"format = '{Format}', " +
            $"status = '{Status}', " +
            $"idListing = {ListingIdOrNull}, " +
            $"idKonsignator = {Konsignator.IdKonsignator}, " +
            $"idDogadjaj = {Dogadjaj.IdDogadjaj}";

        private string ListingIdOrNull =>
            Listing == null || Listing.IdListing == 0
                ? "NULL"
                : Listing.IdListing.ToString();

        public string PrimaryKeyCondition => $"ka.idKarta = {IdKarta}";

        public string SearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (IdKarta > 0)
                    uslovi.Add($"ka.idKarta = {IdKarta}");
                if (!string.IsNullOrWhiteSpace(Sektor))
                    uslovi.Add($"ka.sektor LIKE '%{Sektor}%'");
                if (!string.IsNullOrWhiteSpace(Red))
                    uslovi.Add($"ka.red LIKE '%{Red}%'");
                if (!string.IsNullOrWhiteSpace(Sediste))
                    uslovi.Add($"ka.sediste LIKE '%{Sediste}%'");
                if (Tip.HasValue)
                    uslovi.Add($"ka.tip = '{Tip}'");
                if (Format.HasValue)
                    uslovi.Add($"ka.format = '{Format}'");
                if (Status.HasValue)
                    uslovi.Add($"ka.status = '{Status}'");
                if (Listing != null && Listing.IdListing > 0)
                    uslovi.Add($"ka.idListing = {Listing.IdListing}");
                if (Konsignator != null && Konsignator.IdKonsignator > 0)
                    uslovi.Add($"ka.idKonsignator = {Konsignator.IdKonsignator}");
                if (Dogadjaj != null && Dogadjaj.IdDogadjaj > 0)
                    uslovi.Add($"ka.idDogadjaj = {Dogadjaj.IdDogadjaj}");

                return string.Join(" AND ", uslovi);
            }
        }

        public void SetId(object id)
        {
            IdKarta = Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var lista = new List<IEntity>();

            while (reader.Read())
            {
                var karta = new Karta
                {
                    IdKarta = (int)reader["idKarta"],
                    Sektor = (string)reader["sektor"],
                    Red = (string)reader["red"],
                    Sediste = (string)reader["sediste"],
                    NominalnaCena = (decimal)reader["nominalnaCena"],
                    Tip = Enum.Parse<TipKarte>((string)reader["tip"]),
                    Format = Enum.Parse<FormatKarte>((string)reader["format"]),
                    Status = Enum.Parse<StatusKarte>((string)reader["status"]),

                    Dogadjaj = new Dogadjaj { IdDogadjaj = (int)reader["idDogadjaj"] }
                };

                if (reader["idListing"] != DBNull.Value)
                    karta.Listing = new Listing { IdListing = (int)reader["idListing"] };

                int idKons = (int)reader["idKonsignator"];
                karta.Konsignator = reader["pib"] != DBNull.Value
                    ? new PravnoLice { IdKonsignator = idKons }
                    : new FizickoLice { IdKonsignator = idKons };

                lista.Add(karta);
            }

            return lista;
        }
    }
}
