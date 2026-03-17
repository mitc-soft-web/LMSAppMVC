using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Implementation.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        public async Task<BaseResponse<bool>> AddCategoryAsync(string name, string? description = null)
        {

            if (string.IsNullOrEmpty(name))
            {
                return new BaseResponse<bool> { Message = "Category name is required", Status = false };
            }

            var categoryExists = await _categoryRepository.Any<Category>(c => c.Name == name);
            if (categoryExists)
            {
                return new BaseResponse<bool>
                {
                    Message = "Category already exists",
                    Status = false
                };
            }

            var category = new Category
            {
                Name = name,
                Description = description,
                DateCreated = DateTime.UtcNow
            };

            await _categoryRepository.Add(category);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse<bool>
            {
                Message = $"Category '{name}' added successfully",
                Status = true
            } :
            new BaseResponse<bool>
            {
                Message = "Category couldn't be added",
                Status = false
            };

            
        }

        public async Task<BaseResponse<IReadOnlyList<Category>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll<Category>();
            if(categories is null || !categories.Any())
            {
                return new BaseResponse<IReadOnlyList<Category>>
                {
                    Message = "No categories found",
                    Status = false
                };
            }

            return new BaseResponse<IReadOnlyList<Category>>
            {
                Message = $"{categories.Count} categories retrieved",
                Status = true,
                Data = categories
            };
        }
    }
}
