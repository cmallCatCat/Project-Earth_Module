using System;
using UnityEngine;
#if UNITY_EDITOR
using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class SequenceEffect : Effect
    {
        [SerializeReference]
#if UNITY_EDITOR
        [EffectGenerator]
#endif
        private Effect[] effects = { };


        public override void Work(IEffectSender sender)
        {
            CounterManager.Counter counter = CounterManager.Get(sender);
            if (counter >= effects.Length)
            {
                counter.value = 0;
            }
            effects[counter].Work(sender);

            counter.value++;
        }
    }
}