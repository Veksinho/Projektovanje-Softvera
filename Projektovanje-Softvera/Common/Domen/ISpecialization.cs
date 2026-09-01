using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domen
{
    public interface ISpecialization
    {
        string SubtypeTableName { get; }
        string SubtypeInsertColumns { get; }
        string SubtypeInsertValues { get; }
        string SubtypeUpdateValues { get; }
        string SubtypePrimaryKeyCondition { get; }
    }
}
