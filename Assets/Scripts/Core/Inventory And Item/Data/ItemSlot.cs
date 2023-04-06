using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Filters;
using UnityEngine;
using static Core.Inventory_And_Item.Filters.IdentificationFilters;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    public class ItemSlot : ISerializationCallbackReceiver
    {
        [SerializeField] private ItemStack itemStack;

        [SerializeField] private IdentificationFilterType identificationFilterType;
        [SerializeField] private List<ItemIdentification> customIdentifications;
        [NonSerialized] private IdentificationFilter identificationFilter;

        public ItemSlot()
        {
            itemStack = null;
            identificationFilter = All;
        }

        public ItemStack ItemStack => itemStack;
        public IdentificationFilter IdentificationFilter => identificationFilter;

        public void OnBeforeSerialize()
        {
            if (IdentificationFilter == All)
            {
                identificationFilterType = IdentificationFilterType.All;
            }
            else if (IdentificationFilter == None)
            {
                identificationFilterType = IdentificationFilterType.None;
            }
            else if (IdentificationFilter is CustomIdentificationFilter customIdentificationFilter)
            {
                identificationFilterType = IdentificationFilterType.Custom;
                customIdentifications = customIdentificationFilter.ItemIdentifications;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(IdentificationFilter));
            }
        }

        public void OnAfterDeserialize()
        {
            identificationFilter = identificationFilterType switch
            {
                IdentificationFilterType.All => All,
                IdentificationFilterType.None => None,
                IdentificationFilterType.Custom => new CustomIdentificationFilter(customIdentifications),
                _ => throw new ArgumentOutOfRangeException(nameof(identificationFilterType))
            };
        }

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