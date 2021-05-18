using Abp.AutoMapper;
using Training.Entity.Libraries;
using System;

namespace Training.AppService.Libraries.Dto
{
    [AutoMapFrom(typeof(Library))]
    public class LibraryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
        public Guid ProvinceId { get; set; }
    }
}
