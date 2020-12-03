using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shinetech.Infrastructure.Contract
{
    public interface ICrudRepository<T> : IRepository<T> where T : class
    {
        void SetOrderByField(Func<T, Object> func);
        void SetOrderBySequence(bool ascent);
        PaginatedList<T> GetPageList(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string includeProperties = "");
    }
}
