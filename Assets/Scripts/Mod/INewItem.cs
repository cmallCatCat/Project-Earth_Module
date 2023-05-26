using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using JetBrains.Annotations;

namespace Mod
{
    public interface INewItem
    {
        [UsedImplicitly]
        ItemInfo ItemInfo();
    }
}