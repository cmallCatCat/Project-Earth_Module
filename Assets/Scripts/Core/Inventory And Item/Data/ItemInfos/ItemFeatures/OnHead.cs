#nullable enable
using System;
using Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures
{
    public class OnHead : Equipment
    {
        public OnHead(Action<IEffectSender>? equipOn = null, Action<IEffectSender>? equipOff = null) : base(equipOn, equipOff) { }
    }
}