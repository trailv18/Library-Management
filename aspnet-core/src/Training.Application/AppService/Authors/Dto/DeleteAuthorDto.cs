using Abp.AutoMapper;
using Training.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Authors.Dto
{
    [AutoMapFrom(typeof(Author))]
    public class DeleteAuthorDto
    {
        public Guid Id { get; set; }
    }
}
