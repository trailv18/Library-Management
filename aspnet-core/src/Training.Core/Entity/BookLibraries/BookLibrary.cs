using Abp.Domain.Entities;
using Training.Entity.Books;
using Training.Entity.Libraries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training.Entity.BookLibraries
{
    [Table("Abp.BookLibraries")]
    public class BookLibrary : Entity<Guid>
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
        public int Stock { get; set; }

    }
}
