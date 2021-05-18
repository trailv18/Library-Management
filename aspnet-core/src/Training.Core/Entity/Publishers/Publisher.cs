using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Entity.Books;

namespace Training.Entity.Publishers
{
    [Table("Abp.Publishers")]
    public class Publisher : Entity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Book> Book { get; set; }
    }
}
