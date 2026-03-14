using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Auth;

namespace LMSAppMVC.Interfaces.Services
{
    public interface IUserService
    {
        public Task<BaseResponse<bool>> RegisterMemberAsync(RegisterMemberRequestModel request);
        public Task<BaseResponse<bool>> RegisterLibrarianAsync(RegisterLibrarianRequestModel request);
        public Task<BaseResponse> DeleteMember(Guid memberId);
        public Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel request);
    }
}
