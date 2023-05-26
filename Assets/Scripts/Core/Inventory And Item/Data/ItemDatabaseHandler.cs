using System;
using System.Collections.Generic;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data
{
    public static class ItemDatabaseHandler
    {
        private static ResLoader resLoader = ResLoader.Allocate();

        public static Dictionary<string, Dictionary<string, ItemIdentification>> packageList
            = new Dictionary<string, Dictionary<string, ItemIdentification>>();

        public static void Init(string assetName)
        {
            ResKit.Init();
            List<ItemIdentification> defaultItemList = resLoader.LoadSync<ItemDatabase>(assetName).itemIdentifications;
            Dictionary<string, ItemIdentification> defaultItemDic = new Dictionary<string, ItemIdentification>();
            defaultItemList.ForEach(x => defaultItemDic.Add(x.ItemName, x));
            packageList.Add("Default", defaultItemDic);
        }

        public static ItemIdentification FindItem(string itemName, string packageName = "Default")
        {
            return packageList[packageName][itemName];
        }

        public static bool New(ItemIdentification itemIdentification)
        {
            string packageName = itemIdentification.PackageName;
            if (!packageList.ContainsKey(packageName))
            {
                packageList.Add(packageName, new Dictionary<string, ItemIdentification>());
            }

            if (packageList[packageName].ContainsKey(itemIdentification.ItemName))
            {
                Debug.LogWarning("Item " + itemIdentification.ItemName + " already exists in package " + packageName);
                return false;
            }

            packageList[packageName].Add(itemIdentification.ItemName, itemIdentification);
            return true;

            // if (Database.itemIdentifications.Exists(x => x.ItemName == itemIdentification.ItemName))
            // {
            //     return false;
            // }
            //
            // Database.itemIdentifications.Add(itemIdentification);
            // return true;
        }

        public static bool Delete(string itemName, string packageName = "Default")
        {
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

        public static bool Change(string itemName, ItemIdentification itemIdentification, string packageName = "Default")
        {
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

            packageList[packageName][itemName] = itemIdentification;
            return true;
        }
    }
}