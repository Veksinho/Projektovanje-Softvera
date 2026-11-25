using System.Data;
using System.Globalization;
using DatabaseBroker;
using Microsoft.Data.SqlClient;

namespace SistemskeOperacije.Tests
{
    public abstract class IntegracioniTest
    {
        [TestInitialize]
        public void PripremiBazu()
        {
            using var con = new SqlConnection(DBConfig.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand("dbo.ResetujBazu", con) { CommandType = CommandType.StoredProcedure };
            cmd.ExecuteNonQuery();

            CultureInfo.CurrentCulture = new CultureInfo("sr-Latn-RS");
        }
    }
}