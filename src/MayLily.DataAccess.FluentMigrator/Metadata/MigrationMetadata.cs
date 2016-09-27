using System;
using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Relational;

namespace MayLily.DataAccess.FluentMigrator
{
    public class MigrationMetadata
    {
        private readonly IList<MetaTable> tablesToCreate;
        private readonly IList<MetaTable> tablesToDrop;
        private readonly IList<MetaIndex> indicesToCreate;
        private readonly IList<MetaIndex> indicesToDrop;

        public MigrationMetadata()
        {
            this.tablesToCreate = new List<MetaTable>();
            this.tablesToDrop = new List<MetaTable>();
            this.indicesToCreate = new List<MetaIndex>();
            this.indicesToDrop = new List<MetaIndex>();
        }

        public IEnumerable<MetaTable> TablesToCreate
        {
            get
            {
                return this.tablesToCreate;
            }
        }

        public IEnumerable<MetaTable> TablesToDrop
        {
            get
            {
                return this.tablesToDrop;
            }
        }

        public IEnumerable<MetaIndex> IndicesToCreate
        {
            get
            {
                return this.indicesToCreate;
            }
        }

        public IEnumerable<MetaIndex> IndicesToDrop
        {
            get
            {
                return this.indicesToDrop;
            }
        }

        public void AddMigrationItem(object metaItem, ChangeType changeType)
        {
            switch (changeType)
            {
                case ChangeType.Add:
                    this.MarkItemForAdding(metaItem);
                    break;
                case ChangeType.Remove:
                    this.MarkItemForDropping(metaItem);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private void MarkItemForAdding(object metaItem)
        {
            if (metaItem is MetaTable)
            {
                this.tablesToCreate.Add(metaItem as MetaTable);
            }
            else if (metaItem is MetaIndex)
            {
                this.indicesToCreate.Add(metaItem as MetaIndex);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private void MarkItemForDropping(object metaItem)
        {
            if (metaItem is MetaTable)
            {
                this.tablesToDrop.Add(metaItem as MetaTable);
            }
            else if (metaItem is MetaIndex)
            {
                this.indicesToDrop.Add(metaItem as MetaIndex);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
