namespace LMSAppMVC.Models.DTOs.Auth
{
    public class GenerateLibrarianRegistratationCodeRequestModel
    {
        public required string Email { get; set; }
        public required string FullName { get; set; }
    }
}
