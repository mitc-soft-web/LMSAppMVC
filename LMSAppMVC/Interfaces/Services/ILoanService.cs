using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Loan;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Interfaces.Services
{
    public interface ILoanService
    {
        public Task<BaseResponse<bool>> InitiateBookLoanAsync(Guid bookId, Guid memberId, InitiateBookLoanRequestModel request);
        public Task<BaseResponse<bool>> ApproveBookLoanAsync(Guid id, Guid librarianId);
        public Task<BaseResponse<bool>> ReturnBookAsync(Guid loanId, Guid memberId);
        public Task<BaseResponse<IReadOnlyList<PendingLoansResponse>>> AllPendingLoansAsync();
        public Task<BaseResponse<PendingLoansResponse>> GetPendingLoanDetailsAsync(Guid loanId);
    }
}
