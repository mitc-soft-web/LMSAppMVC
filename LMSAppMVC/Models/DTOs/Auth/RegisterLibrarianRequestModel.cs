namespace LMSAppMVC.Models.DTOs.Auth
{
    public class RegisterLibrarianRequestModel
    {
#pragma warning disable CS8618
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string LibrarianRegistrationCode { get; set; }
#pragma warning restore CS8618
    }
}
