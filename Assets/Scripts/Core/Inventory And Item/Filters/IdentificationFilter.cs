
#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemIdentifications;

namespace Core.Inventory_And_Item.Filters
{
    public class IdentificationFilter
    {
        public readonly FilterType filterType;
        public readonly string[] strings;
        
        internal IdentificationFilter(FilterType type, string[] strings)
        {
            filterType = type;
            this.strings = strings;
        }

        public bool IsMatch(ItemIdentification? itemIdentification)
        {
            return filterType switch
            {
                FilterType.Enum => itemIdentification == null || strings.Contains(itemIdentification.GetType().FullName),
                FilterType.UEnum => itemIdentification == null || !strings.Contains(itemIdentification.GetType().FullName),
                FilterType.Feature => itemIdentification == null || strings.Any(itemIdentification.HasFeature),
                FilterType.UFeature => itemIdentification == null || !strings.Any(itemIdentification.HasFeature),
                FilterType.All => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public static bool operator ==(IdentificationFilter left, IdentificationFilter right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IdentificationFilter left, IdentificationFilter right)
        {
            return !(left == right);
        }

        protected bool Equals(IdentificationFilter other)
        {
            return filterType == other.filterType && strings.Equals(other.strings);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IdentificationFilter) obj);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(filterType, strings);
        }
    }
}