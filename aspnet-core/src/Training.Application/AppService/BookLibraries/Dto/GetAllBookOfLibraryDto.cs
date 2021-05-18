using System;

namespace Training.AppService.BookLibraries.Dto
{
    public class GetAllBookOfLibraryDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid LibraryId { get; set; }
        public string BookName { get; set; }
        public int PriceBorrow { get; set; }
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public int Stock { get; set; }
        public string Publisher { get; set; }
        public Guid PublisherId { get; set; }
        public string Author { get; set; }
        public Guid AuthorId { get; set; }
        public int YearPublic { get; set; }
    }
}
