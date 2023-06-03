using System;
using Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;

namespace Core.Inventory_And_Item.Data.ItemInfos
{
    public class SampleModItem : ModItem
    {
        protected override string        GetPackageName()      => "Sample PackageName";
        protected override string        GetItemName()         => "Sample ItemName";
        protected override string        GetDescription()      => "Sample Description";
        protected override int           GetMaxStack()         => 1;
        protected override string        GetSpriteIconPath()   => "";
        protected override string        GetSpriteInGamePath() => "";
        protected override ItemFeature[] GetItemFeatures()     => Array.Empty<ItemFeature>();
        protected override ItemFeature[] GetFeatures()         => Array.Empty<ItemFeature>();
    }
}