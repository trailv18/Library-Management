using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Categories;

namespace Training.AppService.Categories.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class DeleteCategoryDto
    {
        public Guid Id { get; set; }
    }
}
