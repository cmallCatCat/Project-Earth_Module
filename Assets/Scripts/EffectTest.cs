using System;
using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using Core.QFramework.Framework.Scripts;
using QFramework;
using UI.InGame;
using UnityEngine;

public class EffectTest : MonoBehaviour, IController
{
    private ResLoader resLoader = ResLoader.Allocate();
    public ItemIdentification itemIdentification;
    private ItemIdentification item;
    private IEffectSender effectSender;

    private void Awake()
    {
        ResKit.Init();
        UIKit.CloseAllPanel();
        item = SOHelper.CloneScriptableObject(itemIdentification);
        effectSender=new IEffectSenderTest();
    }

    private void OnGUI()
    {
        
        if (GUILayout.Button("Equip"))
        {
            item.TryToGetFeature<OnHead>()?.equipOn.Work(effectSender,SceneEnvironment.Instance);
        }

        if (GUILayout.Button("UnEquip"))
        {
            item.TryToGetFeature<OnHead>()?.equipOff.Work(effectSender,SceneEnvironment.Instance);
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

public class IEffectSenderTest: IEffectSender
{
    public Transform GetTransform()
    {
        return null;
    }
}