#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using InventoryAndItem.Core.Inventory_And_Item.Filters;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data
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
            this.capacity  = capacity;
            this.transform = transform;
            itemSlots      = new ItemSlot[capacity];
            for (int i = 0; i < capacity; i++) itemSlots[i] = new ItemSlot();
        }

        #region BaseCommands
        private void BaseAdd(ItemSlot itemSlot, ItemStack itemStack)
        {
            itemStack.SetTransform(transform);
            itemSlot.Add(itemStack);
            onItemChanged?.Invoke();
        }

        private void BaseRemove(ItemSlot itemSlot, int toRemoveNumber)
        {
            itemSlot.Remove(toRemoveNumber);
            onItemChanged?.Invoke();
        }

        private void BaseSet(ItemSlot itemSlot, ItemStack? newStack)
        {
            if (newStack == null)
            {
                itemSlot.Set(null);
                onItemChanged?.Invoke();
                return;
            }

            newStack.SetTransform(transform);
            itemSlot.Set(newStack);
            onItemChanged?.Invoke();
        }
        #endregion


        #region 增
        public void Add(ItemStack stack)
        {
            ItemInfo      toAddItem      = stack.ItemInfo;
            ItemDecorator toAddDecorator = stack.ItemDecorator;
            int           toAddNumber    = stack.Number;
            foreach (ItemSlot itemSlot in itemSlots)
            {
                int canAddNumber = itemSlot.CanAddNumber(toAddItem, toAddDecorator);
                if (canAddNumber >= toAddNumber)
                {
                    BaseAdd(itemSlot, new ItemStack(toAddItem, toAddDecorator, toAddNumber, transform));
                    return;
                }

                if (canAddNumber > 0)
                {
                    BaseAdd(itemSlot, new ItemStack(toAddItem, toAddDecorator, canAddNumber, transform));
                    toAddNumber -= canAddNumber;
                }
            }

            throw new Exception("没有足够的空间, 请在添加前检查容量是否足够");
        }

        public void Add(ItemStack stack, int index)
        {
            ItemInfo      toAddInfo      = stack.ItemInfo;
            ItemDecorator toAddDecorator = stack.ItemDecorator;
            int           toAddNumber    = stack.Number;

            ItemSlot itemSlot     = itemSlots[index];
            int      canAddNumber = itemSlot.CanAddNumber(toAddInfo, toAddDecorator);
            if (canAddNumber >= toAddNumber)
            {
                BaseAdd(itemSlot, new ItemStack(toAddInfo, toAddDecorator, toAddNumber, transform));
                return;
            }

            throw new Exception("没有足够的空间, 请在添加前检查容量是否足够");
        }
        #endregion


        #region 删
        public void Remove(ItemStack stack)
        {
            ItemInfo      toRemoveItem      = stack.ItemInfo;
            ItemDecorator toRemoveDecorator = stack.ItemDecorator;
            int           toRemoveNumber    = stack.Number;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                int canRemoveNumber = itemSlot.CanRemoveNumber(toRemoveItem, toRemoveDecorator);
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

        public void Remove(ItemStack stack, int index)
        {
            ItemInfo      toRemoveItem      = stack.ItemInfo;
            ItemDecorator toRemoveDecorator = stack.ItemDecorator;
            int           toRemoveNumber    = stack.Number;

            ItemSlot itemSlot        = itemSlots[index];
            int      canRemoveNumber = itemSlot.CanRemoveNumber(toRemoveItem, toRemoveDecorator);
            if (canRemoveNumber >= toRemoveNumber)
            {
                BaseRemove(itemSlot, toRemoveNumber);
                return;
            }

            throw new Exception("没有足够的物品可移除, 请在移除前检查物品是否足够");
        }

        public void Remove(int index)
        {
            ItemSlot itemSlot = itemSlots[index];
            if (itemSlot.ItemStack != null)
            {
                BaseRemove(itemSlot, itemSlot.ItemStack.Number);
                return;
            }
            throw new Exception("没有物品可移除");
        }
        #endregion


        #region 改
        private int MergeOrSwitch(int fromIndex, int toIndex)
        {
            ItemSlot fromSlot = itemSlots[fromIndex];
            ItemSlot toSlot   = itemSlots[toIndex];
            if (fromSlot.ItemStack == null || toSlot.ItemStack == null ||
                fromSlot.ItemStack.ItemInfo != toSlot.ItemStack.ItemInfo)
            {
                Switch(fromIndex, toIndex);
                return -1;
            }

            int canAddNumber   = toSlot.CanAddNumber(fromSlot.ItemStack.ItemInfo, fromSlot.ItemStack.ItemDecorator);
            int finalAddNumber = Mathf.Min(canAddNumber, fromSlot.ItemStack.Number);
            BaseRemove(fromSlot, finalAddNumber);
            BaseAdd(toSlot, new ItemStack(toSlot.ItemStack.ItemInfo, toSlot.ItemStack.ItemDecorator, finalAddNumber, transform));

            return finalAddNumber;
        }

        public ItemStack? MergeOrSwitch(ItemStack fromStack, int toIndex)
        {
            ItemSlot toSlot = itemSlots[toIndex];
            if (!toSlot.Match(fromStack)) return fromStack;

            int canAddNumber = toSlot.CanAddNumber(fromStack.ItemInfo, fromStack.ItemDecorator);
            if (canAddNumber > 0)
            {
                int finalAddNumber = Mathf.Min(canAddNumber, fromStack.Number);
                BaseAdd(toSlot, new ItemStack(fromStack.ItemInfo, fromStack.ItemDecorator, finalAddNumber, transform));
                if (fromStack.Number - finalAddNumber > 0)
                    return new ItemStack(fromStack.ItemInfo, fromStack.ItemDecorator, fromStack.Number - finalAddNumber,
                        transform);

                return null;
            }

            ItemStack? slotItemStack = toSlot.ItemStack;
            toSlot.Set(fromStack);
            return slotItemStack;
        }

        public void Switch(int index1, int index2)
        {
            ItemSlot   slot1  = itemSlots[index1];
            ItemSlot   slot2  = itemSlots[index2];
            ItemStack? stack1 = slot1.ItemStack;
            ItemStack? stack2 = slot2.ItemStack;
            BaseSet(slot1, stack2);
            BaseSet(slot2, stack1);
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
                ItemInfo?      itemStackItemIdentification = itemSlots[i].ItemStack?.ItemInfo;
                ItemDecorator? itemStackItemDecorator      = itemSlots[i].ItemStack?.ItemDecorator;
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
                ItemInfo?      itemStackItemIdentification = itemSlots[i].ItemStack?.ItemInfo;
                ItemDecorator? itemStackItemDecorator      = itemSlots[i].ItemStack?.ItemDecorator;
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

        public int CanAddNumber(ItemInfo itemInfo, ItemDecorator itemDecorator)
        {
            return itemSlots.Sum(slot => slot.CanAddNumber(itemInfo, itemDecorator));
        }

        public int CanAddNumberFinal(ItemStack itemStack)
        {
            return Mathf.Min(CanAddNumber(itemStack.ItemInfo, itemStack.ItemDecorator), itemStack.Number);
        }

        public bool CanAddAll(ItemStack itemStack)
        {
            return CanAddNumber(itemStack.ItemInfo, itemStack.ItemDecorator) >= itemStack.Number;
        }

        public bool CanAdd(ItemInfo itemInfo, ItemDecorator itemDecorator)
        {
            return CanAddNumber(itemInfo, itemDecorator) > 0;
        }
        #endregion
    }
}