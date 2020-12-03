using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Shinetech.Infrastructure.Contract
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly List<DbContext> _contexts;
        public EFUnitOfWork()
        {
            _contexts = new List<DbContext>();
        }
        public void AddContext(params DbContext[] contexts)
        {
            foreach (var c in contexts)
            {
                _contexts.Add(c);
            }
        }

        public DbTransaction BeginTransaction()
        {
            foreach (var c in _contexts)
            {
                if (c.Database.CurrentTransaction == null)
                {
                    c.Database.BeginTransaction();
                }
            }
            return null;
        }

        public void CommitTransaction()
        {
            foreach (var c in _contexts)
            {
                try
                {
                    c.SaveChanges();
                    if (c.Database.CurrentTransaction != null)
                    {
                        c.Database.CommitTransaction();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {

                    foreach (var entityEntry in c.ChangeTracker.Entries())
                    {
                        entityEntry.State = EntityState.Detached;
                    }
                }
            }
        }

        public void RollbackTransaction()
        {
            foreach (var c in _contexts)
            {
                if (c.Database.CurrentTransaction != null)
                {
                    c.Database.RollbackTransaction();
                }
            }
        }
    }
}
