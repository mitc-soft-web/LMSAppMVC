using LMSAppMVC.Contracts.Entities;
using System.Xml.Linq;

namespace LMSAppMVC.Models.Entities
{
    public class Librarian : BaseUser
    {
       public required string LibrarianRegistrationCode { get; set; }
       public required Guid UserId {  get; set; }
       public User? User { get; set; }
    }
}
