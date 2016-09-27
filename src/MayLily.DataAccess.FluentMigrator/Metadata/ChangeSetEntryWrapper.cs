using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.OpenAccess.Metadata;

namespace MayLily.DataAccess.FluentMigrator
{
    public class ChangeSetEntryWrapper
    {
        private readonly ChangeSetEntry entry;

        public ChangeSetEntryWrapper(ChangeSetEntry entry)
        {
            this.entry = entry;
        }

        public ChangeSetEntry Entry
        {
            get
            {
                return this.entry;
            }
        }

        public List<ChangeSetItem> Changes
        {
            get
            {
                return ChangeSetEntryDelegates.Changes(this.Entry);
            }
        }

        public Collection<ChangeSetEntry> Children
        {
            get
            {
                return ChangeSetEntryDelegates.Children(this.Entry);
            }
        }

        public ChangeType ChangeType
        {
            get
            {
                return ChangeSetEntryDelegates.ChangeType(this.Entry);
            }
        }

        public IMetaItemAttribute Attribute
        {
            get
            {
                return ChangeSetEntryDelegates.Attribute(this.Entry);
            }
        }

        public MetaItem FromItem
        {
            get
            {
                return ChangeSetEntryDelegates.FromItem(this.Entry);
            }
        }

        public MetaItem ToItem
        {
            get
            {
                return ChangeSetEntryDelegates.ToItem(this.Entry);
            }
        }
    }
}
