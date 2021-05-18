using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Entity.Districts;
using Training.Entity.Libraries;

namespace Training.Entity.Provinces
{
    [Table("Abp.Provinces")]
    public class Province : Entity<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<District> District { get; set; }
        public virtual ICollection<Library> Library { get; set; }
    }
}
