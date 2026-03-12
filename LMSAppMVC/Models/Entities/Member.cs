using LMSAppMVC.Contracts.Entities;
using System.Numerics;

namespace LMSAppMVC.Models.Entities
{
    public class Member : BaseUser
    {
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string MembershipNumber { get; set; }
        public required Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
