using System.Collections.Generic;

namespace MayLily.DataAccess.ContextExtensions
{
    public interface IDataAccessValidator
    {
        bool TryValidate(object instance, out IEnumerable<ValidationError> errors);

        IEnumerable<ValidationError> Validate(object instance);
    }
}
