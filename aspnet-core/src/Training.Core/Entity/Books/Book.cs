using Abp.Domain.Entities;
using Training.Entity.Authors;
using Training.Entity.Categories;
using Training.Entity.Publishers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Entity.BookLibraries;

namespace Training.Entity.Books
{
    [Table("Abp.Books")]
    public class Book : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int PriceBorrow { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public int YearPublic { get; set; }
        public IList<BookLibrary> BookLibrary { get; set; }
    }
}
