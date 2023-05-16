﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.Inventory_And_Item.Filters;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    public delegate void OnInventoryChanged();

    [Serializable]
    public class Inventory
    {
        [SerializeField]
        private int capacity;

        [SerializeField]
        private int activeIndex;

        [SerializeField]
        private ItemSlot[] itemSlots;


        public event OnInventoryChanged? onItemChanged;

        public Inventory(int capacity)
        {
            this.capacity = capacity;
            itemSlots = new ItemSlot[capacity];
            for (int i = 0; i < capacity; i++) itemSlots[i] = new ItemSlot();

            activeIndex = 0;
        }

        public ItemSlot ActiveItemSlot => itemSlots[activeIndex];
        public int Capacity => capacity;
        public int ActiveIndex => activeIndex;

        public void Activate(int index)
        {
            if (index < 0 || index >= capacity) throw new Exception("指定的活动栏位越界");

            activeIndex = index;
        }

        #region BaseCommands

        private void BaseAdd(ItemSlot itemSlot, ItemIdentification toAddItem, int toAddNumber)
        {
            itemSlot.Add(toAddItem, toAddNumber);
            onItemChanged?.Invoke();
        }

        private void BaseRemove(ItemSlot itemSlot, int toRemoveNumber)
        {
            itemSlot.Remove(toRemoveNumber);
            onItemChanged?.Invoke();
        }

        private void BaseSet(ItemSlot itemSlot, ItemStack? newStack)
        {
            itemSlot.Set(newStack);
            onItemChanged?.Invoke();
        }

        #endregion


        #region 增

        internal void Add(ItemStack stack)
        {
            Add(stack.ItemIdentification, stack.Number);
        }

        private void Add(ItemIdentification toAddItem, int toAddNumber)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                int canAddNumber = itemSlot.CanAddNumber(toAddItem);
                if (canAddNumber >= toAddNumber)
                {
                    BaseAdd(itemSlot, toAddItem, toAddNumber);
                    return;
                }

                if (canAddNumber > 0)
                {
                    BaseAdd(itemSlot, toAddItem, canAddNumber);
                    toAddNumber -= canAddNumber;
                }
            }

            throw new Exception("没有足够的空间, 请在添加前检查容量是否足够");
        }

        internal void Add(ItemStack stack, int index)
        {
            Add(stack.ItemIdentification, stack.Number, index);
        }

        internal void Add(ItemIdentification itemIdentification, int number, int index)
        {
            ItemSlot itemSlot = itemSlots[index];
            int canAddNumber = itemSlot.CanAddNumber(itemIdentification);
            if (canAddNumber >= number)
            {
                BaseAdd(itemSlot, itemIdentification, number);
                return;
            }

            throw new Exception("没有足够的空间, 请在添加前检查容量是否足够");
        }

        #endregion


        #region 删

        internal void Remove(ItemStack stack)
        {
            Remove(stack.ItemIdentification, stack.Number);
        }

        internal void Remove(ItemIdentification toRemoveItem, int toRemoveNumber)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                int canRemoveNumber = itemSlot.CanRemoveNumber(toRemoveItem);
                if (canRemoveNumber >= toRemoveNumber)
                {
                    BaseRemove(itemSlot, toRemoveNumber);
                    return;
                }

                if (canRemoveNumber > 0)
                {
                    BaseRemove(itemSlot, canRemoveNumber);
                    toRemoveNumber -= canRemoveNumber;
                }
            }

            throw new Exception("没有足够的物品可移除, 请在移除前检查物品是否足够");
        }

        internal void Remove(ItemStack stack, int index)
        {
            Remove(stack.ItemIdentification, stack.Number, index);
        }

        internal void Remove(ItemIdentification toRemoveItem, int toRemoveNumber, int index)
        {
            ItemSlot itemSlot = itemSlots[index];
            int canRemoveNumber = itemSlot.CanRemoveNumber(toRemoveItem);
            if (canRemoveNumber >= toRemoveNumber)
            {
                BaseRemove(itemSlot, toRemoveNumber);
                return;
            }

            throw new Exception("没有足够的物品可移除, 请在移除前检查物品是否足够");
        }

        #endregion


        #region 改

        internal void MergeOrSwitch(int fromIndex, int toIndex)
        {
            ItemSlot fromSlot = itemSlots[fromIndex];
            ItemSlot toSlot = itemSlots[toIndex];
            if (fromSlot.ItemStack == null || toSlot.ItemStack == null ||
                fromSlot.ItemStack.ItemIdentification != toSlot.ItemStack.ItemIdentification)
            {
                Switch(fromIndex, toIndex);
                return;
            }

            int canAddNumber = toSlot.CanAddNumber(fromSlot.ItemStack.ItemIdentification);
            int finalAddNumber = Mathf.Min(canAddNumber, fromSlot.ItemStack.Number);
            BaseRemove(fromSlot, finalAddNumber);
            BaseAdd(toSlot, toSlot.ItemStack.ItemIdentification, finalAddNumber);
        }

        internal void Switch(int fromIndex, int toIndex)
        {
            ItemSlot fromSlot = itemSlots[fromIndex];
            ItemSlot toSlot = itemSlots[toIndex];
            ItemStack? fromStack = fromSlot.ItemStack;
            ItemStack? toStack = toSlot.ItemStack;
            BaseSet(fromSlot, fromStack);
            BaseSet(toSlot, toStack);
        }

        #endregion


        #region 查

        public ItemSlot GetSlot(int index)
        {
            return itemSlots[index];
        }

        public (ItemSlot?, int) SearchFirstMatchedItem(IdentificationFilter filter)
        {
            for (int i = 0; i < capacity; i++)
            {
                ItemIdentification? itemStackItemIdentification = itemSlots[i].ItemStack?.ItemIdentification;
                if (filter.IsMatch(itemStackItemIdentification))
                    return (itemSlots[i], i);
            }

            return (null, -1);
        }

        public (ItemSlot?, int) SearchFirstMatchedSlot(IdentificationFilter filter)
        {
            for (int i = 0; i < capacity; i++)
                if (itemSlots[i].IdentificationFilter == filter)
                    return (itemSlots[i], i);

            return (null, -1);
        }

        public ItemSlot[] SearchIdentification(IdentificationFilter filter)
        {
            List<ItemSlot> slots = new List<ItemSlot>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                ItemIdentification? itemStackItemIdentification = itemSlots[i].ItemStack?.ItemIdentification;
                if (filter.IsMatch(itemStackItemIdentification))
                    slots.Add(itemSlots[i]);
            }

            return slots.ToArray();
        }

        public ItemSlot[] SearchSlot(IdentificationFilter filter)
        {
            List<ItemSlot> slots = new List<ItemSlot>(capacity);
            for (int i = 0; i < capacity; i++)
                if (itemSlots[i].IdentificationFilter == filter)
                    slots.Add(itemSlots[i]);
            return slots.ToArray();
        }

        public ItemSlot[] AllSlots()
        {
            return itemSlots;
        }

        public int CanAddNumber(ItemIdentification itemIdentification)
        {
            return itemSlots.Sum(slot => slot.CanAddNumber(itemIdentification));
        }

        public bool CanAddAll(ItemIdentification itemIdentification, int number)
        {
            return CanAddNumber(itemIdentification) >= number;
        }

        public bool IsFull(ItemIdentification itemIdentification)
        {
            return CanAddNumber(itemIdentification) > 0;
        }

        #endregion
    }
}