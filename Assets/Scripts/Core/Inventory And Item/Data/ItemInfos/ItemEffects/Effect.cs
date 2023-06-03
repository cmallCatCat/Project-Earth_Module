using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public abstract class Effect : ScriptableObject
    {
        public abstract void Work(IEffectSender sender);
    }
}