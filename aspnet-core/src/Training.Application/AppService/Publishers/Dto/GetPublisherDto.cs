using Abp.AutoMapper;
using Training.Entity.Publishers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Publishers.Dto
{
    public class GetPublisherDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
