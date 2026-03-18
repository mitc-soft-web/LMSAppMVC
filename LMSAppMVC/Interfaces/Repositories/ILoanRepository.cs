using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Interfaces.Repositories
{
    public interface ILoanRepository : IBaseRepository
    {
        Task<IReadOnlyList<Loan>> GetPendingLoans();
        Task<Loan> GetPendingLoanDetails(Guid id);
    }
}
