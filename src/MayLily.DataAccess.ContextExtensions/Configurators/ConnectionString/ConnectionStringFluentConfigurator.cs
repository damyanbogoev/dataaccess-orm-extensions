using System;
using System.Configuration;

namespace MayLily.DataAccess.ContextExtensions
{
    public class ConnectionStringFluentConfigurator : IFluentConfigurator<string>
    {
        private string connectionId;
        private string connectionString;

        public ConnectionStringFluentConfigurator Set(string connectionString)
        {
            this.connectionString = connectionString;

            return this;
        }

        public ConnectionStringFluentConfigurator LoadFromConfig(string connectionId)
        {
            this.connectionId = connectionId;

            return this;
        }

        public string Build()
        {
            if (string.IsNullOrEmpty(this.connectionString) == false)
            {
                return this.connectionString;
            }

            if (string.IsNullOrEmpty(this.connectionId) == false)
            {
                return ConnectionStringFluentConfigurator.ReadConnectionString(this.connectionId);
            }

            throw new InvalidOperationException("No connection string options specified.");
        }

        private static string ReadConnectionString(string connectionId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionId];
            if (connectionString != null)
            {
                return connectionString.ConnectionString;
            }

            var appSetting = ConfigurationManager.AppSettings[connectionId];
            if (string.IsNullOrEmpty(appSetting) == false)
            {
                return appSetting;
            }

            throw new InvalidOperationException(string.Format("Unable to find connection string with id '{0}' in configuration file.", connectionId));
        }
    }
}
