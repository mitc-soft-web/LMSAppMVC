namespace LMSAppMVC.Models.DTOs
{
    public class AddBookRequestModel
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required int ISBNYear { get; set; }
    }
}
