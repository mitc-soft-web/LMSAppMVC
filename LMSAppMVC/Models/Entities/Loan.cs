using LMSAppMVC.Contracts.Entities;
using LMSAppMVC.Contracts.Enums;

namespace LMSAppMVC.Models.Entities
{
    public class Loan : BaseEntity
    {
        public required Guid BookId { get; set; }
        public Book? Book { get; set; }
        public required string MembershipNumber { get; set; }
        public required Guid MemberId {  get; set; }
        public Member? Member { get; set; }
        public Guid? LibrarianId { get; set; }
        public DateTime BorrowDate { get; set; }
        public required DateTime DueDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public required DateTime ReturnDate { get; set; }
        public required LoanStatus LoanStatus { get; set; }
        public bool IsReturned { get; set; }

    }
}
