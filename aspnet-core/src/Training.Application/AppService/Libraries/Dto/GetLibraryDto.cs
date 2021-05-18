using Abp.AutoMapper;
using Training.Entity.Libraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Libraries.Dto
{
    public class GetLibraryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
    }
}
