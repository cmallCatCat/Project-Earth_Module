﻿using System;
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
}