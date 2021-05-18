using Abp.AutoMapper;
using Training.Entity.Authors;
using System;

namespace Training.AppService.Authors.Dto
{
    [AutoMapFrom(typeof(Author))]
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
    }
}
