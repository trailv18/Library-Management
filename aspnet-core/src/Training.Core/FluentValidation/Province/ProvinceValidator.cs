using FluentValidation;
using System;
using System.Text;
using Training.Entity.Provinces;

namespace Training.FluentValidation.Provinces
{
    public class ProvinceValidator : AbstractValidator<Province>
    {
        public ProvinceValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Name).NotNull().Length(5, 100);
        }
    }
}
