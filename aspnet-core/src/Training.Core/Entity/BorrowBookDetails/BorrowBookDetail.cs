using Abp.Domain.Entities;
using Training.Entity.Books;
using Training.Entity.BorrowBooks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Entity.Libraries;

namespace Training.Entity.BorrowBookDetails
{
    [Table("Abp.BorrowBookDetails")]
    public class BorrowBookDetail: Entity<Guid>
    {
        public Guid BorrowBookId { get; set; }
        public BorrowBook BorrowBook { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Library")]
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
        public int Qty { get; set; }
        public int PriceBorrow { get; set; }
        public int Total { get; set; }
    }
}
