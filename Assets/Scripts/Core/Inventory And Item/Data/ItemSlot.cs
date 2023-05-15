using System;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.Inventory_And_Item.Filters;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    public class ItemSlot
    {
        [SerializeField] private ItemStack itemStack;
        [SerializeReference] private IdentificationFilter identificationFilter;

        public ItemSlot()
        {
            itemStack = null;
            identificationFilter = new U_EnumIdentificationFilter(Array.Empty<string>());
        }

        public ItemStack ItemStack => itemStack;
        public IdentificationFilter IdentificationFilter => identificationFilter;

        internal void SetIdentificationFilter(IdentificationFilter newIdentificationFilter)
        {
            if (!IsEmpty() && !IdentificationFilter.IsMatch(itemStack.ItemIdentification))
                throw new Exception("因为该物品栏内物品不符合条件，该物品栏不可应用此IdentificationFilter，请转移物品后再试");

            identificationFilter = newIdentificationFilter;
        }

        #region 修改

        internal void Add(ItemIdentification toAddItem, int toAdd)
        {
            if (IsEmpty())
                itemStack = new ItemStack(toAddItem, toAdd);
            else
                itemStack.Add(toAddItem, toAdd);
        }

        internal void Remove(int toRemoveNumber)
        {
            itemStack.Remove(toRemoveNumber);
            if (itemStack.Number == 0) itemStack = null;
        }

        internal void RemoveAll()
        {
            itemStack = null;
        }

        internal void Set(ItemStack newStack)
        {
            if (ItemStack.IsEmpty(newStack) || IdentificationFilter.IsMatch(newStack.ItemIdentification)) itemStack = newStack;

            throw new Exception("该物品栏不可添加此ItemStack");
        }

        #endregion

        #region 查询

        public bool Match(ItemIdentification itemIdentification)
        {
            return identificationFilter.IsMatch(itemIdentification);
        }

        public bool Match(ItemStack itemStack)
        {
            if (ItemStack.IsEmpty(itemStack)) return true;

            return identificationFilter.IsMatch(itemStack.ItemIdentification);
        }

        public int CanAddNumber(ItemIdentification itemIdentification)
        {
            if (!IdentificationFilter.IsMatch(itemIdentification)) return 0;

            if (ItemStack.IsEmpty(itemStack)) return itemIdentification.MaxStack;

            return itemStack.CanAddNumber(itemIdentification);
        }

        public bool IsEmpty()
        {
            return ItemStack.IsEmpty(itemStack);
        }

        public int CanRemoveNumber(ItemIdentification toRemoveItem)
        {
            if (IsEmpty()) return 0;

            return itemStack.CanRemoveNumber(toRemoveItem);
        }

        #endregion
    }
}