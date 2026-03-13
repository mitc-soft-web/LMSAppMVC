namespace LMSAppMVC.Contracts.Services
{
    public interface IIdentityService
    {
        public string GetPasswordHash(string password, string salt = null!);
    }
}
