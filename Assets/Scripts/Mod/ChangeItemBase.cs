using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications;

namespace Mod
{
    public abstract class ChangeItemBase
    {
        public abstract string ChangeItemPackeageName();
        
        public abstract string ChangeItemName();

        public abstract ItemIdentification ChangeItem(ItemIdentification original);
    }
}