using Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using Core.Root.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tests
{
    public class PositionTest : MonoBehaviour
    {
        public Transform transform1;
        public Transform transform2;

        [Space]
        [TitleGroup("Position")]
        public Vector2 position;
        public PositionUtility.CoordinateReference referencePosition;

        [HideIf("referencePosition", PositionUtility.CoordinateReference.World)]
        public bool considerHorizontalAxisPosition;

        [HideIf("referencePosition", PositionUtility.CoordinateReference.World)]
        public bool considerRotationPosition;

        [Space]
        [TitleGroup("Rotation")]
        public Quaternion rotation = Quaternion.identity;
        public RotationUtility.CoordinateReference referenceRotation;

        [HideIf("referenceRotation", RotationUtility.CoordinateReference.World)]
        public bool considerHorizontalAxisRotation;


        private void Update()
        {
            transform2.position = referencePosition.GetPosition(position,
                transform1, considerHorizontalAxisPosition, considerRotationPosition);
            transform2.rotation = referenceRotation.GetRotation(rotation,
                transform1, considerHorizontalAxisRotation);
        }
    }
}