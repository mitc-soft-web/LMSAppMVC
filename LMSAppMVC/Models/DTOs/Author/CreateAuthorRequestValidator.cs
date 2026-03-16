using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Author
{
    public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorRequestModel>
    {
        public CreateAuthorRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MinimumLength(2).WithMessage("Full name should not be less than 2 characters")
                .MaximumLength(100).WithMessage("Full name should not be more than 100 characters");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .IsInEnum();

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Email must be a valid email address.")
               .Must(email => string.IsNullOrEmpty(email) || email == email.Trim())
               .WithMessage("Email cannot contain leading or trailing whitespace.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international phone number.");

        }
    }
}
