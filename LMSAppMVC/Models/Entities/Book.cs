using LMSAppMVC.Contracts.Entities;

namespace LMSAppMVC.Models.Entities
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required Guid AuthorId { get; set; }
        public required Guid CategoryId { get; set; }
        public required string ISBN { get; set; }
        public required int PublishedYear { get; set; }
        public required int TotalCopies { get; set; }
        public required int AvailableCopies { get; set; }

    }
}
