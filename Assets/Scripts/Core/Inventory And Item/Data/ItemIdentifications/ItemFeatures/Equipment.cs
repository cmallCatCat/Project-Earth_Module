using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures
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

        public void OnEquip(IEffectSender sender, IEnvironment environment)
        {
            equipOn.Work(sender, environment);
        }

        public void OffEquip(IEffectSender sender, IEnvironment environment)
        {
            equipOff.Work(sender, environment);
        }
    }
}