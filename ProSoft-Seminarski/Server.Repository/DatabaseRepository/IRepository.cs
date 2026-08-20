using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository.DatabaseRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(IDomainObject obj);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        List<T> Find(string condition);
        void OpenConnection();
        void CloseConnection();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
