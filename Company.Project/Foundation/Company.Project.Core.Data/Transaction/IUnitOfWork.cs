using Company.Project.Core.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Core.Data.Transaction
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        OperationResult Commit();
        void Rollback();
        IDbContextTransaction BeginTransaction();
        Task<int> SaveAsyc();
    }
}
