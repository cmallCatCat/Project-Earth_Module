using System;
using UnityEngine;
#if UNITY_EDITOR
using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
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

        public override void Work(IEffectSender sender)
        {
            for (int i = 0; i < times; i++)
            {
                effect.Work(sender);
            }
        }
    }
}