using LMSAppMVC.Contracts.Entities;
using LMSAppMVC.Contracts.Enums;
using System.Xml.Linq;

namespace LMSAppMVC.Models.Entities
{
    public class Author : BaseEntity
    {
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public required string Country {get; set;}

        public ICollection<Book> Books = new List<Book>();
        
    }
}
