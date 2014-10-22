using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class DataAnnotationsValidator : BaseDataAccessValidator
    {
        public override bool TryValidate(object instance, out IEnumerable<ValidationError> errors)
        {
            ICollection<ValidationResult> results;
            var result = TryValidate(instance, out results);
            errors = DataAnnotationsValidator.Map(results);

            return result;
        }

        private static bool TryValidate(object instance, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(instance, serviceProvider: null, items: null);
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(instance, context, results, validateAllProperties: true);
        }

        private static IEnumerable<ValidationError> Map(IEnumerable<ValidationResult> validationResults)
        {
            var result = new List<ValidationError>();
            foreach (var validationResult in validationResults)
            {
                result.Add(new ValidationError(validationResult.ErrorMessage));
            }

            return result;
        }
    }
}
