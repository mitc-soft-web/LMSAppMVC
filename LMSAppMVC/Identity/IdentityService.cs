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
            var dummyUser = ReturnUser();
            if (string.IsNullOrEmpty(salt))
            {
                return _passwordHasher.HashPassword(dummyUser, password);
            }
            return _passwordHasher.HashPassword(dummyUser, $"{password}{salt}");


        }

        private User ReturnUser()
        {
            var user = new User
            {
                Email = null!,
                HashPassword = null!,
                DateCreated = DateTime.UtcNow,
                RoleId = Guid.Empty
            };
            return user;
        }
    }
}
