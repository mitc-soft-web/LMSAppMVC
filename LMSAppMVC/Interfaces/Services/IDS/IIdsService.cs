using LMSAppMVC.Models.DTOs.IDS;

namespace LMSAppMVC.Interfaces.Services.IDS
{
    public interface IIdsService
    {
        public Task RegisterFailedAttemptAsync(string ipAddress);
        public Task ResetLoginAttemptsAsync(string ipAddress);
        public Task<LoginAttemptsResponse> CheckLoginAttemptAsync(string ipAddress);
    }
}
