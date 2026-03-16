using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;

namespace LMSAppMVC.Implementation.Repositories
{
    public class AuthorRepository(LMSContext context) : BaseRepository(context), IAuthorRepository
    {
    }
}
