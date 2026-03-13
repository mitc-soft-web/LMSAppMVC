using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Auth;

namespace LMSAppMVC.Interfaces.Services
{
    public interface ILibrarianRegistrationCodeService
    {
        public Task<BaseResponse<bool>> GenerateLibrarianRegistrationCodeAsync(GenerateLibrarianRegistratationCodeRequestModel request);
    }
}
