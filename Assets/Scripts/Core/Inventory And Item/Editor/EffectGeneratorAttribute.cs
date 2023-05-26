#if UNITY_EDITOR
using System;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Editor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EffectGeneratorAttribute : PropertyAttribute
    {
    }
}

#endif