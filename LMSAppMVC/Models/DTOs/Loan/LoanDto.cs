using LMSAppMVC.Contracts.Enums;

namespace LMSAppMVC.Models.DTOs.Loan
{
    public class LoanDto
    {
    }

    public class PendingLoansResponse
    {
        public Guid Id { get; set; }
        public required string BookTitle { get; set; }
        public required string Author { get; set; }
        public required string CategoryName { get; set; }
        public required string Borrower { get; set; }
        public required DateTime DateInitiated { get; set; }
        public required DateTime ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
    }
}   
