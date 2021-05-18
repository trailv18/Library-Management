using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.FavoriteBooks;

namespace Training.AppService.FavoriteBooks.Dto
{
    [AutoMapFrom(typeof(FavoriteBook))]
    public class DeleteFavoriteBookDto
    {
        public Guid Id { get; set; }
    }
}
