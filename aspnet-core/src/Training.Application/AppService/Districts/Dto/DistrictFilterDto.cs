using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.Districts.Dto
{
    public class DistrictFilterDto : PagingRequestDto
    {
        public Guid Id { get; set; }
        public string DistrictName { get; set; }
    }
}