using System;
using System.Collections.Generic;
using System.Linq;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using InventoryAndItem.Core.Inventory_And_Item.Filters;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data
{
    [UsedImplicitly]
    public class ItemDatabaseHandler: Singleton<ItemDatabaseHandler>
    {

        private ResLoader resLoader = ResLoader.Allocate();

        public Dictionary<string, Dictionary<string, ItemInfo>> packageList
            = new Dictionary<string, Dictionary<string, ItemInfo>>();

        public void Init(string assetName)
        {
            List<ItemInfo> defaultItemList = resLoader.LoadSync<ItemDatabase>(assetName).itemInfos;
            Dictionary<string, ItemInfo> defaultItemDic = new Dictionary<string, ItemInfo>();
            defaultItemList.ForEach(x => defaultItemDic.Add(x.ItemName, x));
            packageList.Add("Default", defaultItemDic);
        }

        public ItemInfo FindItem(ItemIdentification itemIdentification)
        {
            return packageList[itemIdentification.packageName][itemIdentification.name];
        }
        

        public bool New(ItemInfo itemInfo)
        {
            string packageName = itemInfo.PackageName;
            if (!packageList.ContainsKey(packageName))
            {
                packageList.Add(packageName, new Dictionary<string, ItemInfo>());
            }

            if (packageList[packageName].ContainsKey(itemInfo.ItemName))
            {
                Debug.LogWarning("Item " + itemInfo.ItemName + " already exists in package " + packageName);
                return false;
            }

            packageList[packageName].Add(itemInfo.ItemName, itemInfo);
            return true;
        }

        public bool Delete(ItemIdentification itemIdentification)
        {
            string packageName = itemIdentification.packageName;
            string itemName = itemIdentification.name;

            if (!packageList.ContainsKey(packageName))
            {
                Debug.LogWarning("Package " + packageName + " does not exist");
                return false;
            }

            if (!packageList[packageName].ContainsKey(itemName))
            {
                Debug.LogWarning("Item " + itemName + " does not exist");
                return false;
            }

            packageList[packageName].Remove(itemName);
            return true;
        }

        public bool Change( ItemIdentification itemIdentification , ItemInfo itemInfo)
        {
            string packageName = itemIdentification.packageName;
            string itemName = itemIdentification.name;

            if (!packageList.ContainsKey(packageName))
            {
                Debug.LogWarning("Package " + packageName + " does not exist");
                return false;
            }

            if (!packageList[packageName].ContainsKey(itemName))
            {
                Debug.LogWarning("Item " + itemName + " does not exist");
                return false;
            }

            packageList[packageName][itemName] = itemInfo;
            return true;
        }

        public ItemInfo[] GetAllItems(ItemFilter filter = null, string packageName = "Default")
        {
            if (!packageList.ContainsKey(packageName))
            {
                Debug.LogWarning("Package " + packageName + " does not exist");
                return Array.Empty<ItemInfo>();
            }

            if (filter == null)
            {
                filter = new ItemFilter(FilterType.All, Array.Empty<string>());
            }

            return packageList[packageName].Values.Where(x =>
                filter.IsMatch(x, new ItemDecorator())).ToArray();
        }

        public override void Dispose()
        {
            base.Dispose();
            resLoader.Recycle2Cache();
            resLoader = null;
        }

        private ItemDatabaseHandler()
        {
        }
    }
}