using System;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.ContextExtensions
{
    public interface IBackendFluentConfigurator
    {
        IBackendFluentConfigurator Configuration(BackendConfiguration config);

        IBackendFluentConfigurator LoadFromConfig(string configurationName, ConfigurationMergeMode mergeMode);

        IBackendFluentConfigurator Backend(string backend);

        IBackendFluentConfigurator Driver(string driver);

        IBackendFluentConfigurator ConnectionPool(Action<BackendConfiguration.ConnectionPoolConfiguration> action);

        IBackendFluentConfigurator HighLowKeyGenerator(Action<BackendConfiguration.HighLowKeyGeneratorConfiguration> action);

        IBackendFluentConfigurator Logging(Action<BackendConfiguration.LoggingConfiguration> action);

        IBackendFluentConfigurator Runtime(Action<BackendConfiguration.RuntimeConfiguration> action);

        IBackendFluentConfigurator SecondLevelCache(Action<BackendConfiguration.SecondLevelCacheConfiguration> action);
    }
}
