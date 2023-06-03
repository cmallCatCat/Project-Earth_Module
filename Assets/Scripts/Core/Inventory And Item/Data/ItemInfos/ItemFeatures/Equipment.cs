using System;
using Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures
{
    [Serializable]
    public abstract class Equipment : ItemFeature
    {
        [SerializeReference]
        [EffectGenerator]
        public Effect equipOn;

        [SerializeReference]
        [EffectGenerator]
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
    [CreateAssetMenu(menuName = "CreateOrAdd OnHead", fileName = "OnHead", order = 0)]
    public class OnHead : Equipment { }

    [Serializable]
    [CreateAssetMenu(menuName = "CreateOrAdd OnBody", fileName = "OnBody", order = 0)]
    public class OnBody : Equipment { }

    [Serializable]
    [CreateAssetMenu(menuName = "CreateOrAdd OnLegs", fileName = "OnLegs", order = 0)]
    public class OnLegs : Equipment { }
    
    [Serializable]
    [CreateAssetMenu(menuName = "CreateOrAdd Accessory", fileName = "Accessory", order = 0)]
    public class Accessory : Equipment { }
}