using LMSAppMVC.Models;
using LMSAppMVC.Models.DTOs;

namespace LMSAppMVC.Interfaces.Services
{
    public interface IBookService
    {
        public Task<bool> AddBook(AddBookRequestModel request);
        public Task<IReadOnlyList<Book>> GetAllBooks();
    }
}
