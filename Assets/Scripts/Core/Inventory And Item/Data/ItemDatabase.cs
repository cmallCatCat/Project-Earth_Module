using System;
using System.Collections.Generic;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data
{
    [CreateAssetMenu]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemInfo> itemInfos = new List<ItemInfo>();
        
    }
}