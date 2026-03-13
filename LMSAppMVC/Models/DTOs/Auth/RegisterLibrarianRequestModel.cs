namespace LMSAppMVC.Models.DTOs.Auth
{
    public class RegisterLibrarianRequestModel
    {
        public required string Email { get; set; }
        public required string HashPassword { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string FullName { get; set; }
        public required string LibrarianRegistrationCode { get; set; }
    }
}
