#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures;
using Core.QFramework.Framework.Scripts;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemIdentifications
{
    [CreateAssetMenu(menuName = "Create ItemIdentification", fileName = "ItemIdentification", order = 0)]
    public class ItemIdentification : ScriptableObject
    {
        [SerializeField]
        private string itemName = "Unnamed Item";

        [SerializeField]
        [TextArea]
        private string itemDescription = "UnDefined Item Description";

        [SerializeField]
        private int maxStack = 1;

        [SerializeField]
        private Sprite? spriteIcon;

        [SerializeField]
        private Sprite? spriteInGame;

        [SerializeReference]
        private ItemFeature[] itemFeatures = Array.Empty<ItemFeature>();

        public string ItemName => itemName;

        public string ItemDescription => itemDescription;

        public int MaxStack => maxStack;

        public Sprite? SpriteIcon => spriteIcon;

        public Sprite? SpriteInGame => spriteInGame;

        public ItemFeature[] ItemFeatures => itemFeatures;


        #region Features

        public ItemFeature? TryToGetFeature(Type type)
        {
            return GetFeatures(type.FullName ?? throw new InvalidOperationException()).FirstOrDefault();
        }


        public ItemFeature? TryToGetFeature(string typeName)
        {
            return GetFeatures(typeName).FirstOrDefault();
        }

        public T? TryToGetFeature<T>() where T : ItemFeature
        {
            return GetFeatures(typeof(T).FullName).FirstOrDefault() as T;
        }

        public bool HasFeature(Type type)
        {
            return GetFeatures(type.FullName ?? throw new InvalidOperationException()).Any();
        }

        public bool HasFeature(string typeName)
        {
            return GetFeatures(typeName).Any();
        }

        public bool HasFeature<T>() where T : ItemFeature
        {
            return HasFeature(typeof(T));
        }

        public ItemFeature[] GetFeatures(Type type)
        {
            return GetFeatures(type.FullName ?? throw new InvalidOperationException());
        }

        public ItemFeature[] GetFeatures(string typeName)
        {
            return itemFeatures.Where(itemFeature => itemFeature.GetType().FullName == typeName ||
                                                     itemFeature.GetType().GetAllBaseTypes()
                                                         .Any(type => type.FullName == typeName))
                .ToArray();
        }

        public T[] GetFeatures<T>() where T : ItemFeature
        {
            return GetFeatures(typeof(T).FullName) as T[] ?? throw new InvalidOperationException();
        }

        #endregion

        #region EqualsAndHashCode

        public static bool operator ==(ItemIdentification? a, ItemIdentification? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.ItemName == b.ItemName;
        }

        public static bool operator !=(ItemIdentification? a, ItemIdentification? b)
        {
            return !(a == b);
        }

        protected bool Equals(ItemIdentification other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
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