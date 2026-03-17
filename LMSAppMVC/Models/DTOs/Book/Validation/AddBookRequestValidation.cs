using FluentValidation;

namespace LMSAppMVC.Models.DTOs.Book.Validation
{
    public class AddBookRequestValidation : AbstractValidator<AddBookRequestModel>
    {
        public AddBookRequestValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Book title is required")
                .MinimumLength(15).WithMessage("Title length cannot be less than 15")
                .MaximumLength(100).WithMessage("Title length cannot exceed 100");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("Book title required");

            RuleFor(x => x.PublishedYear)
                .NotEmpty().WithMessage("Published year is required");

            RuleFor(x => x.TotalCopies)
                .NotEmpty().WithMessage("Total copies is required");

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Author is required");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required");
                
                
        }
    }
}
