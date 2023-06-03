#nullable enable
using Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures;
using UnityEngine;

namespace Core.Inventory_And_Item.Data.ItemInfos
{
    [CreateAssetMenu(menuName = "Create SampleItem", fileName = "Items/SampleItem", order = 0)]
    public class SampleItem : ItemInfo
    {
        protected override ItemFeature[] GetFeatures()
        {
            OnHead onHead = new OnHead(OnEquip, OnUnEquip);

            return new ItemFeature[]
            { onHead };
        }

        private void OnEquip(IEffectSender obj) { }

        private void OnUnEquip(IEffectSender obj) { }
    }
}