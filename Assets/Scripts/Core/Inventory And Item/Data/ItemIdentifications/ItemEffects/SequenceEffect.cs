using System;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class SequenceEffect : Effect
    {
        [SerializeReference]
        [EffectGenerator]
        private Effect[] effects = { };


        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            CounterManager.Counter counter = CounterManager.Get(sender);
            if (counter >= effects.Length)
            {
                counter.value = 0;
            }
            effects[counter].Work(sender, environment);

            counter.value++;
        }
    }
}