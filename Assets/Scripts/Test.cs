using System;
using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using QFramework;
using UI.InGame;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public ItemIdentification itemIdentification;
    private ResLoader resLoader = ResLoader.Allocate();

    private void Awake()
    {
        ResKit.Init();
        UIKit.OpenPanel<InventoryPanel>();
        UIKit.ClosePanel<InventoryPanel>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("创建"))
        {
            if (SceneEnvironment.Instance.Player != null)
            {
                throw new Exception("Player is already created");
            }

            GameObject loadSync = resLoader.LoadSync<GameObject>(QAssetBundle.Player_prefab.PLAYER);
            GameObject instantiate = Instantiate(loadSync);
            UIKit.OpenPanel<InventoryPanel>(
                new InventoryPanelData(instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory));
            instantiate.name = "Player";
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