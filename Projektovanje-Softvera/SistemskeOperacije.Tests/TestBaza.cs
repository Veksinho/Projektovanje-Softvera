using DatabaseBroker;
using Microsoft.Data.SqlClient;

namespace SistemskeOperacije.Tests
{
    internal static class TestBaza
    {
        public static void Execute(string sql)
        {
            using SqlConnection connection = new SqlConnection(DBConfig.ConnectionString);
            connection.Open();
            using SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static int BrojRedova(string tabela, string uslov = "1 = 1")
        {
            using SqlConnection connection = new SqlConnection(DBConfig.ConnectionString);
            connection.Open();
            using SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM {tabela} WHERE {uslov}", connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public static bool Postoji(string tabela, string uslov) => BrojRedova(tabela, uslov) > 0;

        public static string? SifraHash(int idBroker)
        {
            using SqlConnection connection = new SqlConnection(DBConfig.ConnectionString);
            connection.Open();
            using SqlCommand command = new SqlCommand($"SELECT sifraHash FROM Broker WHERE idBroker = {idBroker}", connection);
            object? vrednost = command.ExecuteScalar();
            return vrednost == null || vrednost == DBNull.Value ? null : (string)vrednost;
        }
    }
}