using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.LMSDbContext;

namespace LMSAppMVC.Implementation.Repositories
{
    public class MemberRepository(LMSContext context) : BaseRepository(context), IMemberRepository
    {
    }
}
