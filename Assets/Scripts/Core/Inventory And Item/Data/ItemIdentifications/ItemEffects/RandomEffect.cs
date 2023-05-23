using System;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class RandomEffect : Effect
    {
        [Serializable]
        public class EffectsAndPossibilities
        {
            [EffectGenerator]
            public Effect effect;

            [Min(1)]public int probability = 1;
        }

        [SerializeField]
        private EffectsAndPossibilities[] effectsAndPossibilities = { };

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