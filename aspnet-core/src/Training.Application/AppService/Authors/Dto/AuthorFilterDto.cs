using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.Authors.Dto
{
    public class AuthorFilterDto: PagingRequestDto
    {
        public string AuthorName { get; set; }
    }
}
