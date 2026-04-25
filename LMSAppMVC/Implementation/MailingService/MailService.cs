using LMSAppMVC.Configuration;
using LMSAppMVC.Contracts.MailingService;
using LMSAppMVC.Contracts.Messaging;
using LMSAppMVC.Exceptions.Messaging;
using LMSAppMVC.Exceptions.TemplateEngine;
using LMSAppMVC.Interfaces.TemplateEngine;
using LMSAppMVC.Messaging.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LMSAppMVC.Implementation.MailingService
{
    public class MailService(IMailSender mailSender, IRazorEngine razorEngine,
        IOptions<EmailConfiguration> options, ILogger<MailService> logger) : IMailService
    {

        private readonly IMailSender _mailSender = mailSender ?? throw new ArgumentNullException(nameof(mailSender));
        private readonly IRazorEngine _razorEngine = razorEngine ?? throw new ArgumentNullException(nameof(razorEngine));
        private readonly EmailConfiguration _emailConfiguration = options.Value ?? throw new ArgumentException(nameof(options));
        private readonly ILogger<MailService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public async Task<bool> SendInvitationMail(string email, string name, string registrationCode, string role)
        {
            try
            {
                var model = new SendInvitation()
                {
                    Name = name,
                    Email = email,
                    RegistrationCode = registrationCode,
                    Role = role
                };
                var mailBody = await _razorEngine.ParseAsync("SendInvitationCodeMail", model);
                return await _mailSender.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.InvitationSubject, mailBody);
            }
            catch(RazorEngineException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch(MailSenderException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
