using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class LoopEffect : Effect
    {
        [SerializeField]
        private int times = 1;

        [SerializeField]
        [EffectGenerator]
        private Effect effect;

        public override void Work(IEffectSender sender)
        {
            for (int i = 0; i < times; i++) effect.Work(sender);
        }
    }
}