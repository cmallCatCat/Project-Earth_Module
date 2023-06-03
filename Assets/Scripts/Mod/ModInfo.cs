using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemInfos;

namespace Mod
{
    public class ModInfo
    {
        public List<ItemInfo> newItem          = new List<ItemInfo>();
        public List<ItemInfo> deleteItem       = new List<ItemInfo>();
        public List<ItemInfo> changeItemBefore = new List<ItemInfo>();
        public List<ItemInfo> changeItemAfter  = new List<ItemInfo>();
    }
}