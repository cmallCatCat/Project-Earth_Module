#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;
using Core.Root.Utilities;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    public class ItemDecorator
    {
        [SerializeReference]
        private List<ItemFeature> itemFeatures = new List<ItemFeature>();

        #region Features
        public void AddFeature(ItemFeature itemFeature)
        {
            itemFeatures.Add(itemFeature);
        }

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

        public static bool operator ==(ItemDecorator? a, ItemDecorator? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);
        }

        public static bool operator !=(ItemDecorator? a, ItemDecorator? b)
        {
            return !(a == b);
        }

        protected bool Equals(ItemDecorator other)
        {
            return itemFeatures.SequenceEqual(other.itemFeatures);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ItemDecorator)obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return itemFeatures.GetHashCode();
        }
    }
}