using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.LMSDbContext;
using LMSAppMVC.Models;
using LMSAppMVC.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LMSAppMVC.Implementation.Services
{
    public class BookService(LMSContext context) : IBookService
    {
       private readonly LMSContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<bool> AddBook(AddBookRequestModel request)
        {
            var book = new Book
            {
                Author = request.Author,
                ISBN = request.ISBN,
                ISBNYear = request.ISBNYear,
                Title = request.Title,
            };

            await _context.Set<Book>().AddAsync(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<Book>> GetAllBooks()
        {
            var books = await _context.Set<Book>()
                .OrderBy(b => b.Title)
                .ToListAsync();

            return books;
        }
    }
}
