using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Authorization.Users;
using Training.Entity.BookLibraries;
using Training.Entity.Books;
using Training.Entity.Libraries;

namespace Training.Entity.FavoriteBooks
{
    [Table("Abp.FavoriteBooks")]
    public class FavoriteBook : Entity<Guid>
    {
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public Guid BookLibraryId { get; set; }
        public BookLibrary BookLibrary { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
