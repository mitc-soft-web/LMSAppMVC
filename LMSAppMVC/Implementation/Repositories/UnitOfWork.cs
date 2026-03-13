using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace LMSAppMVC.Implementation.Repositories
{
    public class UnitOfWork(LMSContext context) : IUnitOfWork
    {
        private readonly LMSContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _context.Database.CreateExecutionStrategy();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
