using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Entity.Categories;

namespace Training.FluentValidation.Categories
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Name).NotNull().Length(3, 100);
        }
    }
}
