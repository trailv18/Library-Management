using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Entity.Books;

namespace Training.Entity.Authors
{
    [Table("Abp.Authors")]
    public class Author : Entity<Guid>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Book> Book { get; set; }
    }
}
