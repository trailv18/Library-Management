using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Authors;

namespace Training.FluentValidation.Authors
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(value => value.Id).NotNull();
            RuleFor(value => value.Name).NotNull().Length(5, 100);
            RuleFor(value => value.Address).NotNull().Length(10, 150);
            RuleFor(value => value.YearOfBirth).NotNull();
            RuleFor(value => value.Phone).NotNull().Length(10);
        }
    }
}
