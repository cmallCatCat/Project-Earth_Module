using System;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class RandomEffect : Effect
    {
        [Serializable]
        public struct EffectsAndPossibilities
        {
            [EffectGenerator]
            public Effect effect;

            public int probability;
        }

        [SerializeReference]
        private EffectsAndPossibilities[] effectsAndPossibilities;

        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            int sum = effectsAndPossibilities.Sum(x => x.probability);

            int random = UnityEngine.Random.Range(0, sum);

            foreach (EffectsAndPossibilities possibilities in effectsAndPossibilities)
            {
                if (random < possibilities.probability)
                {
                    if (possibilities.effect != null)
                    {
                        possibilities.effect.Work(sender, environment);
                    }
                    break;
                }
                random -= possibilities.probability;
            }
        }
    }
}