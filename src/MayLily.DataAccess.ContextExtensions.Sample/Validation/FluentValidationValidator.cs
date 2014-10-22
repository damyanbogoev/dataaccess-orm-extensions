using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class FluentValidationValidator : BaseDataAccessValidator
    {
        private readonly IValidatorFactory factory;

        public FluentValidationValidator(IValidatorFactory factory)
        {
            this.factory = factory;
        }

        public override bool TryValidate(object instance, out IEnumerable<ValidationError> errors)
        {
            var validator = this.factory.GetValidator(instance.GetType());
            var result = validator.Validate(instance);
            errors = FluentValidationValidator.Map(result.Errors);

            return result.IsValid;
        }

        private static IEnumerable<ValidationError> Map(IList<ValidationFailure> failures)
        {
            var result = new List<ValidationError>();
            foreach (var failure in failures)
            {
                result.Add(new ValidationError(failure.ErrorMessage));
            }

            return result;
        }
    }
}
