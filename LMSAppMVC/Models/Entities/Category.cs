using LMSAppMVC.Contracts.Entities;

namespace LMSAppMVC.Models.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

    }
}
