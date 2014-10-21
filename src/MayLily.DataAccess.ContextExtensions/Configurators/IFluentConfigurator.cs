namespace MayLily.DataAccess.ContextExtensions
{
    public interface IFluentConfigurator<TResult>
    {
        TResult Build();
    }
}
