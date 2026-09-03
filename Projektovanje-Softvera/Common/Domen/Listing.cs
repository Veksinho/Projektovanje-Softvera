using Common.Domen.Enumeracije;
using Common.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        public Broker? Broker { get; set; }
        public Konsignator? Konsignator { get; set; }

        public List<Karta> Karte { get; set; } = new List<Karta>();

        public DateTime DatumObjaveOd { get; set; }
        public DateTime DatumObjaveDo { get; set; }
        public decimal CenaOd { get; set; }
        public decimal CenaDo { get; set; }
        public Karta? KriterijumKarta { get; set; }
        public Dogadjaj? KriterijumDogadjaj { get; set; }

        [JsonIgnore]
        public string StatusPrikaz => EnumHelper.Naziv(Status);
        [JsonIgnore]
        public string SplitPrikaz => EnumHelper.Naziv(Split);
        [JsonIgnore]
        public int BrojKarata => Karte.Count;
        [JsonIgnore]
        public string DogadjajPrikaz => Karte.Count == 0 || Karte[0].Dogadjaj == null
            ? "—"
            : Karte[0].Dogadjaj!.Naziv;
        [JsonIgnore]
        public decimal UkupnaCena => CenaPoKarti * Karte.Count;
        [JsonIgnore]
        public decimal Provizija => UkupnaCena * ProcenatProvizije / 100m;

        public override string ToString() => $"Listing #{IdListing} - {DogadjajPrikaz}";

        private int IdBrokerSql => Broker == null ? 0 : Broker.IdBroker;
        private int IdKonsignatorSql => Konsignator == null ? 0 : Konsignator.IdKonsignator;
        private string NapomenaSql => string.IsNullOrWhiteSpace(Napomena)
            ? "NULL"
            : $"'{Sql.Tekst(Napomena)}'";

        [JsonIgnore]
        public string TableName => "Listing l";

        [JsonIgnore]
        public string Join =>
            "JOIN Broker b ON b.idBroker = l.idBroker " +
            "JOIN Konsignator k ON k.idKonsignator = l.idKonsignator " +
            "LEFT JOIN FizickoLice fl ON fl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN PravnoLice pl ON pl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN Karta ka ON ka.idListing = l.idListing " +
            "LEFT JOIN Dogadjaj d ON d.idDogadjaj = ka.idDogadjaj";

        [JsonIgnore]
        public string SelectColumns =>
            "l.*, " +
            "b.korisnickoIme AS b_korisnickoIme, b.ime AS b_ime, b.prezime AS b_prezime, " +
            "b.telefon AS b_telefon, " +
            "k.email AS k_email, k.telefon AS k_telefon, k.adresa AS k_adresa, " +
            "k.datumRegistracije AS k_datumRegistracije, " +
            "fl.jmbg AS fl_jmbg, fl.ime AS fl_ime, fl.prezime AS fl_prezime, " +
            "fl.brojLicneKarte AS fl_brojLicneKarte, " +
            "pl.pib AS pl_pib, pl.maticniBroj AS pl_maticniBroj, pl.naziv AS pl_naziv, " +
            "ka.idKarta AS ka_idKarta, ka.sektor AS ka_sektor, ka.red AS ka_red, " +
            "ka.sediste AS ka_sediste, ka.nominalnaCena AS ka_nominalnaCena, " +
            "ka.tip AS ka_tip, ka.format AS ka_format, ka.status AS ka_status, " +
            "ka.idDogadjaj AS ka_idDogadjaj, " +
            "d.naziv AS d_naziv, d.datumOdrzavanja AS d_datumOdrzavanja, d.mesto AS d_mesto";

        [JsonIgnore]
        public string InsertColumns =>
            "datumObjave, datumIsteka, status, cenaPoKarti, split, " +
            "minKolicina, procenatProvizije, napomena, idBroker, idKonsignator";

        [JsonIgnore]
        public string InsertValues =>
            $"'{DatumObjave:yyyy-MM-dd HH:mm:ss}', '{DatumIsteka:yyyy-MM-dd HH:mm:ss}', " +
            $"'{Status}', {Sql.Broj(CenaPoKarti)}, '{Split}', " +
            $"{MinKolicina}, {Sql.Broj(ProcenatProvizije)}, {NapomenaSql}, " +
            $"{IdBrokerSql}, {IdKonsignatorSql}";

        [JsonIgnore]
        public string UpdateValues =>
            $"datumObjave = '{DatumObjave:yyyy-MM-dd HH:mm:ss}', " +
            $"datumIsteka = '{DatumIsteka:yyyy-MM-dd HH:mm:ss}', " +
            $"status = '{Status}', " +
            $"cenaPoKarti = {Sql.Broj(CenaPoKarti)}, " +
            $"split = '{Split}', " +
            $"minKolicina = {MinKolicina}, " +
            $"procenatProvizije = {Sql.Broj(ProcenatProvizije)}, " +
            $"napomena = {NapomenaSql}, " +
            $"idBroker = {IdBrokerSql}, " +
            $"idKonsignator = {IdKonsignatorSql}";

        [JsonIgnore]
        public string PrimaryKeyCondition => $"l.idListing = {IdListing}";

        [JsonIgnore]
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
                if (Broker != null && Broker.IdBroker > 0)
                    uslovi.Add($"l.idBroker = {Broker.IdBroker}");
                if (Konsignator != null && Konsignator.IdKonsignator > 0)
                    uslovi.Add($"l.idKonsignator = {Konsignator.IdKonsignator}");
                if (Konsignator != null && !string.IsNullOrWhiteSpace(Konsignator.NazivKriterijum))
                {
                    string naziv = Sql.Tekst(Konsignator.NazivKriterijum);
                    uslovi.Add($"(fl.ime LIKE '%{naziv}%' OR fl.prezime LIKE '%{naziv}%' " +
                               $"OR pl.naziv LIKE '%{naziv}%')");
                }
                if (DatumObjaveOd != default)
                    uslovi.Add($"CAST(l.datumObjave AS DATE) >= '{DatumObjaveOd:yyyy-MM-dd}'");
                if (DatumObjaveDo != default)
                    uslovi.Add($"CAST(l.datumObjave AS DATE) <= '{DatumObjaveDo:yyyy-MM-dd}'");
                if (CenaOd > 0)
                    uslovi.Add($"l.cenaPoKarti >= {Sql.Broj(CenaOd)}");
                if (CenaDo > 0)
                    uslovi.Add($"l.cenaPoKarti <= {Sql.Broj(CenaDo)}");

                string podupit = UslovNadKartama;
                if (!string.IsNullOrWhiteSpace(podupit))
                    uslovi.Add(podupit);

                return string.Join(" AND ", uslovi);
            }
        }

        private string UslovNadKartama
        {
            get
            {
                var uslovi = new List<string>();

                if (KriterijumKarta != null && !string.IsNullOrWhiteSpace(KriterijumKarta.Sektor))
                    uslovi.Add($"ka2.sektor LIKE '%{Sql.Tekst(KriterijumKarta.Sektor)}%'");
                if (KriterijumKarta != null && KriterijumKarta.Tip.HasValue)
                    uslovi.Add($"ka2.tip = '{KriterijumKarta.Tip}'");
                if (KriterijumDogadjaj != null && KriterijumDogadjaj.IdDogadjaj > 0)
                    uslovi.Add($"ka2.idDogadjaj = {KriterijumDogadjaj.IdDogadjaj}");
                if (KriterijumDogadjaj != null && !string.IsNullOrWhiteSpace(KriterijumDogadjaj.Mesto))
                    uslovi.Add($"d2.mesto LIKE '%{Sql.Tekst(KriterijumDogadjaj.Mesto)}%'");

                if (uslovi.Count == 0)
                    return string.Empty;

                return "l.idListing IN (SELECT ka2.idListing FROM Karta ka2 " +
                       "JOIN Dogadjaj d2 ON d2.idDogadjaj = ka2.idDogadjaj " +
                       $"WHERE ka2.idListing IS NOT NULL AND {string.Join(" AND ", uslovi)})";
            }
        }

        public void SetId(object id)
        {
            IdListing = Convert.ToInt32(id);
        }

        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var mapa = new Dictionary<int, Listing>();

            while (reader.Read())
            {
                int id = (int)reader["idListing"];

                if (!mapa.TryGetValue(id, out Listing listing))
                {
                    listing = new Listing
                    {
                        IdListing = id,
                        DatumObjave = (DateTime)reader["datumObjave"],
                        DatumIsteka = (DateTime)reader["datumIsteka"],
                        Status = Enum.Parse<StatusListinga>((string)reader["status"]),
                        CenaPoKarti = Convert.ToDecimal(reader["cenaPoKarti"]),
                        Split = Enum.Parse<TipSplita>((string)reader["split"]),
                        MinKolicina = (int)reader["minKolicina"],
                        ProcenatProvizije = Convert.ToDecimal(reader["procenatProvizije"]),
                        Napomena = reader["napomena"] == DBNull.Value
                            ? null
                            : (string)reader["napomena"],
                        Broker = new Broker
                        {
                            IdBroker = (int)reader["idBroker"],
                            KorisnickoIme = (string)reader["b_korisnickoIme"],
                            Ime = (string)reader["b_ime"],
                            Prezime = (string)reader["b_prezime"],
                            Telefon = (string)reader["b_telefon"]
                        },
                        Konsignator = NapraviKonsignator(reader)
                    };

                    mapa.Add(id, listing);
                }

                if (reader["ka_idKarta"] != DBNull.Value)
                {
                    listing.Karte.Add(new Karta
                    {
                        IdKarta = (int)reader["ka_idKarta"],
                        Sektor = (string)reader["ka_sektor"],
                        Red = (string)reader["ka_red"],
                        Sediste = (string)reader["ka_sediste"],
                        NominalnaCena = Convert.ToDecimal(reader["ka_nominalnaCena"]),
                        Tip = Enum.Parse<TipKarte>((string)reader["ka_tip"]),
                        Format = Enum.Parse<FormatKarte>((string)reader["ka_format"]),
                        Status = Enum.Parse<StatusKarte>((string)reader["ka_status"]),
                        Listing = listing,
                        Konsignator = listing.Konsignator,
                        Dogadjaj = new Dogadjaj
                        {
                            IdDogadjaj = (int)reader["ka_idDogadjaj"],
                            Naziv = (string)reader["d_naziv"],
                            DatumOdrzavanja = (DateTime)reader["d_datumOdrzavanja"],
                            Mesto = (string)reader["d_mesto"]
                        }
                    });
                }
            }

            return mapa.Values.OrderBy(x => x.IdListing).Cast<IEntity>().ToList();
        }

        private static Konsignator NapraviKonsignator(SqlDataReader reader)
        {
            Konsignator konsignator;

            if (reader["pl_pib"] != DBNull.Value)
                konsignator = new PravnoLice
                {
                    Pib = (string)reader["pl_pib"],
                    MaticniBroj = (string)reader["pl_maticniBroj"],
                    NazivFirme = (string)reader["pl_naziv"]
                };
            else if (reader["fl_jmbg"] != DBNull.Value)
                konsignator = new FizickoLice
                {
                    Jmbg = (string)reader["fl_jmbg"],
                    Ime = (string)reader["fl_ime"],
                    Prezime = (string)reader["fl_prezime"],
                    BrojLicneKarte = (string)reader["fl_brojLicneKarte"]
                };
            else
                throw new Exception($"Konsignator {reader["idKonsignator"]} nema podatke o tipu.");

            konsignator.IdKonsignator = (int)reader["idKonsignator"];
            konsignator.Email = (string)reader["k_email"];
            konsignator.Telefon = (string)reader["k_telefon"];
            konsignator.Adresa = (string)reader["k_adresa"];
            konsignator.DatumRegistracije = (DateTime)reader["k_datumRegistracije"];

            return konsignator;
        }
    }
}
