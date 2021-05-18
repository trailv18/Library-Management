using Abp.AutoMapper;
using Training.Entity.Books;
using System;

namespace Training.AppService.Books.Dto
{
    [AutoMapFrom(typeof(Book))]
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PriceBorrow { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid AuthorId { get; set; }
        public int YearPublic { get; set; }
    }
}
