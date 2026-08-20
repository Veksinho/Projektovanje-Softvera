using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Listing : IDomainObject
    {
        public int ListingID { get; set; }
        public float UkupnaVrednost { get; set; }
        public float ProdajnaCena { get; set; }
        public int BrojKarata { get; set; }
        public string Napomena { get; set; }
        public DateTime DatumPoslednjeIzmene { get; set; }
        public DateTime DatumIsteka { get; set; }
        public Status Status { get; set; }

        public string TableName => "Listing";

        public string InsertValues => "@UkupnaVrednost, @ProdajnaCena, @BrojKarata, @Napomena, @DatumPoslednjeIzmene, @DatumIsteka, @Status";

        public IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            Listing l = new Listing()
            {
                ListingID = (int)reader["ListingID"],
                UkupnaVrednost = (float)reader["UkupnaVrednost"],
                ProdajnaCena = (float)reader["ProdajnaCena"],
                BrojKarata = (int)reader["BrojKarata"],
                Napomena = (string)reader["Napomena"],
                DatumPoslednjeIzmene = (DateTime)reader["DatumPoslednjeIzmene"],
                DatumIsteka = (DateTime)reader["DatumIsteka"],
                Status = (Status)reader["Status"]
            };
            return l;
        }
    }

    public enum Status
    {
        Aktivan = 0,
        Prodat = 1,
        Istekao = 2
    }
}
