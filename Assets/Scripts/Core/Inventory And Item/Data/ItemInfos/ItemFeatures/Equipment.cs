using System;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using UnityEngine;
#if UNITY_EDITOR
using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures
{
    [Serializable]
    public abstract class Equipment : ItemFeature
    {
        [SerializeReference]
#if UNITY_EDITOR
        [EffectGenerator]
#endif
        public Effect equipOn;

        [SerializeReference]
#if UNITY_EDITOR
        [EffectGenerator]
#endif
        public Effect equipOff;

        public void OnEquip(IEffectSender sender)
        {
            if (equipOn)
                equipOn.Work(sender);
        }

        public void OffEquip(IEffectSender sender)
        {
            if (equipOff)
                equipOff.Work(sender);
        }
    }
    
    [Serializable]
    [CreateAssetMenu(menuName = "Create OnHead", fileName = "OnHead", order = 0)]
    public class OnHead : Equipment { }

    [Serializable]
    [CreateAssetMenu(menuName = "Create OnBody", fileName = "OnBody", order = 0)]
    public class OnBody : Equipment { }

    [Serializable]
    [CreateAssetMenu(menuName = "Create OnLegs", fileName = "OnLegs", order = 0)]
    public class OnLegs : Equipment { }
    
    [Serializable]
    [CreateAssetMenu(menuName = "Create Accessory", fileName = "Accessory", order = 0)]
    public class Accessory : Equipment { }
}