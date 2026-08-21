using Common.Domen.Enumeracije;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public class Listing : IEntity
    {
        public int IdListing { get; set; }
        public DateTime DatumObjave { get; set; }
        public DateTime DatumIsteka { get; set; }
        public StatusListinga? Status { get; set; }
        public decimal CenaPoKarti { get; set; }
        public TipSplita? Split { get; set; }
        public int MinKolicina { get; set; }
        public decimal ProcenatProvizije { get; set; }
        public string? Napomena { get; set; }

        public Broker Broker { get; set; }
        public Konsignator Konsignator { get; set; }

        public List<Karta> Karte { get; set; } = new List<Karta>();

        public decimal UkupnaCena => CenaPoKarti * Karte.Count;

        public decimal Provizija => UkupnaCena * ProcenatProvizije / 100m;

        public override string ToString() =>
            $"Listing #{IdListing} ({Status}, {Karte.Count} karata)";

        public string TableName => "Listing l";

        public string Join =>
            "JOIN Broker b ON b.idBroker = l.idBroker " +
            "JOIN Konsignator k ON k.idKonsignator = l.idKonsignator " +
            "LEFT JOIN PravnoLice pl ON pl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN FizickoLice fl ON fl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN Karta ka ON ka.idListing = l.idListing " +
            "LEFT JOIN Dogadjaj d ON d.idDogadjaj = ka.idDogadjaj";

        public string InsertColumns =>
            "datumObjave, datumIsteka, status, cenaPoKarti, split, " +
            "minKolicina, procenatProvizije, napomena, idBroker, idKonsignator";

        public string InsertValues =>
            $"'{DatumObjave}', '{DatumIsteka}', " +
            $"'{Status}', {CenaPoKarti}, '{Split}', " +
            $"{MinKolicina}, {ProcenatProvizije}, {NoteOrNull}, " +
            $"{Broker.IdBroker}, {Konsignator.IdKonsignator}";

        public string UpdateValues =>
            $"datumObjave = '{DatumObjave}', " +
            $"datumIsteka = '{DatumIsteka}', " +
            $"status = '{Status}', " +
            $"cenaPoKarti = {CenaPoKarti}, " +
            $"split = '{Split}', " +
            $"minKolicina = {MinKolicina}, " +
            $"procenatProvizije = {ProcenatProvizije}, " +
            $"napomena = {NoteOrNull}, " +
            $"idBroker = {Broker.IdBroker}, " +
            $"idKonsignator = {Konsignator.IdKonsignator}";

        private string NoteOrNull =>
            string.IsNullOrWhiteSpace(Napomena) ? "NULL" : $"'{Napomena}'";

        public string PrimaryKeyCondition => $"l.idListing = {IdListing}";

        public string SearchCondition
        {
            get
            {
                var uslovi = new List<string>();

                if (IdListing > 0)
                    uslovi.Add($"l.idListing = {IdListing}");
                if (Status.HasValue)
                    uslovi.Add($"l.status = '{Status}'");
                if (Split.HasValue)
                    uslovi.Add($"l.split = '{Split}'");
                if (DatumObjave != default)
                    uslovi.Add($"CAST(l.datumObjave AS DATE) >= '{DatumObjave:yyyy-MM-dd}'");
                if (DatumIsteka != default)
                    uslovi.Add($"CAST(l.datumIsteka AS DATE) <= '{DatumIsteka:yyyy-MM-dd}'");
                if (CenaPoKarti > 0)
                    uslovi.Add($"l.cenaPoKarti <= {CenaPoKarti}");
                if (!string.IsNullOrWhiteSpace(Napomena))
                    uslovi.Add($"l.napomena LIKE '%{Napomena}%'");
                if (Broker != null && Broker.IdBroker > 0)
                    uslovi.Add($"l.idBroker = {Broker.IdBroker}");
                if (Konsignator != null && Konsignator.IdKonsignator > 0)
                    uslovi.Add($"l.idKonsignator = {Konsignator.IdKonsignator}");

                return string.Join(" AND ", uslovi);
            }
        }

        public void SetId(object id)
        {
            IdListing = Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var lista = new List<IEntity>();
            var vidjeni = new HashSet<int>();

            while (reader.Read())
            {
                int id = (int)reader["idListing"];
                if (!vidjeni.Add(id))
                    continue;

                var listing = new Listing
                {
                    IdListing = id,
                    DatumObjave = (DateTime)reader["datumObjave"],
                    DatumIsteka = (DateTime)reader["datumIsteka"],
                    Status = Enum.Parse<StatusListinga>((string)reader["status"]),
                    CenaPoKarti = (decimal)reader["cenaPoKarti"],
                    Split = Enum.Parse<TipSplita>((string)reader["split"]),
                    MinKolicina = (int)reader["minKolicina"],
                    ProcenatProvizije = (decimal)reader["procenatProvizije"],
                    Napomena = reader["napomena"] == DBNull.Value
                                   ? null
                                   : (string)reader["napomena"],

                    Broker = new Broker { IdBroker = (int)reader["idBroker"] }
                };

                int idKons = (int)reader["idKonsignator"];
                listing.Konsignator = reader["pib"] != DBNull.Value
                    ? new PravnoLice { IdKonsignator = idKons }
                    : (Konsignator)new FizickoLice { IdKonsignator = idKons };

                lista.Add(listing);
            }

            return lista;
        }
    }
}
