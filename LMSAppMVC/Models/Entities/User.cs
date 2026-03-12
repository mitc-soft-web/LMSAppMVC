using LMSAppMVC.Contracts.Entities;

namespace LMSAppMVC.Models.Entities
{
    public class User : BaseEntity
    {
        public required string Email { get; set; }
        public required string HashPassword { get; set; }
        public Librarian? Librarian { get; set; }
        public Member? Member { get; set; }
        public required Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
