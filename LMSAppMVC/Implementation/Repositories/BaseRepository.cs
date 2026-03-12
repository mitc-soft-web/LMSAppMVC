using LMSAppMVC.Contracts.Entities;
using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMSAppMVC.Implementation.Repositories
{
    public class BaseRepository(LMSContext context) : IBaseRepository
    {
        private readonly LMSContext _context = context ??  throw new ArgumentNullException(nameof(context));

        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);

        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Set<T>().Where(expression).SingleOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IReadOnlyList<T>> GetAll<T>() where T : BaseEntity
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>()
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<T> QueryWhere<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return _context.Set<T>()
                .Where(expression);
        }

        public async Task<T> Update<T>(T entity) where T : BaseEntity
        {
            _context.Update(entity);
            return entity;
        }
    }
}
