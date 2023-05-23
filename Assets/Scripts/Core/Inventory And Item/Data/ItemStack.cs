using System;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using Core.QFramework.Framework.Scripts;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    public class ItemStack: IEffectSender
    {
        private ItemIdentification itemIdentification;

        private ItemDecorator itemDecorator;
        
        private int number;
        
        // private Transform transform;

        public ItemStack(ItemIdentification itemIdentification, ItemDecorator itemDecorator, int number)
        {
            this.itemIdentification = itemIdentification;
            this.itemDecorator = itemDecorator;
            this.number = number;
        }

        public ItemIdentification ItemIdentification => itemIdentification;
        public int Number => number;
        public ItemDecorator ItemDecorator => itemDecorator;

        public int CanAddNumber(ItemIdentification itemIdentification, ItemDecorator itemDecorator)
        {
            if (itemIdentification != this.itemIdentification || itemDecorator != this.itemDecorator) return 0;

            return itemIdentification.MaxStack - number;
        }

        public void Add(ItemStack itemStack)
        {
            if (itemIdentification != itemStack.itemIdentification || itemDecorator != itemStack.itemDecorator)
                throw new Exception("该ItemStack不可应用此添加不同的物品");
            number += itemStack.number;
        }

        public int CanRemoveNumber(ItemIdentification itemIdentification, ItemDecorator itemDecorator)
        {
            if (this.itemIdentification != itemIdentification || this.itemDecorator != itemDecorator)
                return 0;
            return number;
        }

        public void Remove(int toRemoveNumber)
        {
            number -= toRemoveNumber;
        }

        public Transform GetTransform()
        {
            throw new NotImplementedException();
        }
    }
}