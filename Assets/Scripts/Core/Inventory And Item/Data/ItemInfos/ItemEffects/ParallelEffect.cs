using System;
using UnityEngine;
#if UNITY_EDITOR
using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class ParallelEffect : Effect
    {
        [SerializeReference]
#if UNITY_EDITOR
        [EffectGenerator]
#endif
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