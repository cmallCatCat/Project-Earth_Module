using System.Collections.Generic;
using Core.Architectures;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using Core.QFramework;
using Core.Save_And_Load.DictionaryAndList;
using Framework;
using UnityEditor;

public class MyWindow : EditorWindow, IController
{
    InventoryModel inventoryModel;

    [MenuItem("Window/My Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        window.Show();
    }

    void OnGUI()
    {
        inventoryModel = this.GetModel<InventoryModel>();
        List<KeyValueStruct<int, Inventory>> dictionaryToList = DictionaryTransList.DictionaryToList(inventoryModel.inventories);
        foreach (KeyValueStruct<int, Inventory> keyValueStruct in dictionaryToList)
        {
            EditorGUILayout.LabelField($"{keyValueStruct.Key}");
            for (int i = 0; i < keyValueStruct.Value.Capacity; i++)
            {
                EditorGUILayout.LabelField($"{i}");
                ItemSlot itemSlot = keyValueStruct.Value.GetSlot(i);
                if (!ItemStack.IsEmpty(itemSlot.ItemStack))
                {
                    EditorGUILayout.LabelField($"{itemSlot.ItemStack.ItemIdentification}\t {itemSlot.ItemStack.Number}");
                }
                else
                {
                    EditorGUILayout.LabelField("null");
                }
            }
        }
    }

    public IArchitecture GetArchitecture()
    {
        return InGame.Interface;
    }
}