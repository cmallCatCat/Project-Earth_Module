using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using JetBrains.Annotations;

namespace Mod
{
    public interface IChangeItemList
    {
        [UsedImplicitly]
        ItemIdentification[] Identifications();

        [UsedImplicitly]
        ItemInfo[] Infos(ItemInfo[] original);
    }
}