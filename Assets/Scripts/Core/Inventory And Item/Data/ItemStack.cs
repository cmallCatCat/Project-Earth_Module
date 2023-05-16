using System;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.QFramework.Framework.Scripts;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    public class ItemStack
    {
        private int number;
        private ItemIdentification itemIdentification;

        public ItemStack(ItemIdentification itemIdentification, int number)
        {
            this.itemIdentification = itemIdentification;
            this.number = number;
        }

        public ItemIdentification ItemIdentification => itemIdentification;
        public int Number => number;

        public int CanAddNumber(ItemIdentification itemIdentification)
        {
            if (itemIdentification != this.itemIdentification) return 0;

            return itemIdentification.MaxStack - number;
        }

        public void Add(ItemIdentification toAddItem, int toAdd)
        {
            if (itemIdentification != toAddItem) throw new Exception("该ItemStack不可应用此添加不同的物品");

            itemIdentification = toAddItem;
            number += toAdd;
        }

        public int CanRemoveNumber(ItemIdentification toRemoveItem)
        {
            if (itemIdentification != toRemoveItem)
                return 0;
            return number;
        }

        public void Remove(int toRemoveNumber)
        {
            number -= toRemoveNumber;
        }
    }
}