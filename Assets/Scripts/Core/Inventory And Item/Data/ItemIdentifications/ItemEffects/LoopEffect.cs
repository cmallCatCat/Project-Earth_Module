using System;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class LoopEffect : Effect
    {
        [SerializeField]
        private int times = 1;

        [SerializeField]
        [EffectGenerator]
        private Effect effect;

        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            for (int i = 0; i < times; i++)
            {
                effect.Work(sender, environment);
            }
        }
    }
}