using LMSAppMVC.Contracts.Services;
using LMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace LMSAppMVC.Identity
{
    public class IdentityService(IPasswordHasher<User> passwordHasher) : IIdentityService
    {
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        public string GetPasswordHash(string password, string salt = null!)
        {
            if (string.IsNullOrEmpty(salt))
            {
                return _passwordHasher.HashPassword(new User(), password);
            }
            return _passwordHasher.HashPassword(new User(), $"{password}{salt}");


        }

    }
}
