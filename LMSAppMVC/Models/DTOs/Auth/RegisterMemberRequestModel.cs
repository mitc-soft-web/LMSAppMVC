namespace LMSAppMVC.Models.DTOs.Auth
{
    public class RegisterMemberRequestModel
    {
#pragma warning disable CS8618
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
#pragma warning restore CS8618
    }
}
