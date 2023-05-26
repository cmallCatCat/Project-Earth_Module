using System;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects
{
    public static class Rotation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="quaternion">注意不要为{0，0，0，0}</param>
        /// <param name="transform"></param>
        /// <param name="horizontalAxis"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Quaternion GetRotation(this Reference reference, Quaternion quaternion, Transform transform,
            bool horizontalAxis)
        {
            switch (reference)
            {
                case Reference.World:
                    return quaternion;
                case Reference.Local:
                    Quaternion delta = quaternion;

                    if (horizontalAxis && transform.localScale.x < 0)
                    {
                        delta *= Quaternion.Euler(0, 180, 0);
                    }

                    delta *= transform.rotation;

                    return delta;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reference), reference, null);
            }
        }
    }
}