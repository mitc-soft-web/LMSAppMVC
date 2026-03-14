using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;
using LMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMSAppMVC.Implementation.Repositories
{
    public class UserRepository(LMSContext context) : BaseRepository(context),  IUserRespository
    {
        private readonly LMSContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<User> GetUserByEmail(string email)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Set<User>()
                .Include(u => u.Member)
                .Include(u => u.Librarian)
                .SingleOrDefaultAsync(u => u.Email == email);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<User> GetUserRole(Guid userId)
        {
            var userRole = await _context.Set<User>()
                .Include(u => u.Role)
                .Where(u => u.Id == userId)
                .SingleOrDefaultAsync();

#pragma warning disable CS8603 // Possible null reference return.
            return userRole;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
