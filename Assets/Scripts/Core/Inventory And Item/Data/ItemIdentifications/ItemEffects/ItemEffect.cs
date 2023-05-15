using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public abstract class ItemEffect : ScriptableObject
    {
        public abstract void Work(ItemStack stack);
    }
}