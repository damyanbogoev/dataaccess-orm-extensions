using System;
using Telerik.OpenAccess.Metadata;

namespace MayLily.DataAccess.FluentMigrator
{
    public class ChangeSetWorkerWrapper
    {
        public static ChangeSetEntry FindChangeEntryByAttributeKey(ChangeSetEntry entry, MetadataConstants key)
        {
            return ChangeSetWorkerDelegates.FindChangeEntryByAttributeKey(entry, key);
        }

        public static ChangeSetEntry Create(MetadataContainer first, MetadataContainer second)
        {
            var instance = Activator.CreateInstance("Telerik.OpenAccess", "Telerik.OpenAccess.Metadata.ChangeSetWorker").Unwrap();
            var methodInfo = instance.GetType().GetMethod("Create");

            return (ChangeSetEntry)methodInfo.Invoke(instance, new object[] { first, second });
        }

        public static void RemoveChangeSetEntries(ChangeSetEntry entry, MetadataConstants key, Action<ChangeSetEntry> changeEntryHandler)
        {
            ChangeSetWorkerDelegates.RemoveChangeSetEntries(entry, key, changeEntryHandler);
        }

        public static void RemoveDetailEntry(ChangeSetEntry entry, MetadataConstants key)
        {
            ChangeSetWorkerDelegates.RemoveDetailEntry(entry, key);
        }
    }
}
