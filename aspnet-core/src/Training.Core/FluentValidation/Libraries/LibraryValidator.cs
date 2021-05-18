using FluentValidation;
using Training.Entity.Libraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Validator.Libraries
{
    public class LibraryValidator: AbstractValidator<Library>
    {
        public LibraryValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Name).NotNull().Length(5, 100);
            RuleFor(c => c.DistrictId).NotNull();
            RuleFor(c => c.ProvinceId).NotNull();
        }
    }
}
