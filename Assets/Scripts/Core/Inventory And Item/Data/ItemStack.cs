using System;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data
{
    public class ItemStack : IEffectSender
    {
        private ItemInfo itemInfo;

        private ItemDecorator itemDecorator;

        private int number;

        private Transform transform;

        public ItemInfo ItemInfo => itemInfo;

        public int Number => number;

        public ItemDecorator ItemDecorator => itemDecorator;

        public Transform Transform => transform;

        public ItemStack(ItemInfo itemInfo, ItemDecorator itemDecorator, int number, Transform transform)
        {
            this.itemInfo = itemInfo;
            this.itemDecorator = itemDecorator;
            this.number = number;
            this.transform = transform;
        }

        public void SetTransform(Transform transform)
        {
            this.transform = transform;
        }

        public int CanAddNumber(ItemInfo itemInfo, ItemDecorator itemDecorator)
        {
            if (itemInfo != this.itemInfo || itemDecorator != this.itemDecorator) return 0;

            return itemInfo.MaxStack - number;
        }

        public void Add(ItemStack itemStack)
        {
            if (itemInfo != itemStack.itemInfo || itemDecorator != itemStack.itemDecorator)
                throw new Exception("该ItemStack不可应用此添加不同的物品");
            number += itemStack.number;
        }

        public int CanRemoveNumber(ItemInfo itemInfo, ItemDecorator itemDecorator)
        {
            if (this.itemInfo != itemInfo || this.itemDecorator != itemDecorator)
                return 0;
            return number;
        }

        public void Remove(int toRemoveNumber)
        {
            number -= toRemoveNumber;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}