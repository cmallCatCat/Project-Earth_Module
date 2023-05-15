using System;
using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Command;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.Inventory_And_Item.Models;
using Core.Save_And_Load.Command;
using QFramework;
using QFramework.UI;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public ItemIdentification itemIdentification;
    public InventoryModel inventoryModel;
    private ResLoader resLoader = ResLoader.Allocate();

    private void Awake()
    {
        ResKit.Init();
        UIKit.OpenPanel<ItemBar>();
        inventoryModel = this.GetModel<InventoryModel>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("创建"))
        {
            GameObject loadSync = resLoader.LoadSync<GameObject>(QAssetBundle.Player_prefab.PLAYER);
            Instantiate(loadSync);
        }

        // if (GUILayout.Button("加载"))
        // {
        //     this.SendCommand<LoadCommand>();
        // }
        //
        // if (GUILayout.Button("保存"))
        // {
        //     this.SendCommand<SaveCommand>();
        // }
    }

    public IArchitecture GetArchitecture()
    {
        return InGame.Interface;
    }
}