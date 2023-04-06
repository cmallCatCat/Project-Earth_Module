#nullable enable
using System.Collections.Generic;
using Core.Inventory_And_Item.Data;

namespace Core.Inventory_And_Item.Filters
{
    public class CustomIdentificationFilter : IdentificationFilter
    {
        internal CustomIdentificationFilter(List<ItemIdentification> itemIdentifications) :
            base(i =>
            {
                if (i == null) return true;
                return itemIdentifications.Contains(i);
            })
        {
            ItemIdentifications = itemIdentifications;
        }

        internal List<ItemIdentification> ItemIdentifications { get; }
    }
}