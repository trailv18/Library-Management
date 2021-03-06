using Abp.AutoMapper;
using Training.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Authors.Dto
{
    public class GetAuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
    }
}
