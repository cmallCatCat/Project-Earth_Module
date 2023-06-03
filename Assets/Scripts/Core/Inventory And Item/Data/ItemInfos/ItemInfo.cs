#nullable enable
using System;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;
using Core.Root.Utilities;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos
{
    public abstract class ItemInfo : ScriptableObject
    {
        protected string packageName = "Default";

        [SerializeField]
        protected string itemName = "Unnamed Item";

        [SerializeField]
        [TextArea]
        protected string itemDescription = "UnDefined Item Description";

        [SerializeField]
        protected int maxStack = 1;

        [SerializeField]
        protected Sprite? spriteIcon;

        [SerializeField]
        protected Sprite? spriteInGame;

        protected ItemFeature[] itemFeatures = Array.Empty<ItemFeature>();

        

        public string        PackageName     => packageName;
        public string        ItemName        => itemName;
        public string        ItemDescription => itemDescription;
        public int           MaxStack        => maxStack;
        public Sprite?       SpriteIcon      => spriteIcon;
        public Sprite?       SpriteInGame    => spriteInGame;
        public ItemFeature[] ItemFeatures    => itemFeatures;

        public override string ToString()
        {
            return "PackageName: " + packageName + ", ItemName: " + itemName + ", ItemDescription: " + itemDescription +
                   ", MaxStack: " + maxStack;
        }

        protected virtual void Awake()
        {
            itemFeatures = GetFeatures();
        }

        protected abstract ItemFeature[] GetFeatures();

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
        public static bool operator ==(ItemInfo? a, ItemInfo? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;
            return a.PackageName == b.PackageName && a.ItemName == b.ItemName;
        }

        public static bool operator !=(ItemInfo? a, ItemInfo? b)
        {
            return !(a == b);
        }

        protected bool Equals(ItemInfo other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ItemInfo)obj);
        }

        public override int GetHashCode()
        {
            return PackageName.GetHashCode() ^ ItemName.GetHashCode();
        }
        #endregion
    }
}