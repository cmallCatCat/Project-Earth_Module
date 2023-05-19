using System;
using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using Core.QFramework.Framework.Scripts;
using QFramework;
using UI.InGame;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public ItemIdentification itemIdentification;
    private ResLoader resLoader = ResLoader.Allocate();
    private ItemIdentification item;

    private void Awake()
    {
        ResKit.Init();
        UIKit.CloseAllPanel();
        item = SOHelper.CloneScriptableObject(itemIdentification);
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
                new InventoryPanelData(instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory,12));
            instantiate.name = "Player";
        }

        if (GUILayout.Button("Equip"))
        {
            item.TryToGetFeature<OnHead>().equipOn.Work(null,SceneEnvironment.Instance);
        }

        if (GUILayout.Button("UnEquip"))
        {
            item.TryToGetFeature<OnHead>().equipOff.Work(null,SceneEnvironment.Instance);
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