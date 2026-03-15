using LMSAppMVC.Contracts.MailingService;
using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Auth;
using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Implementation.Services
{
    public class LibrarianRegistrationCodeService(ILibrarianRegistrationCodeRepository librarianRegistrationCode,
        ILogger<LibrarianRegistrationCodeService> logger, IMailService mailService,
        IUnitOfWork unitOfWork) : ILibrarianRegistrationCodeService
    {
        private readonly ILibrarianRegistrationCodeRepository _librarianRegistrationCodeRepository = librarianRegistrationCode ?? throw new ArgumentNullException(nameof(librarianRegistrationCode));
        private readonly ILogger<LibrarianRegistrationCodeService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));   
        private readonly IMailService _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        public async Task<BaseResponse<bool>> GenerateLibrarianRegistrationCodeAsync(GenerateLibrarianRegistratationCodeRequestModel request)
        {

            var librarianRegistrationCode = new LibrarianRegistrationCodeGenerator
            {
                FullName = request.FullName,
                Email = request.Email,
                IsUsed = false,
                Expiry = DateTime.UtcNow.AddDays(1),
                DateCreated = DateTime.UtcNow,
                LibrarianRegistrationCode =  await GenerateLibrarianRegistrationCode()


            };
            var createCode = await _librarianRegistrationCodeRepository.Add<LibrarianRegistrationCodeGenerator>(librarianRegistrationCode);
            await _unitOfWork.SaveChangesAsync();
            if(createCode == null)
            {
                _logger.LogError("Code couldn't be saved");
                return new BaseResponse<bool>
                {
                    Message = "Code couldn't be saved",
                    Status = false
                };
            }

            var sent = await _mailService.SendInvitationMail(
                librarianRegistrationCode.Email,
                librarianRegistrationCode.FullName,
                librarianRegistrationCode.LibrarianRegistrationCode,
                "Librarian");

            if(!sent)
            {
                return new BaseResponse<bool>
                {
                    Message = "Email couldn't be sent",
                    Status = false
                };
            }

            _logger.LogInformation("Code generated and email sent successfully");
            return new BaseResponse<bool>
            {
                Message = "Code generated and email sent successfully",
                Status = true
            };

        }

        private async Task<string> GenerateLibrarianRegistrationCode()
        {
            string registrationCode;
            bool exists;

            do
            {
                registrationCode = $"LIB-{Guid.NewGuid().ToString().Substring(0, 5).Replace("-", "").ToUpper()}";
                exists = await _librarianRegistrationCodeRepository.Any<LibrarianRegistrationCodeGenerator>(l => l.LibrarianRegistrationCode == registrationCode);

            }
            while (exists);
            return registrationCode;
        }
    }
}
