using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser.Extensions
{
    public static partial class EntryExtensions
    {
        public static void MoveToTop(this NgxAbstractEntry entry)
        {
            var parent = entry.Parent;
            if (parent is null)
            {
                return;
            }

            var index = parent.Children.IndexOf(entry);
            if (index == -1)
            {
                return;
            }

            parent.Children.RemoveAt(index);
            parent.Children.Insert(0, entry);

        }

        public static void MoveToBottom(this NgxAbstractEntry entry)
        {
            var parent = entry.Parent;
            if (parent is null)
            {
                return;
            }
            var index = parent.Children.IndexOf(entry);
            if (index == -1)
            {
                return;
            }
            parent.Children.RemoveAt(index);
            parent.Children.Add(entry);
        }

        public static void MoveBefore(this NgxAbstractEntry entry, NgxAbstractEntry target)
        {
            var entryParent = entry.Parent;
            if (entryParent is null)
            {
                return;
            }
            var targetParent = target.Parent;
            if (targetParent is null)
            {
                return;
            }

            var index = entryParent.Children.IndexOf(target);
            if (index == -1)
            {
                return;
            }
            entryParent.Children.Remove(entry);
            targetParent.Children.Insert(index, entry);
        }

        public static void MoveAfter(this NgxAbstractEntry entry, NgxAbstractEntry target)
        {
            var entryParent = entry.Parent;
            if (entryParent is null)
            {
                return;
            }
            var targetParent = target.Parent;
            if (targetParent is null)
            {
                return;
            }
            var index = targetParent.Children.IndexOf(target);
            if (index == -1)
            {
                return;
            }
            entryParent.Children.Remove(entry);
            targetParent.Children.Insert(index + 1, entry);

        }

        public static void MoveToIndex(this NgxAbstractEntry entry, int index)
        {
            var parent = entry.Parent;
            if (parent is null)
            {
                return;
            }
            if (index < 0 || index >= parent.Children.Count)
            {
                return;
            }
            parent.Children.Remove(entry);
            parent.Children.Insert(index, entry);
        }

        public static void MoveToIndex(this NgxAbstractEntry entry, int index, NgxBlock parent)
        {
            var entryParent = entry.Parent;
            if (entryParent is null)
            {
                throw new InvalidOperationException("Entry has no parent");
            }
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            entryParent.Children.Remove(entry);

            if (parent.Children.Count == 0 || index >= parent.Children.Count)
            {
                parent.Children.Add(entry);
            }
            else if (index < 0)
            {
                parent.Children.Insert(0, entry);
            }
            else
            {
                parent.Children.Insert(index, entry);
            }

            entry.Parent = parent;
        }

        //Remove
        public static void Remove(this NgxAbstractEntry entry)
        {
            var parent = entry.Parent;
            if (parent is null)
            {
                return;
            }
            parent.Children.Remove(entry);
            entry.Parent = null;
        }
    }
}
