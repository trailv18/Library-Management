using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.BorrowBooks.Dto
{
    public class BorrowBookFilterDto: PagingRequestDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Month { get; set; }
    }
}
