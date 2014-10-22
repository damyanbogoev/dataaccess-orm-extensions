namespace MayLily.DataAccess.ContextExtensions
{
    public class ValidationError
    {
        public ValidationError(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }
    }
}
