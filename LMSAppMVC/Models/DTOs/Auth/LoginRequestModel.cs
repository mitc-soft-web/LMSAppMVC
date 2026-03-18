using LMSAppMVC.Models.DTOs.Auth.Librarian;
using LMSAppMVC.Models.DTOs.Member;

namespace LMSAppMVC.Models.DTOs.Auth
{
    public class LoginRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginResponseModel : BaseResponse
    {
        public required string FullName { get; set; }
        public required Guid UserId { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }

        public Guid MemberId { get; set; }
        public Guid LibrarianId { get; set; }
        public LibrarianDto? Librarian { get; set; }
        public MemberDto? Member { get; set; }
        
    }
}
