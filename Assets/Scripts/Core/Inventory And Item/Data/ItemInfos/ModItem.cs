using System;
using Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;
using Core.Root.Utilities;

namespace Core.Inventory_And_Item.Data.ItemInfos
{
    public abstract class ModItem : ItemInfo
    {
        private static readonly string PLUGINS_PATH = AppDomain.CurrentDomain.BaseDirectory + "/BepInEx/plugins/";

        protected override void Awake()
        {
            base.Awake();
            ItemIdentification = new ItemIdentification(GetPackageName(), GetItemName());
            itemDescription    = GetDescription();
            maxStack           = GetMaxStack();
            spriteIcon         = SpriteLoader.LoadSprite(PLUGINS_PATH + GetSpriteIconPath());
            spriteInGame       = SpriteLoader.LoadSprite(PLUGINS_PATH + GetSpriteInGamePath());
            itemFeatures       = GetItemFeatures();
        }

        ///模组名，用于区分同名的不同物品
        protected abstract string GetPackageName();

        ///物品名
        protected abstract string GetItemName();

        ///物品描述
        protected abstract string GetDescription();

        ///最大堆叠数
        protected abstract int GetMaxStack();

        ///在物品栏时显示的图片的路径，为/BepInEx/plugins/之后的相对路径
        protected abstract string GetSpriteIconPath();

        ///在游戏中显示的图片的路径，为/BepInEx/plugins/之后的相对路径
        protected abstract string GetSpriteInGamePath();

        ///物品特性，如OnHead代表头部装备
        protected abstract ItemFeature[] GetItemFeatures();
    }
}