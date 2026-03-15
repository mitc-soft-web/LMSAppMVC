using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Auth.Validation
{
    public class GenerateLibrarianRegistratationCodeRequestValidator : AbstractValidator<GenerateLibrarianRegistratationCodeRequestModel>
    {
        public GenerateLibrarianRegistratationCodeRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Enter valid email address")
                .Must(email => string.IsNullOrEmpty(email) || email == email.Trim())
                .WithMessage("Email cannot contain leading or trailing whitespace.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required");
        }
    }
}
