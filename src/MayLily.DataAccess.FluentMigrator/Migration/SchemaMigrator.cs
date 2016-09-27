using System;
using System.Collections.Generic;
using System.Linq;
using OpenAccessRuntime.Relational;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Relational;
using Telerik.OpenAccess.SPI;

namespace MayLily.DataAccess.FluentMigrator
{
    public class SchemaMigrator
    {
        private string connectionString;
        private BackendConfiguration backendConfiguration;
        private MetadataContainer actualModel;
        private ChangeSetEntry changeSet;

        public SchemaMigrator(MigrationSettings settings)
        {
            this.connectionString = settings.ConnectionString;
            this.backendConfiguration = settings.BackendConfiguration;
            this.actualModel = MetadataWorker.CloneMetadataContainer(settings.Metadata);
        }

        public MigrationMetadata GetMigrationMetadata()
        {
            var changeSet = this.GetChangeSet();
            var result = new MigrationMetadata();

            var tableEntries = ChangeSetWorkerWrapper.FindChangeEntryByAttributeKey(changeSet, MetadataConstants.Tables);
            foreach (ChangeSetEntry entry in tableEntries)
            {
                var wrappedItem = new ChangeSetEntryWrapper(entry);
                if (wrappedItem.ChangeType == ChangeType.Modify)
                {
                    var indexEntries = ChangeSetWorkerWrapper.FindChangeEntryByAttributeKey(entry, MetadataConstants.Indexes);
                    foreach (ChangeSetEntry indexEntry in indexEntries)
                    {
                        var wrappedIndexItem = new ChangeSetEntryWrapper(indexEntry);
                        result.AddMigrationItem(GetMetaItem(wrappedIndexItem), wrappedIndexItem.ChangeType);
                    }
                }
                else
                {
                    result.AddMigrationItem(GetMetaItem(wrappedItem), wrappedItem.ChangeType);
                }
            }

            return result;
        }

        private ChangeSetEntry GetChangeSet()
        {
            var backendModel = this.GetBackendDatabaseModel();
            this.ProcessChangeSet(backendModel);

            return this.changeSet;
        }

        private void ProcessChangeSet(MetadataContainer container)
        {
            ProcessContainer(container);

            this.changeSet = ChangeSetWorkerWrapper.Create(container, this.actualModel);
            ProcessRelationalItemChanges(this.changeSet);
        }

        private static void ProcessContainer(MetadataContainer container)
        {
            if (container == null)
            {
                return;
            }

            foreach (MetaTable table in container.Tables)
            {
                var column = table.Columns.FirstOrDefault(x => string.Equals(x.Name, "voa_version"));
                if (column != null && !string.IsNullOrEmpty(column.SqlType))
                {
                    column.SqlType = column.SqlType.ToLowerInvariant();
                }
            }
        }

        internal static void ProcessRelationalItemChanges(ChangeSetEntry changeSet)
        {
            ChangeSetWorkerWrapper.RemoveChangeSetEntries(changeSet, MetadataConstants.Tables, SchemaMigrator.ProcessTableChangeEntry);
        }

        internal static void ProcessTableChangeEntry(ChangeSetEntry tableEntry)
        {
            ChangeSetWorkerWrapper.RemoveChangeSetEntries(tableEntry, MetadataConstants.Columns, SchemaMigrator.ProcessColumnChangeEntry);
            ChangeSetWorkerWrapper.RemoveDetailEntry(tableEntry, MetadataConstants.IsJoinTable);
            ChangeSetWorkerWrapper.RemoveDetailEntry(tableEntry, MetadataConstants.PKConstraintName);
            ChangeSetWorkerWrapper.RemoveDetailEntry(tableEntry, MetadataConstants.UseDefaultMapping);
        }

        internal static void ProcessColumnChangeEntry(ChangeSetEntry columnEntry)
        {
            ChangeSetWorkerWrapper.RemoveDetailEntry(columnEntry, MetadataConstants.AdoType);
            ChangeSetWorkerWrapper.RemoveDetailEntry(columnEntry, MetadataConstants.Converter);
            ChangeSetWorkerWrapper.RemoveDetailEntry(columnEntry, MetadataConstants.TargetClass);
            ChangeSetWorkerWrapper.RemoveDetailEntry(columnEntry, MetadataConstants.TargetField);
        }

        private MetadataContainer GetBackendDatabaseModel()
        {
            if (string.IsNullOrEmpty(this.connectionString) == false)
            {
                return this.GetDatabaseSchema();
            }

            return new MetadataContainer();
        }

        private MetadataContainer GetDatabaseSchema()
        {
            var schemaReader = this.CreateSchemaReader();
            if (schemaReader != null)
            {
                Database database = null;
                try
                {
                    this.backendConfiguration.Runtime.OnlyMetadata = true;
                    database = Database.Get(connectionString, this.backendConfiguration, new MetadataContainer());
                    if (database != null)
                    {
                        ISchemaHandler handler = database.GetSchemaHandler();
                        if (handler != null && handler.DatabaseExists() == false)
                        {
                            //handler.CreateDatabase();

                            throw new InvalidOperationException("The database does not exist.");
                        }
                    }
                }
                finally
                {
                    if (database != null)
                    {
                        database.Dispose();
                        database = null;
                    }
                }

                string[] schemas = this.GetSchemas(this.actualModel);
                var parameters = new SchemaReadParameters(schemas)
                {
                    TablesAndViews = true,
                    StoredProcedures = true,
                    Indexes = true,
                    Constraints = true,
                    UserDefinedTypes = true,
                };

                return schemaReader.GetSchema(parameters);
            }

            return null;
        }

        private ISchemaReader CreateSchemaReader()
        {
            if (this.backendConfiguration == null)
            {
                return null;
            }

            var url = string.Format("Backend={0};connectionstring={1}", this.backendConfiguration.Backend, this.connectionString);

            return MappingHandler.GetSchemaReader(url, this.backendConfiguration.ProviderName);
        }

        private string[] GetSchemas(MetadataContainer container)
        {
            if (container == null || container.Schemas == null)
            {
                return new string[] { };
            }

            IList<string> containerSchemas = container.Schemas;
            int schemasCount = containerSchemas.Count;
            string[] schemas = new string[schemasCount];
            for (int i = 0; i < schemasCount; i++)
            {
                schemas[i] = containerSchemas[i];
            }

            return schemas;
        }

        private static MetaItem GetMetaItem(ChangeSetEntryWrapper wrappedItem)
        {
            return wrappedItem.FromItem != null
                        ? wrappedItem.FromItem
                        : wrappedItem.ToItem;
        }
    }
}
