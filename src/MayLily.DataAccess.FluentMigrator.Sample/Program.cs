using MayLily.DataAccess.ContextExtensions;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.FluentMigrator.Sample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var context = Db.Init()
                .ConnectionString(c => c.LoadFromConfig("MyConnection"))
                .BackendConfiguration(b => b.LoadFromConfig("MssqlConfiguration", ConfigurationMergeMode.ConfigFileDefinitionWins))
                .MetadataContainer(m => m.Source<MyMetadataSource>().NullForeignKey())
                .FetchStrategy(fs => { })
                .CacheKey("MyCacheKey")
                .EnableValidation(new DataAnnotationsValidator())
                .Build<MyContext>(migrateSchema: false);

            using (context)
            {
                var settings = new MigrationSettings
                {
                    BasePath = @"..\..\Migrations",
                    BackendConfiguration = new BackendConfiguration { Backend = "mssql", ProviderName = "System.Data.SqlClient" },
                    ConnectionString = context.Connection.ConnectionString,
                    Description = "Adding a products table.",
                    Metadata = context.Metadata,
                    Name = "CreateProductsTable"
                };

                FluentMigratorClassGenerator.Generate(settings);
            }
        }
    }
}
