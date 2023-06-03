using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
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
        public Reference referencePosition;

        [HideIf("referencePosition", Reference.World)]
        public bool considerHorizontalAxisPosition;

        [HideIf("referencePosition", Reference.World)]
        public bool considerRotationPosition;

        [Space]
        [TitleGroup("Rotation")]
        public Quaternion rotation = Quaternion.identity;
        public Reference referenceRotation;

        [HideIf("referenceRotation", Reference.World)]
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