using DatabaseBroker;
using Microsoft.Data.SqlClient;
using SistemskeOperacije.Bezbednost;
using System.Text;

namespace MigracijaSifara
{
    // Alat za prelazak razvojne baze na heširane šifre (docs/HESIRANJE-SIFARA.md, H3).
    //
    // Alat nije deo sistema koji se meri (klijent, server, sistemske operacije), pa za njega
    // ne važi pravilo iz CLAUDE.md da parametrizacija SQL-a dolazi tek u fazi 6.1:
    // vrednosti se ovde prosleđuju preko SqlParameter.
    //
    // Alat nikad ne ispisuje šifre ni heševe iz baze. Jedini izlaz sa hešom je režim --hesiraj,
    // koji ne dira bazu.
    internal static class Program
    {
        private const string PrefiksHesa = "$2";
        private const string SufiksTestBaze = "_Test";

        private static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                if (args.Length == 0)
                    return Migriraj();

                if (args.Length == 2 && args[0] == "--hesiraj")
                    return Hesiraj(args[1]);

                IspisiUputstvo();
                return 1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Greška: {ex.Message}");
                return 1;
            }
        }

        private static void IspisiUputstvo()
        {
            Console.Error.WriteLine("Upotreba:");
            Console.Error.WriteLine("  MigracijaSifara                      migracija šifara u bazi iz appsettings.json");
            Console.Error.WriteLine("  MigracijaSifara --hesiraj \"<šifra>\"  ispisuje heš šifre, baza se ne koristi");
        }

        private static int Hesiraj(string sifra)
        {
            if (string.IsNullOrEmpty(sifra))
            {
                Console.Error.WriteLine("Šifra za heširanje nije prosleđena.");
                return 1;
            }

            Console.WriteLine(PasswordHasher.Hash(sifra));
            return 0;
        }

        private static int Migriraj()
        {
            var csb = new SqlConnectionStringBuilder(DBConfig.ConnectionString);
            Console.WriteLine($"Server: {csb.DataSource}");
            Console.WriteLine($"Baza:   {csb.InitialCatalog}");

            if (csb.InitialCatalog.EndsWith(SufiksTestBaze, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Baza je test baza. Test baza se puni iz ResetujBazu, migracija se ne radi.");
                return 1;
            }

            using var connection = new SqlConnection(csb.ConnectionString);
            connection.Open();

            if (!PostojiKolonaSifraHash(connection))
            {
                Console.WriteLine("Kolona dbo.Broker.sifraHash ne postoji. Prvo pokrenite db/MigrationPasswordHash.sql.");
                return 1;
            }

            (int ukupno, int hesirano, int prazno) = PrebrojBrokere(connection);
            int zaHesiranje = ukupno - hesirano - prazno;

            Console.WriteLine($"Brokera ukupno:          {ukupno}");
            Console.WriteLine($"Već heširanih:           {hesirano}");
            Console.WriteLine($"Treba heširati:          {zaHesiranje}");
            if (prazno > 0)
                Console.WriteLine($"Prazna vrednost (preskače se, prijava nije moguća): {prazno}");

            if (zaHesiranje == 0)
            {
                Console.WriteLine("Nema šifara za heširanje. Baza je već migrirana.");
                return 0;
            }

            Console.Write("Nastaviti? (d/n) ");
            string? odgovor = Console.ReadLine();
            if (!string.Equals(odgovor?.Trim(), "d", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Prekinuto. Baza nije menjana.");
                return 0;
            }

            int promenjeno = HesirajSifre(connection);
            Console.WriteLine($"Promenjeno redova: {promenjeno}");
            return 0;
        }

        private static bool PostojiKolonaSifraHash(SqlConnection connection)
        {
            using var cmd = new SqlCommand("SELECT COL_LENGTH(N'dbo.Broker', N'sifraHash')", connection);
            return cmd.ExecuteScalar() is not (null or DBNull);
        }

        private static (int ukupno, int hesirano, int prazno) PrebrojBrokere(SqlConnection connection)
        {
            using var cmd = new SqlCommand(
                "SELECT COUNT(*), " +
                "ISNULL(SUM(CASE WHEN sifraHash LIKE @prefiks + N'%' THEN 1 ELSE 0 END), 0), " +
                "ISNULL(SUM(CASE WHEN DATALENGTH(sifraHash) = 0 THEN 1 ELSE 0 END), 0) " +
                "FROM dbo.Broker", connection);
            cmd.Parameters.Add("@prefiks", System.Data.SqlDbType.NVarChar, 10).Value = PrefiksHesa;

            using var reader = cmd.ExecuteReader();
            reader.Read();
            return (reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
        }

        private static int HesirajSifre(SqlConnection connection)
        {
            using var transaction = connection.BeginTransaction();

            // UPDLOCK/HOLDLOCK: pročitani redovi ostaju zaključani do kraja transakcije.
            var redovi = new List<(int idBroker, string sifra)>();
            using (var select = new SqlCommand(
                "SELECT idBroker, sifraHash FROM dbo.Broker WITH (UPDLOCK, HOLDLOCK) " +
                "WHERE sifraHash NOT LIKE @prefiks + N'%' AND DATALENGTH(sifraHash) > 0",
                connection, transaction))
            {
                select.Parameters.Add("@prefiks", System.Data.SqlDbType.NVarChar, 10).Value = PrefiksHesa;
                using var reader = select.ExecuteReader();
                while (reader.Read())
                    redovi.Add((reader.GetInt32(0), reader.GetString(1)));
            }

            int promenjeno = 0;
            using (var update = new SqlCommand(
                "UPDATE dbo.Broker SET sifraHash = @sifraHash WHERE idBroker = @idBroker",
                connection, transaction))
            {
                SqlParameter pHash = update.Parameters.Add("@sifraHash", System.Data.SqlDbType.NVarChar, 100);
                SqlParameter pId = update.Parameters.Add("@idBroker", System.Data.SqlDbType.Int);

                foreach ((int idBroker, string sifra) in redovi)
                {
                    pHash.Value = PasswordHasher.Hash(sifra);
                    pId.Value = idBroker;
                    promenjeno += update.ExecuteNonQuery();
                }
            }

            transaction.Commit();
            return promenjeno;
        }
    }
}
