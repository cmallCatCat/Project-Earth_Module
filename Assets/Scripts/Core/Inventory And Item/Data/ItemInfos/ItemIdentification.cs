using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos
{
    [Serializable]
    public class ItemIdentification
    {
        [SerializeField]
        public string packageName;
        [SerializeField]
        public string name;

        public string PackageName => packageName;
        public string Name        => name;
        
        public ItemIdentification(string packageName, string name)
        {
            this.packageName = packageName;
            this.name        = name;
        }
    }
}