using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Interfaces.Repositories
{
    public interface IUserRespository : IBaseRepository
    {
        Task<User> GetUserRole(Guid userId);
        Task<User> GetUserByEmail(string email);
    }
}
