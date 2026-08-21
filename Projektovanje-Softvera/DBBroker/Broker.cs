using System.Data.Common;

namespace DBBroker
{
    public class Broker
    {
        private DBConnection connection;
        public Broker()
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
    }
}
