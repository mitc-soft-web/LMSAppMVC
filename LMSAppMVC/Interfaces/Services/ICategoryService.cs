using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<BaseResponse<bool>> AddCategoryAsync(string name, string description);
        public Task<BaseResponse<IReadOnlyList<Category>>> GetAllCategoriesAsync();
    }
}
