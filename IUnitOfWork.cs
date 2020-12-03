using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Shinetech.Infrastructure.Contract
{
    public interface IUnitOfWork
    {
        DbTransaction BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
        void AddContext(params DbContext[] contexts);
    }
}
