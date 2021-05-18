using System;

namespace Training.AppService.Books.Dto
{
    public class GetBookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PriceBorrow { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public int YearPublic { get; set; }
    }
}
