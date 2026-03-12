using LMSAppMVC.Contracts.Entities;

namespace LMSAppMVC.Models.Entities
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public User? User { get; set; }
    }
}
