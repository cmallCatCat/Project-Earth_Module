#nullable enable
using System;
using System.Linq;
using Core.Root.Utilities;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos
{
    [CreateAssetMenu(menuName = "Create ItemInfo", fileName = "ItemInfo", order = 0)]
    public class ItemInfo : ScriptableObject
    {
        private string packageName = "Default";

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

        private static readonly string PLUGINS_PATH = AppDomain.CurrentDomain.BaseDirectory + "/BepInEx/plugins/";

        public string PackageName => packageName;
        public string ItemName    => itemName;

        public string ItemDescription => itemDescription;

        public int MaxStack => maxStack;

        public Sprite? SpriteIcon => spriteIcon;

        public Sprite? SpriteInGame => spriteInGame;

        public ItemFeature[] ItemFeatures => itemFeatures;

        /// <summary>
        /// 请使用此方法创建模组物品
        /// </summary>
        /// <param name="packageName">模组名，用于区分同名的不同物品</param>
        /// <param name="itemName">物品名</param>
        /// <param name="itemDescription">物品描述</param>
        /// <param name="maxStack">最大堆叠数</param>
        /// <param name="spriteIconPath">在物品栏时显示的图片的路径，为/BepInEx/plugins/之后的相对路径</param>
        /// <param name="spriteInGamePath">在游戏中显示的图片的路径，为/BepInEx/plugins/之后的相对路径</param>
        /// <param name="itemFeatures">物品特性，如OnHead代表头部装备</param>
        /// <returns></returns>
        public static ItemInfo Create(string packageName,    string itemName, string itemDescription, int maxStack,
            string                           spriteIconPath, string spriteInGamePath,
            ItemFeature[]                    itemFeatures)
        {
            ItemInfo newItemInfo = CreateInstance<ItemInfo>();
            newItemInfo.packageName     = packageName;
            newItemInfo.itemName        = itemName;
            newItemInfo.itemDescription = itemDescription;
            newItemInfo.maxStack        = maxStack;
            newItemInfo.spriteIcon      = SpriteLoader.LoadSprite(PLUGINS_PATH + spriteIconPath);
            newItemInfo.spriteInGame    = SpriteLoader.LoadSprite(PLUGINS_PATH + spriteInGamePath);
            newItemInfo.itemFeatures    = itemFeatures;
            return newItemInfo;
        }

        public override string ToString()
        {
            return "PackageName: " + packageName + ", ItemName: " + itemName + ", ItemDescription: " + itemDescription +
                   ", MaxStack: " + maxStack;
        }


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