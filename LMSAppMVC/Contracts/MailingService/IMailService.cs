namespace LMSAppMVC.Contracts.MailingService
{
    public interface IMailService
    {
        public Task<bool> SendInvitationMail(string email, string name, string registrationCode, string role);
    }
}
