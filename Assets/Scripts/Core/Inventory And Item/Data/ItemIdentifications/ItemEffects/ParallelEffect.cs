using System;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class ParallelEffect : Effect
    {
        [SerializeReference]
        [EffectGenerator]
        private Effect[] effects = { };

        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            foreach (Effect effect in effects)
            {
                effect.Work(sender, environment);
            }
        }
    }
}