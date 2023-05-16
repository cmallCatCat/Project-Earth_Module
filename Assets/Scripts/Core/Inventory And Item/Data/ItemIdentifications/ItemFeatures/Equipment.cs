using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures
{
    [Serializable]
    public abstract class Equipment : ItemFeature
    {
        [SerializeReference] public List<Effect> equipOn;
        [SerializeReference] public List<Effect> equipOff;

        public void OnEquip(IEffectSender sender, IEnvironment environment)
        {
            equipOn.ForEach(x => x.Work(sender, environment));
        }
        
        public void OffEquip(IEffectSender sender, IEnvironment environment)
        {
            equipOff.ForEach(x => x.Work(sender, environment));
        }

    }
}