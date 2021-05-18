using FluentValidation;
using Training.Entity.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Validator.Districts
{
    public class DistrictValidator : AbstractValidator<District>
    {
        public DistrictValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Name).NotNull().Length(5, 100);
            RuleFor(c => c.ProvinceId).NotNull();
        }
    }
}
