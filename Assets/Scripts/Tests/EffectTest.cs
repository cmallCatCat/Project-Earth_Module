using Core;
using Core.Architectures;
using Core.QFramework.Framework.Scripts;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;
using QFramework;
using UnityEngine;

public class EffectTest : MonoBehaviour, IController
{
    private ResLoader resLoader = ResLoader.Allocate();
    
    public ItemInfo itemInfo;
    private ItemInfo item;
    private IEffectSender effectSender;

    private void Awake()
    {
        ResKit.Init();
        UIKit.CloseAllPanel();
        item = SOHelper.CloneScriptableObject(itemInfo);
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