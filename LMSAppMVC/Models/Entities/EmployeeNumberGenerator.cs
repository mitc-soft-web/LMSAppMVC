using LMSAppMVC.Contracts.Entities;

namespace LMSAppMVC.Models.Entities
{
    public class EmployeeNumberGenerator : BaseEntity
    {
        public required string EmployeeNumber { get; set; }
    }
}
