using System;
using System.Collections.Generic;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data
{
    [CreateAssetMenu]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemIdentification> itemIdentifications = new List<ItemIdentification>();
        
    }
}