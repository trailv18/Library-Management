using FluentValidation;
using Training.Entity.BookLibraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Validator.BookLibraries
{
     public class BookLibraryValidator : AbstractValidator<BookLibrary>
    {
        public BookLibraryValidator()
        {
            RuleFor(value => value.Id).NotNull();
            RuleFor(value => value.BookId).NotNull();
            RuleFor(value => value.LibraryId).NotNull();
            RuleFor(value => value.Stock).NotEmpty().LessThan(1000);
        }
    }
}
