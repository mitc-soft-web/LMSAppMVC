namespace LMSAppMVC.Models.DTOs.Auth
{
    public class RegisterViewModel
    {
        public required RegisterMemberRequestModel Member {get; set;}
        public required RegisterLibrarianRequestModel Librarian {get; set;}
    }
}
