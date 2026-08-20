using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Karta : IDomainObject
    {
        public int KartaID { get; set; }
        public string Sekcija { get; set; }
        public string Red { get; set; }
        public string Sediste { get; set; }
        public double Cena { get; set; }
        public TipKarte Tip { get; set; }
        public DateTime DatumKupovine { get; set; }
        public Listing Listing { get; set; }
        public Dogadjaj Dogadjaj { get; set; }


        public string TableName => "Karta";

        public string InsertValues => "@Sekcija, @Red, @Sediste, @Cena, @Tip, @DatumKupovine, @ListingID, @DogadjajID";

        public IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            Karta k = new Karta()
            {
                KartaID = (int)KartaID,
                Sekcija = (string)Sekcija,
                Red = (string)Red,
                Sediste = (string)Sediste,
                Cena = (float)Cena,
                Tip = (TipKarte)Tip,
                DatumKupovine = (DateTime)DatumKupovine,
                Listing = new Listing
                {
                    ListingID = (int)reader["ListingID"],
                    UkupnaVrednost = (float)reader["UkupnaVrednost"],
                    ProdajnaCena = (float)reader["ProdajnaCena"],
                    BrojKarata = (int)reader["BrojKarata"],
                    Napomena = (string)reader["Napomena"],
                    DatumPoslednjeIzmene = (DateTime)reader["DatumPoslednjeIzmene"],
                    DatumIsteka = (DateTime)reader["DatumIsteka"],
                    Status = (Status)reader["Status"]
                },
                Dogadjaj = new Dogadjaj
                {
                    DogadjajID = (int)reader["DogadjajID"],
                    Naziv = (string)reader["Naziv"],
                    MestoOdrzavanja = (string)reader["MestoOdrzavanja"],
                    Vreme = (DateTime)reader["Vreme"],
                    Grad = (string)reader["Grad"],
                    Drzava = (string)reader["Drzava"],
                }
            };
            return k;
        }
    }

    public enum TipKarte
    {
        Fizicka = 0,
        EKarta = 1,
        MobilniTransfer = 2,
        MobilniQ = 3
    }
}
