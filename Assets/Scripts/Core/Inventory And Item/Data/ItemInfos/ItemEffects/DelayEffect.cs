﻿using System;
using Core.Root.Base;
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

            public static Args Create(IEffectSender effectSender)
            {
                return new Args
                { sender = effectSender };
            }
        }

        public override void Work(IEffectSender sender)
        {
            IEnvironment.Instance.Delay(Delay, Args.Create(sender), delay);
        }

        private void Delay(Args args)
        {
            effect.Work(args.sender);
        }
    }
}