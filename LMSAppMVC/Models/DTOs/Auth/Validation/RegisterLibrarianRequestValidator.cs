using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Auth.Validation
{
    public class RegisterLibrarianRequestValidator: AbstractValidator<RegisterLibrarianRequestModel>
    {
        public RegisterLibrarianRequestValidator()
        {
            RuleFor(x => x.FullName)
               .NotEmpty().WithMessage("Fullname name is required.")
               .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .Must(email => string.IsNullOrEmpty(email) || email == email.Trim())
                .WithMessage("Email cannot contain leading or trailing whitespace.");

            RuleFor(x => x.LibrarianRegistrationCode)
                .NotEmpty().WithMessage("Employee number required");


            RuleFor(x => x.HashPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required.");

            When(x => !string.IsNullOrEmpty(x.HashPassword), () =>
            {
                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.HashPassword)
                    .WithMessage("Confirm password must match the password.");
            });
        }
    }
}
