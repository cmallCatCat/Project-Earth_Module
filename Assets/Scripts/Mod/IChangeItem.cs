using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using JetBrains.Annotations;

namespace Mod
{
    public interface IChangeItem
    {
        [UsedImplicitly]
        ItemIdentification Identification();

        [UsedImplicitly]
        ItemInfo Info(ItemInfo original);
    }
}