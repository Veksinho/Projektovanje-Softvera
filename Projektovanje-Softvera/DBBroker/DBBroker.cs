using Common.Domen;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DatabaseBroker
{
    public class DBBroker
    {
        private readonly DBConnection connection;
        public DBBroker()
        {
            connection = new DBConnection();
        }

        public void Rollback()
        {
            connection.Rollback();
        }

        public void Commit()
        {
            connection.Commit();
        }

        public void BeginTransaction()
        {
            connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            connection.CloseConnection();
        }

        public void OpenConnection()
        {
            connection.OpenConnection();
        }

        public List<IEntity> GetAll(IEntity e)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {e.TableName} {e.Join}";
            using var reader = command.ExecuteReader();

            return e.GetReaderList(reader);
        }

        public List<IEntity> GetByCondition(IEntity e, string uslov)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {e.TableName} {e.Join} WHERE {uslov}";
            using var reader = command.ExecuteReader();

            return e.GetReaderList(reader);
        }

        public IEntity? GetById(IEntity e)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {e.TableName} {e.Join} WHERE {e.PrimaryKeyCondition}";
            using var reader = command.ExecuteReader();
            var lista = e.GetReaderList(reader);

            return lista.Count > 0 ? lista[0] : null;
        }

        public void Add(IEntity e)
        {
            string tableName = e.TableName.Split(' ')[0];

            using var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO {tableName} ({e.InsertColumns}) VALUES ({e.InsertValues}); " +
                "SELECT CAST(SCOPE_IDENTITY() AS int);";

            object id = command.ExecuteScalar();
            e.SetId(id);
        }

        public void Edit(IEntity e)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"UPDATE {e.TableName.Split(" ")[1]} SET {e.UpdateValues} FROM {e.TableName} WHERE {e.PrimaryKeyCondition}";

            command.ExecuteNonQuery();
        }

        public void Delete(IEntity e)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"DELETE {e.TableName.Split(" ")[1]} FROM {e.TableName} WHERE {e.PrimaryKeyCondition}";

            command.ExecuteNonQuery();
        }
    }
}
