using LMSAppMVC.Models.Entities;

namespace LMSAppMVC.Models.DTOs.Book
{
    public class BookDto
    {
        public required Guid Id { get; set;  }
        public DateTime DateAdded { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Category { get; set; }
        public required string ISBN { get; set; }
        public required int PublishedYear { get; set; }
        public required int TotalCopies { get; set; }
        public required int AvailableCopies { get; set; }
    }

    public class BookDetailsResponseForMember
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Category { get; set; }
        public required string ISBN { get; set; }
        public required int PublishedYear { get; set; }
    }

    public class ListOfBookResponseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string  Author { get; set; }
        public required string Category { get; set; }
        public required int PublishedYear { get; set; }
    }
}
