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
        public Konsignator? Konsignator { get; set; }
        public Dogadjaj? Dogadjaj { get; set; }

        public bool SamoSlobodne { get; set; }

        [JsonIgnore] public string TipPrikaz => EnumHelper.Naziv(Tip);
        [JsonIgnore] public string FormatPrikaz => EnumHelper.Naziv(Format);
        [JsonIgnore] public string StatusPrikaz => EnumHelper.Naziv(Status);
        [JsonIgnore] public string ListingPrikaz => Listing == null ? "—" : Listing.IdListing.ToString();

        public override string ToString() =>
            $"{Sektor} / {Red} / {Sediste}";

        private string IdListingSql => Listing == null ? "NULL" : Listing.IdListing.ToString();
        private int IdKonsignatorSql => Konsignator == null ? 0 : Konsignator.IdKonsignator;
        private int IdDogadjajSql => Dogadjaj == null ? 0 : Dogadjaj.IdDogadjaj;

        [JsonIgnore]
        public string TableName => "Karta ka";

        [JsonIgnore]
        public string Join =>
            "JOIN Dogadjaj d ON d.idDogadjaj = ka.idDogadjaj " +
            "JOIN Konsignator k ON k.idKonsignator = ka.idKonsignator " +
            "LEFT JOIN FizickoLice fl ON fl.idKonsignator = k.idKonsignator " +
            "LEFT JOIN PravnoLice pl ON pl.idKonsignator = k.idKonsignator";

        [JsonIgnore]
        public string SelectColumns =>
            "ka.*, " +
            "d.naziv AS d_naziv, d.datumOdrzavanja AS d_datumOdrzavanja, d.mesto AS d_mesto, " +
            "k.email AS k_email, k.telefon AS k_telefon, k.adresa AS k_adresa, " +
            "k.datumRegistracije AS k_datumRegistracije, " +
            "fl.jmbg AS fl_jmbg, fl.ime AS fl_ime, fl.prezime AS fl_prezime, " +
            "fl.brojLicneKarte AS fl_brojLicneKarte, " +
            "pl.pib AS pl_pib, pl.maticniBroj AS pl_maticniBroj, pl.naziv AS pl_naziv";

        [JsonIgnore]
        public string InsertColumns =>
            "sektor, red, sediste, nominalnaCena, tip, format, status, " +
            "idListing, idKonsignator, idDogadjaj";

        [JsonIgnore]
        public string InsertValues =>
            $"'{Sql.Tekst(Sektor)}', '{Sql.Tekst(Red)}', '{Sql.Tekst(Sediste)}', " +
            $"{Sql.Broj(NominalnaCena)}, '{Tip}', '{Format}', '{Status}', " +
            $"NULL, {IdKonsignatorSql}, {IdDogadjajSql}";

        [JsonIgnore]
        public string UpdateValues =>
            $"sektor = '{Sql.Tekst(Sektor)}', red = '{Sql.Tekst(Red)}', sediste = '{Sql.Tekst(Sediste)}', " +
            $"nominalnaCena = {Sql.Broj(NominalnaCena)}, tip = '{Tip}', format = '{Format}', " +
            $"status = '{Status}', idListing = {IdListingSql}, " +
            $"idKonsignator = {IdKonsignatorSql}, idDogadjaj = {IdDogadjajSql}";

        [JsonIgnore]
        public string PrimaryKeyCondition => $"ka.idKarta = {IdKarta}";

        [JsonIgnore]
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
                if (Konsignator != null && !string.IsNullOrWhiteSpace(Konsignator.NazivKriterijum))
                {
                    string naziv = Sql.Tekst(Konsignator.NazivKriterijum);
                    uslovi.Add($"(fl.ime LIKE '%{naziv}%' OR fl.prezime LIKE '%{naziv}%' OR pl.naziv LIKE '%{naziv}%')");
                }
                if (Dogadjaj != null && Dogadjaj.IdDogadjaj > 0)
                    uslovi.Add($"ka.idDogadjaj = {Dogadjaj.IdDogadjaj}");
                if (Dogadjaj != null && !string.IsNullOrWhiteSpace(Dogadjaj.Mesto))
                    uslovi.Add($"d.mesto LIKE '%{Sql.Tekst(Dogadjaj.Mesto)}%'");

                if (SamoSlobodne)
                    uslovi.Add("ka.idListing IS NULL");

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
                Konsignator konsignator;

                if (reader["fl_jmbg"] != DBNull.Value)
                {
                    konsignator = new FizickoLice
                    {
                        Jmbg = (string)reader["fl_jmbg"],
                        Ime = (string)reader["fl_ime"],
                        Prezime = (string)reader["fl_prezime"],
                        BrojLicneKarte = (string)reader["fl_brojLicneKarte"]
                    };
                }
                else if (reader["pl_pib"] != DBNull.Value)
                {
                    konsignator = new PravnoLice
                    {
                        Pib = (string)reader["pl_pib"],
                        MaticniBroj = (string)reader["pl_maticniBroj"],
                        NazivFirme = (string)reader["pl_naziv"]
                    };
                }
                else
                {
                    throw new Exception($"Konsignator {reader["idKonsignator"]} nema podatke o tipu.");
                }

                konsignator.IdKonsignator = (int)reader["idKonsignator"];
                konsignator.Email = (string)reader["k_email"];
                konsignator.Telefon = (string)reader["k_telefon"];
                konsignator.Adresa = (string)reader["k_adresa"];
                konsignator.DatumRegistracije = (DateTime)reader["k_datumRegistracije"];

                Dogadjaj dogadjaj = new Dogadjaj
                {
                    IdDogadjaj = (int)reader["idDogadjaj"],
                    Naziv = (string)reader["d_naziv"],
                    DatumOdrzavanja = (DateTime)reader["d_datumOdrzavanja"],
                    Mesto = (string)reader["d_mesto"]
                };

                Listing? listing = reader["idListing"] == DBNull.Value
                    ? null
                    : new Listing { IdListing = (int)reader["idListing"] };

                lista.Add(new Karta
                {
                    IdKarta = (int)reader["idKarta"],
                    Sektor = (string)reader["sektor"],
                    Red = (string)reader["red"],
                    Sediste = (string)reader["sediste"],
                    NominalnaCena = Convert.ToDecimal(reader["nominalnaCena"]),
                    Tip = Enum.Parse<TipKarte>((string)reader["tip"]),
                    Format = Enum.Parse<FormatKarte>((string)reader["format"]),
                    Status = Enum.Parse<StatusKarte>((string)reader["status"]),
                    Listing = listing,
                    Konsignator = konsignator,
                    Dogadjaj = dogadjaj
                });
            }

            return lista;
        }
    }
}
