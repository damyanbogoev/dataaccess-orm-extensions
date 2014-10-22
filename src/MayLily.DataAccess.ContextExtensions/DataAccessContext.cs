using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace MayLily.DataAccess.ContextExtensions
{
    public class DataAccessContext : OpenAccessContext
    {
        public DataAccessContext(string connectionString, string cacheKey, BackendConfiguration backendConfiguration, MetadataContainer container)
            : base(connectionString, cacheKey, backendConfiguration, container)
        {
        }

        public DataAccessContext(DataAccessContext context)
            : base(context)
        {
            this.ShouldValidateEntities = context.ShouldValidateEntities;
            this.Validator = context.Validator;
        }

        public IDataAccessValidator Validator
        {
            get;
            set;
        }

        public bool ShouldValidateEntities
        {
            get;
            set;
        }

        public override void SaveChanges(ConcurrencyConflictsProcessingMode failureMode)
        {
            this.Validate();

            base.SaveChanges(failureMode);
        }

        protected virtual void Validate()
        {
            if (this.ShouldValidateEntities == false || this.Validator == null)
            {
                return;
            }

            var changes = this.GetChanges();

            this.ValidateEntities(changes.GetInserts<object>());
            this.ValidateEntities(changes.GetUpdates<object>());
        }

        protected virtual void ValidateEntities(IList<object> entities)
        {
            IEnumerable<ValidationError> errors;
            foreach (var entity in entities)
            {
                if (this.Validator.TryValidate(entity, out errors) == false)
                {
                    this.ThrowValidationException(errors);
                }
            }
        }

        private void ThrowValidationException(IEnumerable<ValidationError> errors)
        {
            var aggregatedErrors = string.Join(Environment.NewLine, errors.Select(e => string.Concat("--", e.ErrorMessage)));
            var errorMessage = string.Format("Validation failed: {0}{1}", Environment.NewLine, aggregatedErrors);

            throw new InvalidOperationException(errorMessage);
        }
    }
}
