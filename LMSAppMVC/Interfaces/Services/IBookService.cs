using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Book;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Interfaces.Services
{
    public interface IBookService
    {
        public Task<BaseResponse<bool>> AddBookAsync(AddBookRequestModel request);
        public Task<BaseResponse<IReadOnlyList<ListOfBookResponseDto>>> GetAllBooksAsync();
        public Task<BaseResponse<IReadOnlyList<ListOfBookResponseDto>>> GetAllAvailableBooksAsync();
        public Task<BaseResponse<BookDetailsResponseForMember>> GetBookByIdForMemberAsync(Guid id);
        public Task<BaseResponse<BookDto>> GetBookByIdForLibrarianAsync(Guid id);
    }
}

