using Abp.AutoMapper;
using System;
using Training.Entity.FavoriteBooks;

namespace Training.AppService.FavoriteBooks.Dto
{
    [AutoMapFrom(typeof(FavoriteBook))]
    public class FavoriteBookDto
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }
        public Guid BookLibraryId { get; set; }
        public Guid BookId { get; set; }
        public Guid LibraryId { get; set; }

    }
}
