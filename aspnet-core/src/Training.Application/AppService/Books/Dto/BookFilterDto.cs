using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.Books.Dto
{
    public class BookFilterDto : PagingRequestDto
    {
        public string BookName { get; set; }
    }
}
