using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Loan
{
    public class InitiateBookLoanrequestValidator : AbstractValidator<InitiateBookLoanRequestModel>
    {
        public InitiateBookLoanrequestValidator()
        {
            RuleFor(x => x.MembershipNumber)
                .NotEmpty().WithMessage("Membership number is required");


            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("Return date is required");
                
        }
    }
}
