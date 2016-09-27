using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace MayLily.DataAccess.FluentMigrator
{
    public class MigrationSettings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string BasePath { get; set; }

        public string ConnectionString { get; set; }

        public BackendConfiguration BackendConfiguration { get; set; }

        public MetadataContainer Metadata { get; set; }
    }
}
