namespace LMSAppMVC.Messaging.Models
{
    public class SendInvitation : Base
    {
        public required string Role { get; set; }
        public required string RegistrationCode { get; set; }
    }
}
