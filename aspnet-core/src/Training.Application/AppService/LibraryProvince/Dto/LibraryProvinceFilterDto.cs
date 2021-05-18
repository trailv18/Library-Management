using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.LibraryProvince.Dto
{
    public class LibraryProvinceFilterDto : PagingRequestDto
    {
        public Guid ProvinceId { get; set; }
    }
}
