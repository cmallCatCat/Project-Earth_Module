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

        private int index;

        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            if (index < effects.Length)
            {
                effects[index].Work(sender, environment);
            }
            else
            {
                index = 0;
                effects[index].Work(sender, environment);
            }

            index++;
        }
    }
}