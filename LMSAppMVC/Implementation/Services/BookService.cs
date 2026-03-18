using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Book;
using LMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LMSAppMVC.Implementation.Services
{
    public class BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, 
        ICategoryRepository categoryRepository, IAuthorRepository authorRepository) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        private readonly IUnitOfWork _unitOfOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IAuthorRepository _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        public async Task<BaseResponse<bool>> AddBookAsync(AddBookRequestModel request)
        {

            var bookExists = await _bookRepository.Any<Book>(b => b.Title == request.Title && b.AuthorId == request.AuthorId);
            if(bookExists)
            {
                return new BaseResponse<bool>
                {
                    Message = "Book title with same author already exists",
                    Status = false
                };
            }

            var author = await _authorRepository.Get<Author>(a => a.Id == request.AuthorId);

            // Check author
            if(author is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Author doesn't exist",
                    Status = false
                };
            }

            // Check category
            var category = await _categoryRepository.Get<Category>(c => c.Id == request.CategoryId);
            if (category is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Category doesn't exist",
                    Status = false
                };
            }

            var book = new Book
            {
                Title = request.Title,
                ISBN = request.ISBN,
                TotalCopies = request.TotalCopies,
                AvailableCopies = request.TotalCopies,
                AuthorId = author.Id,
                CategoryId = category.Id,
                PublishedYear = request.PublishedYear,
                DateCreated = DateTime.UtcNow
            };
            await _bookRepository.Add<Book>(book);
            var result = await _unitOfOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse<bool>
            {
                Message = "Book added successfully",
                Status = true
            } :
            new BaseResponse<bool>
            {
                Message = "Book couldn't be added",
                Status = false
            };
            

        }

        public async Task<BaseResponse<IReadOnlyList<ListOfBookResponseDto>>> GetAllAvailableBooksAsync()
        {
            var availableBooks = await _bookRepository.GetAllAvailableBooksWithDetails();

            if (availableBooks is null || !availableBooks.Any())
            {
                return new BaseResponse<IReadOnlyList<ListOfBookResponseDto>>
                {
                    Message = "No books available presently",
                    Status = false
                };
            }

            return new BaseResponse<IReadOnlyList<ListOfBookResponseDto>>
            {
                Message = $"{availableBooks.Count()} books retrieved successfully",
                Status = true,
                Data = availableBooks.Select(book => new ListOfBookResponseDto
                { 
                    Id = book.Id,
                    Title = book.Title,
                    PublishedYear = book.PublishedYear,
                    Author = book.Author != null ? book.Author.FullName : "",
                    Category = book.Category != null ? book.Category.Name : ""

                }).ToList()
            };
        }

        public async Task<BaseResponse<IReadOnlyList<ListOfBookResponseDto>>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksWithDetails();
            if(books is null || !books.Any())
            {
                return new BaseResponse<IReadOnlyList<ListOfBookResponseDto>>
                {
                    Message = "No books found",
                    Status = false
                };
            }

            return new BaseResponse<IReadOnlyList<ListOfBookResponseDto>>
            {
                Message = $"{books.Count()} books retrieved successfully",
                Status = true,
                Data = books.Select(book => new ListOfBookResponseDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublishedYear = book.PublishedYear,
                    Author = book.Author != null ? book.Author.FullName : "",
                    Category = book.Category != null ? book.Category.Name : ""

                }).ToList()
            };
        }

        public async Task<BaseResponse<BookDto>> GetBookByIdForLibrarianAsync(Guid id)
        {
            var book = await _bookRepository.GetBookDetails(id);
            if (book is null)
            {
                return new BaseResponse<BookDto>
                {
                    Message = "Book cannot be found",
                    Status = false
                };
            }

            return new BaseResponse<BookDto>
            {
                Message = $"Book with title '{book.Title}' retrieved successfully",
                Status = true,
                Data = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author != null ? book.Author.FullName : "",
                    Category = book.Category != null ? book.Category.Name : "",
                    ISBN = book.ISBN,
                    PublishedYear = book.PublishedYear,
                    AvailableCopies = book.AvailableCopies,
                    TotalCopies = book.TotalCopies,
                    DateAdded = book.DateCreated
                }
            };
        }

        public async Task<BaseResponse<BookDetailsResponseForMember>> GetBookByIdForMemberAsync(Guid id)
        {
            var book = await _bookRepository.GetBookDetails(id);
            if(book is null)
            {
                return new BaseResponse<BookDetailsResponseForMember>
                {
                    Message = "Book cannot be found",
                    Status = false
                };
            }

            return new BaseResponse<BookDetailsResponseForMember>
            {
                Message = $"Book with title '{book.Title}' retrieved successfully",
                Status = true,
                Data = new BookDetailsResponseForMember
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author != null ? book.Author.FullName : "",
                    Category = book.Category != null ? book.Category.Name : "",
                    ISBN = book.ISBN,
                    PublishedYear = book.PublishedYear,
                }
            };
        }
    }
}
