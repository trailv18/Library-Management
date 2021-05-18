using Abp.AutoMapper;
using Training.Entity.BorrowBookDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.BorrowBookDetails.Dto
{
    public class GetAllBorrowBookDetailDto
    {
        public Guid Id { get; set; }
        public string Book { get; set; }
        public string Library { get; set; }
        public int Qty { get; set; }
        public int PriceBorrow { get; set; }
        public int Total { get; set; }
    }
}
