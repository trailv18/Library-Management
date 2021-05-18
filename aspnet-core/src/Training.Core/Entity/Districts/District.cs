using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Entity.Libraries;
using Training.Entity.Provinces;

namespace Training.Entity.Districts
{
    [Table("Abp.Districts")]
    public class District : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public Province Province { get; set; }
        public virtual ICollection<Library> Library { get; set; }
    }
}
