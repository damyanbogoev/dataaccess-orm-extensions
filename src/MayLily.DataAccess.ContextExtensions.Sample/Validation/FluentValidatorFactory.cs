using System;
using FluentValidation;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class FluentValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(Type type)
        {
            if (type == typeof(Category))
            {
                return new CategoryValidator();
            }

            throw new NotSupportedException();
        }

        public IValidator<T> GetValidator<T>()
        {
            return this.GetValidator(typeof(T)) as IValidator<T>;
        }
    }
}
