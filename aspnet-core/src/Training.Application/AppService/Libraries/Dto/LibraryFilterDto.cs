using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.Libraries.Dto
{
    public class LibraryFilterDto : PagingRequestDto
    {
        public string LibraryName { get; set; }
    }
}