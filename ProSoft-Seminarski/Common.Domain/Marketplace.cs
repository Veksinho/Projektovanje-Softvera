using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Marketplace : IDomainObject
    {
        public int MarketplaceID { get; set; }
        public string Naziv { get; set; }

        public string TableName => "Marketplace";

        public string InsertValues => "@Naziv";

        public IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            Marketplace m = new Marketplace()
            {
                MarketplaceID  = (int)reader["MarketplaceID"],
                Naziv = (string)reader["Naziv"]
            };
            return m;
        }
    }
}
