using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Auth.Validation
{
    public class GenerateLibrarianRegistratationCodeRequestValidator : AbstractValidator<GenerateLibrarianRegistratationCodeRequestModel>
    {
        public GenerateLibrarianRegistratationCodeRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .Equal(x => x.Email.Trim()).WithMessage("Email cannot contain leading or trailing whitespace.");
        }
    }
}
