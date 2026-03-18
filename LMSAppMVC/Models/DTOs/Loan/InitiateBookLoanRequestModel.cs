using LMSAppMVC.Contracts.Enums;

namespace LMSAppMVC.Models.DTOs.Loan
{
    public class InitiateBookLoanRequestModel
    {
#pragma warning disable CS8618 
        public string MembershipNumber { get; set; }
#pragma warning restore CS8618
        public DateTime ReturnDate { get; set; }
        //public Guid BookId { get; set; }
    }
}
