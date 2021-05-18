using Abp.AutoMapper;
using Training.Entity.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Districts.Dto
{
    [AutoMapFrom(typeof(District))]
    public class DistrictDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
    }
}
