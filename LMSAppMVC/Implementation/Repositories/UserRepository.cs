using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;

namespace LMSAppMVC.Implementation.Repositories
{
    public class UserRepository(LMSContext context) : BaseRepository(context),  IUserRespository
    {
        private readonly LMSContext _context = context ?? throw new ArgumentNullException(nameof(context));

    }
}
