using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using Core.QFramework.Framework.Scripts;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications
{
    [CreateAssetMenu(menuName = "Create ItemIdentification", fileName = "ItemIdentification", order = 0)]
    public class ItemIdentification : ScriptableObject
    {
        [SerializeField] private string itemName = "Unnamed Item";

        [SerializeField] private string itemDescription = "UnDefined Item Description";

        [SerializeField] private int maxStack = 1;

        [SerializeField] private Sprite spriteIcon;

        [SerializeField] private Sprite spriteInGame;

        [SerializeReference] private List<ItemFeature> itemFeatures = new List<ItemFeature>();

        public string ItemName => itemName;

        public string ItemDescription => itemDescription;

        public int MaxStack => maxStack;

        public Sprite SpriteIcon => spriteIcon;

        public Sprite SpriteInGame => spriteInGame;

        public List<ItemFeature> ItemFeatures => itemFeatures;

        [CanBeNull]
        public ItemFeature TryToGetFeature(Type type)
        {
            return itemFeatures.FirstOrDefault(itemFeature =>
                itemFeature.GetType() == type || itemFeature.GetType().IsSubclassOf(type));
        }

        [CanBeNull]
        public ItemFeature TryToGetFeature(string typeName)
        {
            return itemFeatures.FirstOrDefault(itemFeature =>
                itemFeature.GetType().FullName == typeName ||
                itemFeature.GetType().GetAllBaseTypes().Any(type => type.FullName == typeName));
        }

        public bool HasFeature(Type type)
        {
            return TryToGetFeature(type) != null;
        }
        
        public bool HasFeature(string typeName)
        {
            return TryToGetFeature(typeName) != null;
        }       
        
        public ItemFeature[] GetFeatures(Type type)
        {
            return itemFeatures.Where(itemFeature => itemFeature.GetType() == type || itemFeature.GetType().IsSubclassOf(type))
                .ToArray();
        }
        
        public ItemFeature[] GetFeatures(string typeName)
        {
            return itemFeatures.Where(itemFeature => itemFeature.GetType().FullName == typeName ||
                itemFeature.GetType().GetAllBaseTypes().Any(type => type.FullName == typeName))
                .ToArray();
        }

        #region EqualsAndHashCode

        public static bool operator ==(ItemIdentification a, ItemIdentification b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.ItemName == b.ItemName;
        }

        public static bool operator !=(ItemIdentification a, ItemIdentification b)
        {
            return !(a == b);
        }

        protected bool Equals(ItemIdentification other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ItemIdentification)obj);
        }

        public override int GetHashCode()
        {
            return ItemName.GetHashCode();
        }

        #endregion
    }
}