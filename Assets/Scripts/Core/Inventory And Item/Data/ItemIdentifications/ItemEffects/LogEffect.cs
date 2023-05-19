using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    [Serializable]
    public class LogEffect : Effect
    {
        public string message;
        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            Debug.Log(message);
        }
    }
}