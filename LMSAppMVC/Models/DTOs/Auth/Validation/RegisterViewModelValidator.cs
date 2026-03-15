using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Auth.Validation
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            When(x => x.Member != null, () =>
            {
                RuleFor(x => x.Member)
                    .SetValidator(new RegisterMemberRequestValidator());
            });

            When(x => x.Librarian != null, () =>
            {
                RuleFor(x => x.Librarian)
                    .SetValidator(new RegisterLibrarianRequestValidator());
            });
        }
    }
}
