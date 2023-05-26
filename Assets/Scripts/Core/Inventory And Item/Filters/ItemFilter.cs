#nullable enable
using System;
using System.Linq;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;

namespace InventoryAndItem.Core.Inventory_And_Item.Filters
{
    public class ItemFilter
    {
        public readonly FilterType filterType;
        public readonly string[] strings;

        internal ItemFilter(FilterType type, string[] strings)
        {
            filterType = type;
            this.strings = strings;
        }

        public bool IsMatch(ItemInfo? itemInfo, ItemDecorator? itemDecorator)
        {
            if (itemInfo == null || itemDecorator == null)
            {
                return true;
            }

            return filterType switch
            {
                FilterType.Enum => strings.Contains(itemInfo.GetType().FullName),
                FilterType.UEnum => !strings.Contains(itemInfo.GetType().FullName),
                FilterType.Feature => strings.Any(itemInfo.HasFeature) ||
                                      strings.Any(itemDecorator.HasFeature),
                FilterType.UFeature => !(strings.Any(itemInfo.HasFeature) ||
                                         strings.Any(itemDecorator.HasFeature)),
                FilterType.All => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static bool operator ==(ItemFilter left, ItemFilter right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ItemFilter left, ItemFilter right)
        {
            return !(left == right);
        }

        protected bool Equals(ItemFilter other)
        {
            return filterType == other.filterType && strings.Equals(other.strings);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ItemFilter)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(filterType, strings);
        }
    }
}