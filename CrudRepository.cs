using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Shinetech.Infrastructure.Contract
{
      public class CrudRepository<T> : Repository<T>, ICrudRepository<T> where T : IEntity
    {
        static Func<T, object> _orderByFunc;
        static bool _isOrderByAsc = true;
        public CrudRepository(DbContext context, IUnitOfWork unitOfWork)
              : base(context, unitOfWork)
        {
        }

        public void SetOrderByField(Func<T, object> func)
        {
            _orderByFunc = func;
        }
        public void SetOrderBySequence(bool ascent)
        {
            _isOrderByAsc = ascent;
        }

        public PaginatedList<T> GetPageList(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string includeProperties = "")
        {
            var source = Get(predicate, null, includeProperties);
            int count = source.Count();
            List<T> manifests = null;
            if (count > 0)
            {
                if (_isOrderByAsc)
                {
                    manifests = _orderByFunc != null ? source.AsEnumerable().OrderBy(_orderByFunc).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList() : source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    manifests = _orderByFunc != null ? source.AsEnumerable().OrderByDescending(_orderByFunc).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList() : source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }

            }
            return new PaginatedList<T>(pageIndex, pageSize, count, manifests ?? new List<T>());
        }
    }
}
