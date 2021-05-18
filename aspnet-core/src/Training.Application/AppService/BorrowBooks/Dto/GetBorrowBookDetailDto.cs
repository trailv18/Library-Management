using Abp.AutoMapper;
using System;
using Training.Entity.BorrowBookDetails;

namespace Training.AppService.BorrowBooks.Dto
{
    [AutoMapFrom(typeof(BorrowBookDetail))]
    public class GetBorrowBookDetailDto
    {
        public Guid Id { get; set; }
        public string Book { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Qty { get; set; }
        public int PriceBorrow { get; set; }
        public int Total { get; set; }
    }
}
