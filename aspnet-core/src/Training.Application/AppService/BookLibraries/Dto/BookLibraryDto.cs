using Abp.AutoMapper;
using Training.Entity.BookLibraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.BookLibraries.Dto
{
    [AutoMapFrom(typeof(BookLibrary))]
    public class BookLibraryDto
    {
        public Guid Id { get; set; }
        public Guid LibraryId { get; set; }
        public Guid BookId { get; set; }
        public int Stock { get; set; }
    }
}
