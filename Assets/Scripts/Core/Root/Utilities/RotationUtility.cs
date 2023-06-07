using System;
using UnityEngine;

namespace Core.Root.Utilities
{
    public static class RotationUtility
    {
        public enum CoordinateReference
        {
            World,
            Local
        }

        public static Quaternion GetRotation(this CoordinateReference reference, Quaternion inputQuaternion, Transform transform, bool applyHorizontalFlip)
        {
            switch (reference)
            {
                case CoordinateReference.World:
                    return inputQuaternion;
                case CoordinateReference.Local:
                    Quaternion localRotation = inputQuaternion;

                    if (applyHorizontalFlip && transform.localScale.x < 0)
                    {
                        localRotation *= Quaternion.Euler(0, 180, 0);
                    }

                    localRotation *= transform.rotation;

                    return localRotation;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reference), reference, null);
            }
        }
    }

}