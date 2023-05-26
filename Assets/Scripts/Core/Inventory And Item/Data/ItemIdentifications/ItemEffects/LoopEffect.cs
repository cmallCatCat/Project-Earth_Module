using System;

#if UNITY_EDITOR

using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class LoopEffect : Effect
    {
        [SerializeField]
        private int times = 1;

        [SerializeField]
#if UNITY_EDITOR
        [EffectGenerator]
#endif
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