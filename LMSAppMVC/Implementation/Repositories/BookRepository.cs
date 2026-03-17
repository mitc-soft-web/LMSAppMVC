using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;
using LMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMSAppMVC.Implementation.Repositories
{
    public class BookRepository(LMSContext context) : BaseRepository(context), IBookRepository
    {
        private readonly LMSContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Book>> GetAllAvailableBooksWithDetails()
        {
            var books = await _context.Set<Book>()
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.AvailableCopies > 1)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            return books;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetails()
        {
            var books = await _context.Set<Book>()
                .Include(b => b.Author)
                .Include(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            return books;
        }

        public async Task<Book> GetBookDetails(Guid bookId)
        {
            var book = await _context.Set<Book>()
                .Include(b => b.Author)
                .Include(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .SingleOrDefaultAsync(b => b.Id == bookId);

            return book;
        }
    }
}
