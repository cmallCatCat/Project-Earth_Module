using Core.Inventory_And_Item.Data.ItemInfos;
using JetBrains.Annotations;

namespace Mod
{
    public interface IDeleteItemList
    {
        [UsedImplicitly]
        ItemIdentification[] Identifications();
    }
}