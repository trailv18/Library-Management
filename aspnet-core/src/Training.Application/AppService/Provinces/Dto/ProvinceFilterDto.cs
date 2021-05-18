using System;
using Training.Entity.Paging;

namespace Training.AppService.Provinces.Dto
{
    public class ProvinceFilterDto : PagingRequestDto
    {
        public string ProvinceName { get; set; }
    }
}
