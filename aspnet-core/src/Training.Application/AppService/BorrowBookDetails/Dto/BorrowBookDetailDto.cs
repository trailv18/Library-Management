using Abp.AutoMapper;
using Training.Entity.BorrowBookDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.BorrowBookDetails.Dto
{
    [AutoMapFrom(typeof(BorrowBookDetail))]
    public class BorrowBookDetailDto
    {
        public Guid Id { get; set; }
        public Guid BorrowBookId { get; set; }
        public Guid BookId { get; set; }
        public string Book { get; set; }
        public Guid LibraryId { get; set; }
        public int Qty { get; set; }
        public int PriceBorrow { get; set; }
        public int Total { get; set; }
    }
}
