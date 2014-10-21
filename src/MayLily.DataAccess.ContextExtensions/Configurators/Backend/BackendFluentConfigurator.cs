using System;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.ContextExtensions
{
    public class BackendFluentConfigurator : IFluentConfigurator<BackendConfiguration>, IBackendFluentConfigurator
    {
        private string configurationName;
        private ConfigurationMergeMode mergeMode;
        private BackendConfiguration config;
        private bool configViaFile;

        public BackendFluentConfigurator()
        {
            this.config = new BackendConfiguration();
        }

        public IBackendFluentConfigurator Configuration(BackendConfiguration config)
        {
            this.config = config;

            return this;
        }

        public IBackendFluentConfigurator LoadFromConfig(string configurationName, ConfigurationMergeMode mergeMode)
        {
            this.configurationName = configurationName;
            this.mergeMode = mergeMode;
            this.configViaFile = true;

            return this;
        }

        public IBackendFluentConfigurator Backend(string backend)
        {
            this.config.Backend = backend;

            return this;
        }

        public IBackendFluentConfigurator Driver(string driver)
        {
            this.config.Driver = driver;

            return this;
        }

        public IBackendFluentConfigurator ConnectionPool(Action<BackendConfiguration.ConnectionPoolConfiguration> action)
        {
            action(this.config.ConnectionPool);

            return this;
        }

        public IBackendFluentConfigurator HighLowKeyGenerator(Action<BackendConfiguration.HighLowKeyGeneratorConfiguration> action)
        {
            action(this.config.HighLowKeyGenerator);

            return this;
        }

        public IBackendFluentConfigurator Logging(Action<BackendConfiguration.LoggingConfiguration> action)
        {
            action(this.config.Logging);

            return this;
        }

        public IBackendFluentConfigurator Runtime(Action<BackendConfiguration.RuntimeConfiguration> action)
        {
            action(this.config.Runtime);

            return this;
        }

        public IBackendFluentConfigurator SecondLevelCache(Action<BackendConfiguration.SecondLevelCacheConfiguration> action)
        {
            action(this.config.SecondLevelCache);

            return this;
        }

        public BackendConfiguration Build()
        {
            if (this.configViaFile)
            {
                BackendConfiguration.MergeBackendConfigurationFromConfigFile(this.config, this.mergeMode, this.configurationName);
            }

            return this.config;
        }
    }
}
