using System;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    public static class PositionUtility
    {
        public enum CoordinateReference
        {
            World,
            Local
        }

        public static Vector2 GetPosition(this CoordinateReference reference, Vector2 inputVector, 
            Transform transform, bool applyHorizontalFlip, bool applyRotation)
        {
            switch (reference)
            {
                case CoordinateReference.World:
                    return inputVector;
                case CoordinateReference.Local:
                    Vector2 localOffset = inputVector;

                    if (applyRotation)
                    {
                        localOffset = transform.rotation * inputVector;
                    }

                    if (applyHorizontalFlip && transform.localScale.x < 0)
                    {
                        localOffset.x = -localOffset.x;
                    }

                    return (Vector2)transform.position + localOffset;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reference), reference, null);
            }
        }
    }

}