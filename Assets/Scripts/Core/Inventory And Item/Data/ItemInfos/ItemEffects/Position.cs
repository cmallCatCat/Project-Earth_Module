using System;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    public static class Position
    {
        public static Vector2 GetPosition(this Reference reference, Vector2 vector2, Transform transform,
            bool horizontalAxis, bool rotation)
        {
            switch (reference)
            {
                case Reference.World:
                    return vector2;
                case Reference.Local:
                    Vector2 delta = vector2;
                    if (rotation)
                    {
                        delta = transform.rotation * vector2;
                    }

                    if (horizontalAxis && transform.localScale.x < 0)
                    {
                        delta.x = -delta.x;
                    }

                    return (Vector2)transform.position + delta;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reference), reference, null);
            }
        }
    }
}