using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Interfaces.Services
{
    public interface IBookService
    {
        public Task<bool> AddBook(AddBookRequestModel request);
        public Task<IReadOnlyList<Book>> GetAllBooks();
    }
}
