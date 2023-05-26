using System;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;

#if UNITY_EDITOR
using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures
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

        public void OnEquip(IEffectSender sender, IEnvironment environment)
        {
            if (equipOn)
                equipOn.Work(sender, environment);
        }

        public void OffEquip(IEffectSender sender, IEnvironment environment)
        {
            if (equipOff)
                equipOff.Work(sender, environment);
        }
    }
}