using LMSAppMVC.Models.Entities;
using System.Collections.Generic;

namespace LMSAppMVC.Interfaces.Repositories
{
    public interface IBookRepository : IBaseRepository
    {
        Task<IEnumerable<Book>> GetAllBooksWithDetails();
        Task<IEnumerable<Book>> GetAllAvailableBooksWithDetails();
        Task<Book> GetBookDetails(Guid bookId);
    }
}
