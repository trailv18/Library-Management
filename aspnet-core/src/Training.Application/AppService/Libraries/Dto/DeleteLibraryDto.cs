using Abp.AutoMapper;
using Training.Entity.Libraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.Libraries.Dto
{
    [AutoMapFrom(typeof(Library))]
    public class DeleteLibraryDto
    {
        public Guid Id { get; set; }
    }
}
