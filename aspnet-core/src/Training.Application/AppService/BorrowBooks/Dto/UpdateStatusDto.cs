using Abp.AutoMapper;
using Training.Entity.BorrowBooks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.BorrowBooks.Dto
{
    [AutoMapFrom(typeof(BorrowBook))]
    public class UpdateStatusDto
    {
        public Guid Id { get; set; }
        public DateTime DateBorrow { get; set; }
        public DateTime DateRepay { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
        public long UserId { get; set; }
    }
}
