using FluentValidation;

namespace LMSAppMVC.Models.DTOs.User.Validation
{
    public class RegisterMemberRequestValidator : AbstractValidator<RegisterMemberRequestModel>
    {
        public RegisterMemberRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Fullname name is required.")
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .Equal(x => x.Email.Trim()).WithMessage("Email cannot contain leading or trailing whitespace.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international phone number.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");
                


            RuleFor(x => x.HashPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.HashPassword).WithMessage("Confirm password must match the password.");
        }
    }
}
