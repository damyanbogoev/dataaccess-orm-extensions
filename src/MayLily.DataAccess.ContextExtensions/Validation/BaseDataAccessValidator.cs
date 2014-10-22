using System.Collections.Generic;
using System.Linq;

namespace MayLily.DataAccess.ContextExtensions
{
    public abstract class BaseDataAccessValidator : IDataAccessValidator
    {
        public virtual IEnumerable<ValidationError> Validate(object instance)
        {
            IEnumerable<ValidationError> errors;
            if (this.TryValidate(instance, out errors))
            {
                return errors;
            }

            return Enumerable.Empty<ValidationError>();
        }

        public abstract bool TryValidate(object instance, out IEnumerable<ValidationError> errors);
    }
}
