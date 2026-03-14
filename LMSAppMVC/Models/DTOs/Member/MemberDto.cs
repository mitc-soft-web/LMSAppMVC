namespace LMSAppMVC.Models.DTOs.Member
{
    public class MemberDto
    {
        public required string FullName { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string MembershipNumber { get; set; }
    }
}
