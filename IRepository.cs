using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shinetech.Infrastructure.Contract
{
    public interface IRepository
    {
        DbContext Context { get; set; }
    }

    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository<TEntity> : IRepository
        where TEntity : class
    {
        DbSet<TEntity> DbSet { get; set; }
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        IQueryable<TEntity> GetWithTracking(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetById(int id, string includeProperties = "");
        Task<TEntity> GetByIdAsync(int id, string includeProperties = "");

        TEntity GetByIdWithTracking(int id, string includeProperties = "");

        void Insert(TEntity entity);
        void InsertAsync(TEntity entity);

        void Delete(int id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        int Save();
        Task<int> SaveAsync();
    }
}
