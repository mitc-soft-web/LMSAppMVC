using LMSAppMVC.Contracts.Entities;

namespace LMSAppMVC.Models.Entities
{
    public class User : BaseEntity
    {
#pragma warning disable CS8618
        public string Email { get; set; }
        public  string HashPassword { get; set; }
        public Librarian? Librarian { get; set; }
        public Member? Member { get; set; }
        public Guid RoleId { get; set; }
#pragma warning restore CS8618
        public Role? Role { get; set; }

    }
}
