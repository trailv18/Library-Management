using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Paging;

namespace Training.AppService.BookLibraries.Dto
{
    public class BookLibraryFilterDto: PagingRequestDto
    {
        public Guid LibraryId { get; set; }
        public string BookName { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
    }
}
