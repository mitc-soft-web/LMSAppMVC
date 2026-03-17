using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Models.DTOs.Book
{
    public class BookDto
    {
        public DateTime DateAdded { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Category { get; set; }
        public required string ISBN { get; set; }
        public required int PublishedYear { get; set; }
        public required int TotalCopies { get; set; }
        public required int AvailableCopies { get; set; }
    }

    public class ListOfBookResponseDto
    {
        public required string Title { get; set; }
        public required string  Author { get; set; }
        public required string Category { get; set; }
        public required int PublishedYear { get; set; }
    }
}
