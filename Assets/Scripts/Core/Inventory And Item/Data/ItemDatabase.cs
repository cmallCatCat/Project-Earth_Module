using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemInfos;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    [CreateAssetMenu]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemInfo> itemInfos = new List<ItemInfo>();
    }
}