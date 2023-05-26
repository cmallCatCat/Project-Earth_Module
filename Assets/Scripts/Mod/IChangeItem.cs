using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using JetBrains.Annotations;

namespace Mod
{
    public interface IChangeItem
    {
        [UsedImplicitly]
        ItemIdentification ChangeItemIdentification();

        [UsedImplicitly]
        ItemInfo ItemInfo(ItemInfo original);
    }
}