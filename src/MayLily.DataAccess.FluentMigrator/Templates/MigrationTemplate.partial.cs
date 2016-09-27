namespace MayLily.DataAccess.FluentMigrator
{
    public partial class MigrationTemplate
    {
        public string Name
        {
            get;
            set;
        }

        public long Version
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public MigrationMetadata Metadata
        {
            get;
            set;
        }
    }
}
