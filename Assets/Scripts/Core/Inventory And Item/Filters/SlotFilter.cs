using System;
using Core.Inventory_And_Item.Data;

namespace Core.Inventory_And_Item.Filters
{
    public class SlotFilter
    {
        private Predicate<ItemSlot> _isMatch;

        internal SlotFilter(Predicate<ItemSlot> isMatch)
        {
            _isMatch = isMatch;
        }

        public bool IsMatch(ItemSlot slot)
        {
            return _isMatch.Invoke(slot);
        }
    }
}