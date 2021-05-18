using System;
using System.Collections.Generic;
using System.Text;

namespace Training.AppService.FavoriteBooks.Dto
{
    public class GetFavoriteBookDto
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }
        public Guid BookLibraryId { get; set; }
        public Guid LibraryId { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string LibraryName { get; set; }
    }
}
