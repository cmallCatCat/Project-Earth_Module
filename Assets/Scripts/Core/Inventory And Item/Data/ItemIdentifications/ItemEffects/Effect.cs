using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public abstract class Effect : ScriptableObject
    {
        public abstract void Work(IEffectSender sender, IEnvironment environment);
    }
}