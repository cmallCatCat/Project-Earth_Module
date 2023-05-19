using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class DelayEffect : Effect
    {
        [SerializeField]
        private float delay;

        [SerializeField]
        [EffectGenerator]
        private Effect effect;

        public struct Args
        {
            public IEffectSender sender;
            public IEnvironment environment;

            public static Args Create(IEffectSender effectSender, IEnvironment environment1)
            {
                return new Args { sender = effectSender, environment = environment1 };
            }
        }

        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            environment.Delay(Delay, Args.Create(sender, environment), delay);
        }

        private void Delay(Args args)
        {
            effect.Work(args.sender, args.environment);
        }
    }
}