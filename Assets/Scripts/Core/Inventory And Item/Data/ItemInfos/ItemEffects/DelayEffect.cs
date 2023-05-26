using System;
using UnityEngine;
#if UNITY_EDITOR
using InventoryAndItem.Core.Inventory_And_Item.Editor;
#endif

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class DelayEffect : Effect
    {
        [SerializeField]
        [Min(0.01f)]
        private float delay = 1;

        [SerializeField]
#if UNITY_EDITOR
        [EffectGenerator]
#endif
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