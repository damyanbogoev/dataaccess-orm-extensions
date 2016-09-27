using System;
using System.Reflection;
using Telerik.OpenAccess.Metadata;

namespace MayLily.DataAccess.FluentMigrator
{
    public static class ChangeSetWorkerDelegates
    {
        private static Type changeSetWorkerType = GetChangeSetWorkerType();
        private static Func<ChangeSetEntry, MetadataConstants, ChangeSetEntry> findChangeEntryByAttributeKey;
        private static Action<ChangeSetEntry, MetadataConstants, Action<ChangeSetEntry>> removeChangeSetEntries;
        private static Action<ChangeSetEntry, MetadataConstants> removeDetailEntry;

        public static Func<ChangeSetEntry, MetadataConstants, ChangeSetEntry> FindChangeEntryByAttributeKey
        {
            get
            {
                if (findChangeEntryByAttributeKey == null)
                {
                    findChangeEntryByAttributeKey = ReflectionUtils.CreateDelegateFor<Func<ChangeSetEntry, MetadataConstants, ChangeSetEntry>>(changeSetWorkerType, "FindChangeEntryByAttributeKey", new Type[] { typeof(ChangeSetEntry), typeof(MetadataConstants) }, BindingFlags.NonPublic | BindingFlags.Static);
                }

                return findChangeEntryByAttributeKey;
            }
        }

        public static Action<ChangeSetEntry, MetadataConstants, Action<ChangeSetEntry>> RemoveChangeSetEntries
        {
            get
            {
                if (removeChangeSetEntries == null)
                {
                    removeChangeSetEntries = ReflectionUtils.CreateDelegateFor<Action<ChangeSetEntry, MetadataConstants, Action<ChangeSetEntry>>>(changeSetWorkerType, "RemoveChangeSetEntries", new Type[] { typeof(ChangeSetEntry), typeof(MetadataConstants), typeof(Action<ChangeSetEntry>) }, BindingFlags.NonPublic | BindingFlags.Static);
                }

                return removeChangeSetEntries;
            }
        }

        public static Action<ChangeSetEntry, MetadataConstants> RemoveDetailEntry
        {
            get
            {
                if (removeDetailEntry == null)
                {
                    removeDetailEntry = ReflectionUtils.CreateDelegateFor<Action<ChangeSetEntry, MetadataConstants>>(changeSetWorkerType, "RemoveDetailEntry", new Type[] { typeof(ChangeSetEntry), typeof(MetadataConstants) }, BindingFlags.NonPublic | BindingFlags.Static);
                }

                return removeDetailEntry;
            }
        }

        private static Type GetChangeSetWorkerType()
        {
            var assembly = Assembly.Load("Telerik.OpenAccess");

            return assembly.GetType("Telerik.OpenAccess.Metadata.ChangeSetWorker");
        }
    }
}
