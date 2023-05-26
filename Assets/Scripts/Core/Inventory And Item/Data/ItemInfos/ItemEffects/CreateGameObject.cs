using System;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects
{
    [Serializable]
    public class CreateGameObject : Effect
    {
        [AssetsOnly]
        public GameObject toCreate;

        public bool useObjectPool;

        [BoxGroup("Position")]
        [EnumToggleButtons, HideLabel]
        public Reference referencePosition;

        [BoxGroup("Position")]
        public Vector2 position;

        [BoxGroup("Position")]
        [HideIf("referencePosition", Reference.World)]
        public bool considerHorizontalAxisPosition;

        [PropertySpace(0, 10)]
        [BoxGroup("Position")]
        [HideIf("referencePosition", Reference.World)]
        public bool considerRotationPosition;

        [BoxGroup("Rotation")]
        [EnumToggleButtons, HideLabel]
        public Reference referenceRotation;

        [BoxGroup("Rotation")]
        public Quaternion rotation = Quaternion.identity;

        [PropertySpace(0, 10)]
        [BoxGroup("Rotation")]
        [HideIf("referenceRotation", Reference.World)]
        public bool considerHorizontalAxisRotation;

        public override void Work(IEffectSender sender, IEnvironment environment)
        {
            environment.Instantiate(toCreate,useObjectPool,
                referencePosition.GetPosition(position,
                    sender.GetTransform(), considerHorizontalAxisPosition, considerRotationPosition),
                referenceRotation.GetRotation(rotation,
                    sender.GetTransform(), considerHorizontalAxisRotation));
        }
    }
}