using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;

namespace LMSAppMVC.Implementation.Repositories
{
    public class RoleRepository(LMSContext context) : BaseRepository(context), IRoleRepository
    {
    }
}
