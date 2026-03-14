using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Auth.Validation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Enter valid email address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
