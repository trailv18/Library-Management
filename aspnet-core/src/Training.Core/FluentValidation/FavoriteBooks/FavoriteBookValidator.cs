using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.FavoriteBooks;

namespace Training.FluentValidation.FavoriteBooks
{
    public class FavoriteBookValidator : AbstractValidator<FavoriteBook>
    {
        public FavoriteBookValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.UserId).NotNull();
            RuleFor(c => c.BookLibraryId).NotNull();
            RuleFor(c => c.BookId).NotNull();
            RuleFor(c => c.LibraryId).NotNull();
        }
    }
}
