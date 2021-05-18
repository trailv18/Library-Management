using Abp.AutoMapper;
using Training.Entity.Provinces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Provinces.Dto
{
    [AutoMapFrom(typeof(Province))]
    public class DeleteProvinceDto
    {
        public Guid Id { get; set; }

    }
}
