using System;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.ContextExtensions
{
    public class Db
    {
        private readonly ConnectionStringFluentConfigurator connectionString;
        private readonly BackendFluentConfigurator backend;
        private readonly MetadataFluentConfigurator metadata;
        private readonly FetchStrategyFluentConfigurator strategy;
        private string cacheKey;

        private Db()
        {
            this.connectionString = new ConnectionStringFluentConfigurator();
            this.backend = new BackendFluentConfigurator();
            this.metadata = new MetadataFluentConfigurator();
            this.strategy = new FetchStrategyFluentConfigurator();
        }

        public static Db Init()
        {
            return new Db();
        }

        public Db ConnectionString(Action<ConnectionStringFluentConfigurator> action)
        {
            action(this.connectionString);

            return this;
        }

        public Db BackendConfiguration(Action<IBackendFluentConfigurator> action)
        {
            action(this.backend);

            return this;
        }

        public Db MetadataContainer(Action<IMetadataFluentConfigurator> action)
        {
            action(this.metadata);

            return this;
        }

        public Db FetchStrategy(Action<IFetchStrategyFluentConfigurator> action)
        {
            action(this.strategy);

            return this;
        }

        public Db CacheKey(string cacheKey)
        {
            this.cacheKey = cacheKey;

            return this;
        }

        public OpenAccessContext Build(bool migrateSchema = false)
        {
            var result = new OpenAccessContext(this.connectionString.Build(), this.cacheKey, this.backend.Build(), this.metadata.Build());
            result.FetchStrategy = this.strategy.Build();
            if (migrateSchema)
            {
                Db.MigrateSchema(result);
            }

            return result;
        }

        public TContext Build<TContext>(bool migrateSchema = false)
            where TContext : OpenAccessContext
        {
            var type = typeof(TContext);
            var ctor = type.GetConstructor(new[] { typeof(OpenAccessContext) });

            return ctor.Invoke(new object[] { this.Build(migrateSchema) }) as TContext;
        }

        private static void MigrateSchema(OpenAccessContext context)
        {
            var schemaHandler = context.GetSchemaHandler();
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (string.IsNullOrEmpty(script) == false)
            {
                schemaHandler.ForceExecuteDDLScript(script);
            }
        }
    }
}
