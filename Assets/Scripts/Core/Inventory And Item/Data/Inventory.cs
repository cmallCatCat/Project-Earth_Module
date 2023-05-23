#nullable enable
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
        private ItemSlot[] itemSlots;

        private Transform transform;
        
        public event OnInventoryChanged? onItemChanged;

        public int Capacity => capacity;


        public Inventory(int capacity, Transform transform)
        {
            this.capacity = capacity;
            this.transform = transform;
            itemSlots = new ItemSlot[capacity];
            for (int i = 0; i < capacity; i++) itemSlots[i] = new ItemSlot();

        }

        #region BaseCommands

        private void BaseAdd(ItemSlot itemSlot, ItemStack itemStack)
        {
            itemSlot.Add(itemStack);
        }

        private void BaseRemove(ItemSlot itemSlot, int toRemoveNumber)
        {
            itemSlot.Remove(toRemoveNumber);
        }

        private void BaseSet(ItemSlot itemSlot, ItemStack? newStack)
        {
            itemSlot.Set(newStack);
        }

        #endregion


        #region 增

        public void Add(ItemStack stack)
        {
            ItemIdentification toAddItem = stack.ItemIdentification;
            ItemDecorator toAddDecorator = stack.ItemDecorator;
            int toAddNumber = stack.Number;
            foreach (ItemSlot itemSlot in itemSlots)
            {
                int canAddNumber = itemSlot.CanAddNumber(toAddItem, toAddDecorator);
                if (canAddNumber >= toAddNumber)
                {
                    BaseAdd(itemSlot, new ItemStack(toAddItem, toAddDecorator, toAddNumber));
                    onItemChanged?.Invoke();
                    return;
                }

                if (canAddNumber > 0)
                {
                    BaseAdd(itemSlot, new ItemStack(toAddItem, toAddDecorator, canAddNumber));
                    toAddNumber -= canAddNumber;
                }
            }

            throw new Exception("没有足够的空间, 请在添加前检查容量是否足够");
        }

        public void Add(ItemStack stack, int index)
        {
            ItemIdentification toAddIdentification = stack.ItemIdentification;
            ItemDecorator toAddDecorator = stack.ItemDecorator;
            int toAddNumber = stack.Number;

            ItemSlot itemSlot = itemSlots[index];
            int canAddNumber = itemSlot.CanAddNumber(toAddIdentification, toAddDecorator);
            if (canAddNumber >= toAddNumber)
            {
                BaseAdd(itemSlot, new ItemStack(toAddIdentification, toAddDecorator, toAddNumber));
                onItemChanged?.Invoke();
                return;
            }

            throw new Exception("没有足够的空间, 请在添加前检查容量是否足够");
        }

        #endregion


        #region 删

        public void Remove(ItemStack stack)
        {
            ItemIdentification toRemoveItem = stack.ItemIdentification;
            ItemDecorator toRemoveDecorator = stack.ItemDecorator;
            int toRemoveNumber = stack.Number;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                int canRemoveNumber = itemSlot.CanRemoveNumber(toRemoveItem, toRemoveDecorator);
                if (canRemoveNumber >= toRemoveNumber)
                {
                    BaseRemove(itemSlot, toRemoveNumber);
                    onItemChanged?.Invoke();
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

        public void Remove(ItemStack stack, int index)
        {
            ItemIdentification toRemoveItem = stack.ItemIdentification;
            ItemDecorator toRemoveDecorator = stack.ItemDecorator;
            int toRemoveNumber = stack.Number;

            ItemSlot itemSlot = itemSlots[index];
            int canRemoveNumber = itemSlot.CanRemoveNumber(toRemoveItem, toRemoveDecorator);
            if (canRemoveNumber >= toRemoveNumber)
            {
                BaseRemove(itemSlot, toRemoveNumber);
                onItemChanged?.Invoke();
                return;
            }

            throw new Exception("没有足够的物品可移除, 请在移除前检查物品是否足够");
        }

        #endregion


        #region 改

        public int MergeOrSwitch(int fromIndex, int toIndex, bool invoke = true)
        {
            ItemSlot fromSlot = itemSlots[fromIndex];
            ItemSlot toSlot = itemSlots[toIndex];
            if (fromSlot.ItemStack == null || toSlot.ItemStack == null ||
                fromSlot.ItemStack.ItemIdentification != toSlot.ItemStack.ItemIdentification)
            {
                Switch(fromIndex, toIndex, invoke);
                if (invoke)
                {
                    onItemChanged?.Invoke();
                }

                return -1;
            }

            int canAddNumber = toSlot.CanAddNumber(fromSlot.ItemStack.ItemIdentification, fromSlot.ItemStack.ItemDecorator);
            int finalAddNumber = Mathf.Min(canAddNumber, fromSlot.ItemStack.Number);
            BaseRemove(fromSlot, finalAddNumber);
            BaseAdd(toSlot, new ItemStack(toSlot.ItemStack.ItemIdentification, toSlot.ItemStack.ItemDecorator, finalAddNumber));
            if (invoke)
            {
                onItemChanged?.Invoke();
            }

            return finalAddNumber;
        }

        public void Switch(int index1, int index2, bool invoke = true)
        {
            ItemSlot slot1 = itemSlots[index1];
            ItemSlot slot2 = itemSlots[index2];
            ItemStack? stack1 = slot1.ItemStack;
            ItemStack? stack2 = slot2.ItemStack;
            BaseSet(slot1, stack2);
            BaseSet(slot2, stack1);
            if (invoke)
            {
                onItemChanged?.Invoke();
            }
        }

        #endregion


        #region 查

        public ItemSlot GetSlot(int index)
        {
            return itemSlots[index];
        }

        public (ItemSlot?, int) SearchFirstMatchedItem(ItemFilter filter)
        {
            for (int i = 0; i < capacity; i++)
            {
                ItemIdentification? itemStackItemIdentification = itemSlots[i].ItemStack?.ItemIdentification;
                ItemDecorator? itemStackItemDecorator = itemSlots[i].ItemStack?.ItemDecorator;
                if (filter.IsMatch(itemStackItemIdentification, itemStackItemDecorator))
                    return (itemSlots[i], i);
            }

            return (null, -1);
        }

        public (ItemSlot?, int) SearchFirstMatchedSlot(ItemFilter filter)
        {
            for (int i = 0; i < capacity; i++)
                if (itemSlots[i].ItemFilter == filter)
                    return (itemSlots[i], i);

            return (null, -1);
        }

        public ItemSlot[] SearchIdentification(ItemFilter filter)
        {
            List<ItemSlot> slots = new List<ItemSlot>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                ItemIdentification? itemStackItemIdentification = itemSlots[i].ItemStack?.ItemIdentification;
                ItemDecorator? itemStackItemDecorator = itemSlots[i].ItemStack?.ItemDecorator;
                if (filter.IsMatch(itemStackItemIdentification, itemStackItemDecorator))
                    slots.Add(itemSlots[i]);
            }

            return slots.ToArray();
        }

        public ItemSlot[] SearchSlot(ItemFilter filter)
        {
            List<ItemSlot> slots = new List<ItemSlot>(capacity);
            for (int i = 0; i < capacity; i++)
                if (itemSlots[i].ItemFilter == filter)
                    slots.Add(itemSlots[i]);
            return slots.ToArray();
        }

        public ItemSlot[] AllSlots()
        {
            return itemSlots;
        }

        public int CanAddNumber(ItemIdentification itemIdentification, ItemDecorator itemDecorator)
        {
            return itemSlots.Sum(slot => slot.CanAddNumber(itemIdentification, itemDecorator));
        }

        public bool CanAddAll(ItemStack itemStack)
        {
            return CanAddNumber(itemStack.ItemIdentification, itemStack.ItemDecorator) >= itemStack.Number;
        }

        public bool CanAdd(ItemIdentification itemIdentification, ItemDecorator itemDecorator)
        {
            return CanAddNumber(itemIdentification, itemDecorator) > 0;
        }

        #endregion
    }
}