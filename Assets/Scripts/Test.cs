using System;
using System.Collections.Generic;
using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using Core.QFramework;
using Core.Save_And_Load.Command;
using Core.Save_And_Load.DictionaryAndList;
using Core.Save_And_Load.Event;
using Core.Save_And_Load.Utilities;
using Framework;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public GameObject inventoryHolder;
    public int inventoryKey;
    public ItemIdentification itemIdentification;
    private GameObject instantiate;

    private void OnGUI() 
    {
        if (GUILayout.Button("创建库存持有者"))
        {
            instantiate = Instantiate(inventoryHolder);
        }

        if (GUILayout.Button("加载"))
        {
            this.SendCommand<LoadCommand>();
        }

        if (GUILayout.Button("保存"))
        {
            this.SendCommand<SaveCommand>();
        }

        if (GUILayout.Button("给予物品"))
        {
            ItemStack itemStack = new ItemStack(itemIdentification,1);
            this.SendCommand(new GetItemCommand(inventoryKey, itemStack ));
        }
    }

    public IArchitecture GetArchitecture()
    {
        return InGame.Interface;
    }
}

public class GetItemCommand: AbstractCommand
{
    private readonly int inventoryKey;
    private readonly ItemStack itemStack;

    public GetItemCommand(int inventoryKey, ItemStack itemStack)
    {
        this.inventoryKey = inventoryKey;
        this.itemStack = itemStack;
    }

    protected override void OnExecute()
    {
        this.GetModel<InventoryModel>().Add(inventoryKey, itemStack);
    }
}


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