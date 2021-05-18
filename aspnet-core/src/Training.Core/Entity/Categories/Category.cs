using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Entity.Books;

namespace Training.Entity.Categories
{
    [Table("Abp.Categories")]
    public class Category : Entity<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<Book> Book { get; set; }
    }
}
