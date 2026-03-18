using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;
using LMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace LMSAppMVC.Implementation.Repositories
{
    public class LoanRepository(LMSContext context) : BaseRepository(context), ILoanRepository
    {
        private readonly LMSContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Loan> GetPendingLoanDetails(Guid id)
        {
            var pendingLoan = await _context.Set<Loan>()
               .Include(l => l.Book)
               .ThenInclude(b => b.Author)
               .Include(l => l.Book)
               .ThenInclude(b => b.Category)
               .Include(l => l.Member)
               .Where(l => l.LoanStatus == Contracts.Enums.LoanStatus.Pending)
               .AsNoTracking()
               .OrderBy(l => l.BorrowDate)
               .AsSplitQuery()
               .SingleOrDefaultAsync(l => l.Id == id);

            return pendingLoan;

        }

        public async Task<IReadOnlyList<Loan>> GetPendingLoans()
        {
            var pendingLoans = await _context.Set<Loan>()
                .Include(l => l.Book)
                .ThenInclude(b => b.Author)
                .Include(l => l.Book)
                .ThenInclude(b => b.Category)
                .Include(l => l.Member)
                .Where(l => l.LoanStatus == Contracts.Enums.LoanStatus.Pending)
                .AsNoTracking()
                .OrderBy(l => l.BorrowDate)
                .AsSplitQuery()
                .ToListAsync();

            Console.WriteLine($"Loan {pendingLoans.Count()}");

            return pendingLoans;
                
        }
    }
}
