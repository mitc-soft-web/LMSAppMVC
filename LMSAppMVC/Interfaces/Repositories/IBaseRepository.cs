using LMSAppMVC.Contracts.Entities;
using System.Linq.Expressions;

namespace LMSAppMVC.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        Task<T> Add<T>(T entity) where T : BaseEntity;
        Task<T> Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        Task<IReadOnlyList<T>> GetAll<T>() where T : BaseEntity;
        IQueryable<T> QueryWhere<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
    }
}
