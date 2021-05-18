using Abp.AutoMapper;
using Training.Entity.Publishers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Publishers.Dto
{
    [AutoMapFrom(typeof(Publisher))]
    public class DeletePublisherDto
    {
        public Guid Id { get; set; }
    }
}
