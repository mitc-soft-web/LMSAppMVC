namespace LMSAppMVC.Models.DTOs.Book
{
    public class AddBookRequestModel
    {
        public required string Title { get; set; }
        public required Guid AuthorId { get; set; }
        public required Guid CategoryId { get; set; }
        public required string ISBN { get; set; }
        public required int PublishedYear { get; set; }
        public required int TotalCopies { get; set; }
    }
}
