using FluentValidation;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty();
        }
    }
}
