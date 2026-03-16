using LMSAppMVC.Contracts.Enums;

namespace LMSAppMVC.Models.DTOs.Author
{
    public class CreateAuthorRequestModel
    {
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public required string Country { get; set; }
    }
}
