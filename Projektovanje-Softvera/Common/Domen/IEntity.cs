using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public interface IEntity

    {
        string TableName { get; }

        string Join { get; }

        string InsertColumns { get; }

        string InsertValues { get; }

        string UpdateValues { get; }

        string PrimaryKeyCondition { get; }

        string SearchCondition { get; }
        void SetId(object id);

        List<IEntity> GetReaderList(SqlDataReader reader);

    }

}
