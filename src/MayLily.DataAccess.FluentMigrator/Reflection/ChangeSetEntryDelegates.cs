using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Telerik.OpenAccess.Metadata;

namespace MayLily.DataAccess.FluentMigrator
{
    public static class ChangeSetEntryDelegates
    {
        private static Func<ChangeSetEntry, List<ChangeSetItem>> changes;
        private static Func<ChangeSetEntry, Collection<ChangeSetEntry>> children;
        private static Func<ChangeSetEntry, ChangeType> changeType;
        private static Func<ChangeSetEntry, IMetaItemAttribute> attribute;
        private static Func<ChangeSetEntry, MetaItem> fromItem;
        private static Func<ChangeSetEntry, MetaItem> toItem;

        public static Func<ChangeSetEntry, List<ChangeSetItem>> Changes
        {
            get
            {
                if (changes == null)
                {
                    changes = ReflectionUtils.CreateDelegateFor<ChangeSetEntry, Func<ChangeSetEntry, List<ChangeSetItem>>>("get_Changes", new Type[] { }, BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return changes;
            }
        }

        public static Func<ChangeSetEntry, Collection<ChangeSetEntry>> Children
        {
            get
            {
                if (children == null)
                {
                    children = ReflectionUtils.CreateDelegateFor<ChangeSetEntry, Func<ChangeSetEntry, Collection<ChangeSetEntry>>>("get_Children", new Type[] { }, BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return children;
            }
        }

        public static Func<ChangeSetEntry, ChangeType> ChangeType
        {
            get
            {
                if (changeType == null)
                {
                    changeType = ReflectionUtils.CreateDelegateFor<ChangeSetEntry, Func<ChangeSetEntry, ChangeType>>("get_ChangeType", new Type[] { }, BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return changeType;
            }
        }

        public static Func<ChangeSetEntry, IMetaItemAttribute> Attribute
        {
            get
            {
                if (attribute == null)
                {
                    attribute = ReflectionUtils.CreateDelegateFor<ChangeSetEntry, Func<ChangeSetEntry, IMetaItemAttribute>>("get_Attribute", new Type[] { }, BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return attribute;
            }
        }

        public static Func<ChangeSetEntry, MetaItem> FromItem
        {
            get
            {
                if (fromItem == null)
                {
                    fromItem = ReflectionUtils.CreateDelegateFor<ChangeSetEntry, Func<ChangeSetEntry, MetaItem>>("get_FromItem", new Type[] { }, BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return fromItem;
            }
        }

        public static Func<ChangeSetEntry, MetaItem> ToItem
        {
            get
            {
                if (toItem == null)
                {
                    toItem = ReflectionUtils.CreateDelegateFor<ChangeSetEntry, Func<ChangeSetEntry, MetaItem>>("get_ToItem", new Type[] { }, BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return toItem;
            }
        }
    }
}
