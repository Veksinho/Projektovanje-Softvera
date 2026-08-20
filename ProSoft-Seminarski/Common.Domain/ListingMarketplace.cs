using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class ListingMarketplace : IDomainObject
    {
        public Listing Listing { get; set; }
        public Marketplace Marketplace { get; set; }
        public DateTime DatumObjavljivanja { get; set; }

        public string TableName => "ListingMarketplace";

        public string InsertValues => "@ListingID, @MarketplaceID, @DatumObjavljivanja";

        public IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            ListingMarketplace lm = new ListingMarketplace()
            {
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
                Marketplace = new Marketplace
                {
                    MarketplaceID = (int)reader["MarketplaceID"],
                    Naziv = (string)reader["Naziv"]
                },
                DatumObjavljivanja = (DateTime)reader["DatumObjavljivanja"]
            };
            return lm;
        }
    }
}
