using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace i4o
{    
    public interface IIndex<TChild> : ICollection<TChild>
    {
        IEnumerable<TChild> WhereThroughIndex(Expression<Func<TChild, bool>> whereClause);

        IEnumerable<TChild> WhereThroughIndex(object value);
        void Reset(TChild changedObject);
    }
}
