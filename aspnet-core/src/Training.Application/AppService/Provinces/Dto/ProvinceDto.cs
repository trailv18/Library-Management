﻿using Abp.AutoMapper;
using Training.Entity.Provinces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Provinces.Dto
{
    [AutoMapFrom(typeof(Province))]
    public class ProvinceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
