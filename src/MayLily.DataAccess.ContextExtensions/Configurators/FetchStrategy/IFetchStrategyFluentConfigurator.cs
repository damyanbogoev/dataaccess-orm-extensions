using System;
using Telerik.OpenAccess.FetchOptimization;

namespace MayLily.DataAccess.ContextExtensions
{
    public interface IFetchStrategyFluentConfigurator
    {
        IFetchStrategyFluentConfigurator Set(FetchStrategy strategy);
        IFetchStrategyFluentConfigurator Options(Action<FetchStrategy> action);
    }
}
