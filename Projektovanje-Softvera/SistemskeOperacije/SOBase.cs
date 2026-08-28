using DatabaseBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije
{
    public abstract class SOBase
    {
        protected DBBroker dbBroker;
        public object? Result { get; protected set; }

        public SOBase()
        {
            dbBroker = new DBBroker();
        }

        public void ExecuteTemplate()
        {
            try
            {
                dbBroker.OpenConnection();
                dbBroker.BeginTransaction();

                Validate();
                ExecuteConcreteOperation();

                dbBroker.Commit();
            }
            catch (Exception ex)
            {
                dbBroker.Rollback();
                throw;
            }
            finally
            {
                dbBroker.CloseConnection();
            }
        }

        protected abstract void ExecuteConcreteOperation();

        protected virtual void Validate() { }
    }
}
