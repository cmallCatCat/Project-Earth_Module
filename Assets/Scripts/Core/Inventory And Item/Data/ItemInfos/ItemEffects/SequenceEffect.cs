using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class SequenceEffect : Effect
    {
        [SerializeReference]
        [EffectGenerator]
        private Effect[] effects =
        { };


        public override void Work(IEffectSender sender)
        {
            CounterManager.Counter counter               = CounterManager.Get(sender);
            if (counter >= effects.Length) counter.value = 0;
            effects[counter].Work(sender);

            counter.value++;
        }
    }
}