using Abp.Domain.Entities;
using Training.Entity.Districts;
using Training.Entity.Provinces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Training.Entity.BookLibraries;

namespace Training.Entity.Libraries
{
    [Table("Abp.Libraries")]
    public class Library : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public Province Province { get; set; }
        public Guid DistrictId { get; set; }
        public District District { get; set; }
        public IList<BookLibrary> BookLibrary { get; set; }

    }
}
