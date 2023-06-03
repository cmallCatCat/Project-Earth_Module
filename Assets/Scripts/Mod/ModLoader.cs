using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Root.Expand;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException

namespace Mod
{
    public class ModLoader : MonoSingleton<ModLoader>
    {
        public Dictionary<Assembly, ModInfo> ModInfos = new Dictionary<Assembly, ModInfo>();

        public static readonly string PluginPath = AppDomain.CurrentDomain.BaseDirectory + "/BepInEx/plugins";


        private void Start()
        {
            DontDestroyOnLoad(gameObject);

#if !UNITY_EDITOR
            LoadMods();
#endif
        }

        [UsedImplicitly]
        private void LoadMods()
        {
            Assembly[] solutionAssemblies = AssemblyExpand.GetSolutionAssemblies(PluginPath);
            Debug.Log("------Load Mods Start------");

            foreach (Assembly solutionAssembly in solutionAssemblies) LoadMod(solutionAssembly);

            Debug.Log("------Load Mods End------");

            ShowMods();
        }

        private void ShowMods()
        {
            Debug.Log("------Mods items------");
            foreach (ModInfo modInfo in ModInfos.Values)
            {
                Debug.Log("----new items----");
                modInfo.newItem.Log();
                Debug.Log("----delete items----");
                modInfo.deleteItem.Log();
                Debug.Log("----change items before----");
                modInfo.changeItemBefore.Log();
                Debug.Log("----change items after----");
                modInfo.changeItemAfter.Log();
            }
        }

        private void LoadMod(Assembly modAssembly)
        {
            Type[] allTypes  = modAssembly.GetTypes();
            Type   statement = allTypes.FirstOrDefault(type => type.BaseType.Name == "BaseUnityPlugin");
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

            LoadDeleteItem(modAssembly, allTypes, modInfo);

            LoadDeleteItemList(modAssembly, allTypes, modInfo);

            LoadChangeItem(modAssembly, allTypes, modInfo);

            LoadChangeItemList(modAssembly, allTypes, modInfo);
        }

        private static void LoadNewItem(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(INewItem).GetImplements(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                ItemInfo newItem = (ItemInfo)subClass.GetMethod("Info").Invoke(instance, null);
                bool     bNew    = ItemDatabaseHandler.Instance.New(newItem);
                if (bNew) modInfo.newItem.Add(newItem);
            }
        }

        private void LoadDeleteItem(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(IDeleteItem).GetImplements(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                ItemIdentification deleteItemIdentification
                    = (ItemIdentification)subClass.GetMethod("Identification").Invoke(instance, null);

                ItemInfo deletedItem = ItemDatabaseHandler.Instance.FindItem(deleteItemIdentification);
                bool     succeed     = ItemDatabaseHandler.Instance.Delete(deleteItemIdentification);

                if (succeed) modInfo.deleteItem.Add(deletedItem);
            }
        }

        private void LoadDeleteItemList(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(IDeleteItemList).GetImplements(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                ItemIdentification[] deleteItemIdentifications
                    = (ItemIdentification[])subClass.GetMethod("Identifications").Invoke(instance, null);

                foreach (ItemIdentification deleteItemIdentification in deleteItemIdentifications)
                {
                    ItemInfo deletedItem = ItemDatabaseHandler.Instance.FindItem(deleteItemIdentification);
                    bool     succeed     = ItemDatabaseHandler.Instance.Delete(deleteItemIdentification);
                    if (succeed) modInfo.deleteItem.Add(deletedItem);
                }
            }
        }

        private static void LoadChangeItem(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(IChangeItem).GetImplements(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                ItemIdentification changeItemIdentification
                    = (ItemIdentification)subClass.GetMethod("Identification").Invoke(instance, null);

                ItemInfo original = ItemDatabaseHandler.Instance.FindItem(changeItemIdentification);
                ItemInfo changedItem
                    = (ItemInfo)subClass.GetMethod("Info").Invoke(instance, new object[]
                    { original });

                bool change = ItemDatabaseHandler.Instance.Change(changeItemIdentification, changedItem);
                if (change)
                {
                    modInfo.changeItemBefore.Add(original);
                    modInfo.changeItemAfter.Add(changedItem);
                }
            }
        }

        private static void LoadChangeItemList(Assembly modAssembly, Type[] allTypes, ModInfo modInfo)
        {
            foreach (Type subClass in typeof(IChangeItemList).GetImplements(allTypes))
            {
                object instance = modAssembly.CreateInstance(subClass.FullName);

                ItemIdentification[] changeItemIdentifications
                    = (ItemIdentification[])subClass.GetMethod("Identifications").Invoke(instance, null);

                ItemInfo[] originalList = changeItemIdentifications.Select(ItemDatabaseHandler.Instance.FindItem).ToArray();
                ItemInfo[] changedItems
                    = (ItemInfo[])subClass.GetMethod("Infos").Invoke(instance, new object[]
                    { originalList });

                for (int index = 0; index < changedItems.Length; index++)
                {
                    ItemIdentification changeItemIdentification = changeItemIdentifications[index];
                    ItemInfo           originalItem = originalList[index];
                    ItemInfo           changedItem = changedItems[index];
                    bool               succeed = ItemDatabaseHandler.Instance.Change(changeItemIdentification, changedItem);
                    if (succeed)
                    {
                        modInfo.changeItemBefore.Add(originalItem);
                        modInfo.changeItemAfter.Add(changedItem);
                    }
                }
            }
        }
    }
}