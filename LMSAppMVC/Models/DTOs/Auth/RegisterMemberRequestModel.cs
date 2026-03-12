namespace LMSAppMVC.Models.DTOs.User
{
    public class RegisterMemberRequestModel
    {
        public required string Email { get; set; }
        public required string HashPassword { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
    }
}
