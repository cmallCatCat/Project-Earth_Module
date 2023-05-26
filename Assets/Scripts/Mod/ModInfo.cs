using System;
using System.Collections.Generic;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications;

namespace Mod
{
    public class ModInfo
    {
        public List<ItemIdentification> newItem = new List<ItemIdentification>();
        public List<ItemIdentification> deleteItem= new List<ItemIdentification>();
        public List<ItemIdentification> changeItem = new List<ItemIdentification>();
        public List<string> changeItemName = new List<string>();
    }
}