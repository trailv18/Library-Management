using Abp.AutoMapper;
using Training.Entity.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Districts.Dto
{
    public class GetDistrictDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProvinceName { get; set; }
    }
}
