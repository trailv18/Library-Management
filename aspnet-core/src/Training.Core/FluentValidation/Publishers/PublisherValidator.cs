using FluentValidation;
using Training.Entity.Publishers;

namespace Training.FluentValidation.Publishers
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            RuleFor(value => value.Id).NotNull();
            RuleFor(value => value.Name).NotNull().Length(5, 100);
            RuleFor(value => value.Email).NotNull().EmailAddress();
            RuleFor(value => value.Address).NotNull().Length(10, 150);
            RuleFor(value => value.Phone).NotNull().Length(10);
        }
    }
}
