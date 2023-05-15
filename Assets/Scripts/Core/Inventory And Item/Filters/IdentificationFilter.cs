#nullable enable
using System;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemIdentifications;

namespace Core.Inventory_And_Item.Filters
{
    [Serializable]
    public abstract class IdentificationFilter
    {
        protected Predicate<ItemIdentification?> _isMatch;

        internal IdentificationFilter(Predicate<ItemIdentification?> isMatch)
        {
            _isMatch = isMatch;
        }

        public bool IsMatch(ItemIdentification? stack)
        {
            return _isMatch.Invoke(stack);
        }
    }
}