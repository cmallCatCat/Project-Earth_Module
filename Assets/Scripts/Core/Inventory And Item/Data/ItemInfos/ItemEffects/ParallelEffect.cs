using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class ParallelEffect : Effect
    {
        [SerializeReference]
        [EffectGenerator]
        private Effect[] effects =
        { };

        public override void Work(IEffectSender sender)
        {
            foreach (Effect effect in effects) effect.Work(sender);
        }
    }
}