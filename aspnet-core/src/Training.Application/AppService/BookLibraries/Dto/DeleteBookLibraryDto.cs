using Abp.AutoMapper;
using Training.Entity.BookLibraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.BookLibraries.Dto
{
    [AutoMapFrom(typeof(BookLibrary))]
    public class DeleteBookLibraryDto
    {
        public Guid Id { get; set; }
    }
}
