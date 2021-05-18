using Abp.AutoMapper;
using Training.Entity.BorrowBookDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.BorrowBookDetails.Dto
{
    [AutoMapFrom(typeof(BorrowBookDetail))]
    public class DeleteBorrowBookDetailDto
    {
        public Guid Id { get; set; }
    }
}
