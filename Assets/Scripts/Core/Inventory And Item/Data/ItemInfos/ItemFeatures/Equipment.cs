#nullable enable
using System;
using Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;

namespace Core.Inventory_And_Item.Data.ItemInfos.ItemFeatures
{
    public abstract class Equipment : ItemFeature
    {
        
        public Action<IEffectSender>? equipOn;

        public Action<IEffectSender>? equipOff;

        protected Equipment(Action<IEffectSender>? equipOn =null, Action<IEffectSender>? equipOff=null)
        {
            this.equipOn = equipOn;
            this.equipOff = equipOff;
        }

        public void OnEquip(IEffectSender sender)
        {
            equipOn?.Invoke(sender);
        }

        public void OffEquip(IEffectSender sender)
        {
            equipOff?.Invoke(sender);
        }
    }
}