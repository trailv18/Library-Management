using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.Categories.Dto
{
    public class CategoryFilterDto : PagingRequestDto
    {
        public string CategoryName { get; set; }
    }
}