using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class RandomEffect : Effect
    {
        [Serializable]
        public class EffectsAndPossibilities
        {
#if UNITY_EDITOR
            [EffectGenerator]
#endif
            public Effect effect;

            [Min(1)]
            public int probability = 1;
        }

        [SerializeField]
        private EffectsAndPossibilities[] effectsAndPossibilities =
        { };

        public override void Work(IEffectSender sender)
        {
            int sum = effectsAndPossibilities.Sum(x => x.probability);

            int random = UnityEngine.Random.Range(0, sum);

            foreach (EffectsAndPossibilities possibilities in effectsAndPossibilities)
            {
                if (random < possibilities.probability)
                {
                    if (possibilities.effect != null) possibilities.effect.Work(sender);

                    break;
                }

                random -= possibilities.probability;
            }
        }
    }
}