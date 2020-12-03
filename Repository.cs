using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shinetech.Infrastructure.Contract
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public DbContext Context { get; set; }
        public DbSet<TEntity> DbSet { get; set; }
        protected IUnitOfWork _unitOfWork { get; set; }

        public Repository(DbContext context, IUnitOfWork unitOfWork)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
            this._unitOfWork = unitOfWork;
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this.DbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetWithTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this.DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public virtual TEntity GetById(int id, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking().Where(r => r.Id == id);

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            TEntity first = query.FirstOrDefault();
            return first;
        }

        public virtual Task<TEntity> GetByIdAsync(int id, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking().Where(r => r.Id == id);

            foreach (string includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefaultAsync();
        }

        public virtual TEntity GetByIdWithTracking(int id, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking().Where(r => r.Id == id);

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void InsertAsync(TEntity entity)
        {
            DbSet.AddAsync(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = DbSet.AsNoTracking().SingleOrDefault(r => r.Id == id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public int Save()
        {
            try
            {
                int result = this.Context.SaveChanges();
                return result;
            }
            finally
            {
                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in Context.ChangeTracker
                    .Entries())
                {
                    entityEntry.State = EntityState.Detached;
                }
            }
        }

        public Task<int> SaveAsync()
        {
            try
            {
                Task<int> result = Context.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}