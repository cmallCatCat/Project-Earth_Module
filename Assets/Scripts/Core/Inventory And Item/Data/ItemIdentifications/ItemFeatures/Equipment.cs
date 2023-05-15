using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures
{
    [Serializable]
    public abstract class Equipment : ItemFeature
    {
        [SerializeReference] public List<ItemEffect> equipOn;
        [SerializeReference] public List<ItemEffect> equipOff;

        public void OnEquip()
        {
            // equipOn.work();
        }
    }
}