using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Auth;
using LMSAppMVC.Models.DTOs.Author;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Interfaces.Services
{
    public interface IAuthorService
    {
        public Task<BaseResponse<bool>> CreateAuthorAsync(CreateAuthorRequestModel request);
        public Task<BaseResponse<IReadOnlyList<Author>>> GetAllAuthorsAsync();
    }
}
