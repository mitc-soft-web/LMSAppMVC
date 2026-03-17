using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;

namespace LMSAppMVC.Implementation.Repositories
{
    public class CategoryRepository(LMSContext context) : BaseRepository(context), ICategoryRepository
    {
    }
}
