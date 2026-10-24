using DatabaseBroker;
using Microsoft.Data.SqlClient;

[assembly: DoNotParallelize]

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public static class PodesavanjeTestova
    {
        [AssemblyInitialize]
        public static void ProveriBazu(TestContext _)
        {
            var csb = new SqlConnectionStringBuilder(DBConfig.ConnectionString);
            if (!csb.InitialCatalog.EndsWith("_Test"))
                throw new InvalidOperationException($"Testovi se ne mogu izvršavati nad bazom '{csb.InitialCatalog}'.");
        }
    }
}