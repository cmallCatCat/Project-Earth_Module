using System;
using UnityEditor;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public abstract class Effect : ScriptableObject
    {
        [NonSerialized]public bool enable = true;
        public abstract void Work(IEffectSender sender, IEnvironment environment);
    }
}