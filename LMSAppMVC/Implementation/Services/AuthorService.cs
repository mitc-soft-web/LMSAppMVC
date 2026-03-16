using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Author;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Implementation.Services
{
    public class AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork) : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        public async Task<BaseResponse<bool>> CreateAuthorAsync(CreateAuthorRequestModel request)
        {
            var authorExists = await _authorRepository.Any<Author>(a => a.FullName == request.FullName);
            if (authorExists)
            {
                return new BaseResponse<bool>
                {
                    Message = "Author already exists!",
                    Status = false
                };
            }

            var author = new Author
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Country = request.Country,
                DateCreated = DateTime.UtcNow,
            };

            var addAuthor = await _authorRepository.Add(author);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse<bool>
            {
                Message = "Author added successfully",
                Status = true
            }
            : new BaseResponse<bool>
            {
                Message = "Failed to add Author",
                Status = false
            };

        }

        public async Task<BaseResponse<IReadOnlyList<Author>>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAll<Author>();
            if(authors is null || !authors.Any())
            {
                return new BaseResponse<IReadOnlyList<Author>>
                {
                    Message = "No author found",
                    Status = false
                };
            }

            return new BaseResponse<IReadOnlyList<Author>>
            {
                Message = $"{authors.Count} authors retrieved",
                Status = true,
                Data = authors
            };
        }
    }
}
