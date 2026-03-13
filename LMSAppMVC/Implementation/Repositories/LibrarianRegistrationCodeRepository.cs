using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;

namespace LMSAppMVC.Implementation.Repositories
{
    public class LibrarianRegistrationCodeRepository(LMSContext context) : BaseRepository(context), ILibrarianRegistrationCodeRepository
    {
        private readonly LMSContext _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}
