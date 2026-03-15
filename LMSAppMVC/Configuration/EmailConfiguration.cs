namespace LMSAppMVC.Configuration
{
    public class EmailConfiguration
    {
        public string FromEmail { get; set; } = "greatmoh007@gmail.com";
        public string FromName { get; set; } = "MITC Library";
#pragma warning disable CS8618 
#pragma warning restore CS8618
        public string InvitationSubject { get; set; } = "Invitation";
    }
}
