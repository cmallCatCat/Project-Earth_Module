using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extents;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using QFramework;
using UnityEngine;

namespace Mod
{
    public class ModLoader : MonoBehaviour
    {
        public static ModLoader Instance;
        public Dictionary<Assembly, ModInfo> ModInfos;
        public ResLoader resLoader = ResLoader.Allocate();

        public static readonly string PluginPath = AppDomain.CurrentDomain.BaseDirectory + "/BepInEx/plugins";


        // Start is called before the first frame update
        void Start()
        {
            ResKit.Init();
            Instance = this;
            DontDestroyOnLoad(gameObject);

#if !UNITY_EDITOR
            LoadPlugins();
#endif
        }

        private void LoadPlugins()
        {
            Assembly[] solutionAssemblies = AssemblyExtents.GetSolutionAssemblies(PluginPath);
            Debug.Log("-----Load Mods Start-----");
            Initialize();

            foreach (Assembly solutionAssembly in solutionAssemblies)
            {
                LoadMod(solutionAssembly);
            }

            Debug.Log("-----Load Mods End-----");
        }

        private void Initialize()
        {
            ModInfos = new Dictionary<Assembly, ModInfo>();
        }

        private void LoadMod(Assembly modAssembly)
        {
            Type[] allTypes = modAssembly.GetTypes();
            Type statement = allTypes.FirstOrDefault(type => type.BaseType.Name == "BaseUnityPlugin");
            Debug.Log("Mod Loading: " + statement);
            if (statement == null)
            {
                Debug.LogWarning("Mod Statement not found");
                return;
            }

            ModInfo modInfo = new ModInfo();
            if (!ModInfos.TryAdd(modAssembly, modInfo))
            {
                Debug.LogWarning("Mod already loaded");
                return;
            }

            LoadNewItem(modAssembly, allTypes, modInfo);

            LoadChangeItem(modAssembly, allTypes, modInfo);

            LoadDeleteItem(modAssembly, allTypes, modInfo);
        }

        private static void LoadNewItem(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(NewItemBase).GetSubClasses(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                ItemIdentification newItem
                    = (ItemIdentification)subClass.GetMethod("NewItem").Invoke(instance, null);
                bool bNew = ItemDatabaseHandler.New(newItem);
                if (bNew)
                {
                    modInfo.newItem.Add(newItem);
                }
            }
        }

        private void LoadDeleteItem(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(DeleteItemBase).GetSubClasses(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                string itemPackageName = subClass.GetMethod("DeleteItemPackageName").Invoke(instance, null) as string;
                string itemName = subClass.GetMethod("DeleteItemName").Invoke(instance, null) as string;
                
                ItemIdentification deletedItem = ItemDatabaseHandler.FindItem(itemName, itemPackageName);
                bool delete = ItemDatabaseHandler.Delete(itemName, itemPackageName);
                
                if (delete)
                {
                    modInfo.deleteItem.Add(deletedItem);
                }
            }
        }

        private static void LoadChangeItem(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(ChangeItemBase).GetSubClasses(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                string itemPackageName = subClass.GetMethod("ChangeItemPackeageName").Invoke(instance, null) as string;
                string itemName = subClass.GetMethod("ChangeItemName").Invoke(instance, null) as string;

                ItemIdentification original = ItemDatabaseHandler.FindItem(itemName);
                ItemIdentification changedItem
                    = (ItemIdentification)subClass.GetMethod("ChangeItem").Invoke(instance, new object[] { original });

                bool change = ItemDatabaseHandler.Change(itemName,changedItem,itemPackageName);
                if (change)
                {
                    modInfo.changeItemName.Add(itemName);
                }
            }
        }
    }
}