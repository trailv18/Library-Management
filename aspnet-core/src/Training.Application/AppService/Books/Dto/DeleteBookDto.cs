using Abp.AutoMapper;
using Training.Entity.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Books.Dto
{
    [AutoMapFrom(typeof(Book))]
    public class DeleteBookDto
    {
        public Guid Id { get; set; }
    }
}
