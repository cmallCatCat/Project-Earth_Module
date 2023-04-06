#nullable enable
using System;
using Core.Inventory_And_Item.Data;

namespace Core.Inventory_And_Item.Filters
{
    public class IdentificationFilter
    {
        private Predicate<ItemIdentification?> _isMatch;

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