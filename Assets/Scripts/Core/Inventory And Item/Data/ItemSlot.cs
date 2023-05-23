#nullable enable
using System;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using Core.Inventory_And_Item.Filters;
using Core.QFramework.Framework.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    public class ItemSlot : ISerializationCallbackReceiver
    {
        private ItemStack? itemStack;
        private ItemFilter itemFilter;

        public ItemStack? ItemStack => itemStack;

        public ItemFilter ItemFilter => itemFilter;

        public ItemSlot()
        {
            itemStack = null;
            itemFilter = new ItemFilter(FilterType.All, Array.Empty<string>());
        }

        internal void SetIdentificationFilter(ItemFilter newItemFilter)
        {
            if (itemStack != null && !ItemFilter.IsMatch(itemStack.ItemIdentification, itemStack.ItemDecorator))
                throw new Exception("因为该物品栏内物品不符合条件，该物品栏不可应用此IdentificationFilter，请转移物品后再试");

            itemFilter = newItemFilter;
        }

        #region 修改

        internal void Add(ItemStack toAddStack)
        {
            if (itemStack == null)
                itemStack = toAddStack;
            else
                itemStack.Add(toAddStack);
        }

        internal void Remove(int toRemoveNumber)
        {
            if (itemStack == null) throw new Exception("该物品栏内无物品，不可减少物品");
            itemStack.Remove(toRemoveNumber);
            if (itemStack.Number == 0) itemStack = null;
        }

        internal void Set(ItemStack? newStack)
        {
            if (newStack == null)
            {
                itemStack = null;
                return;
            }

            if (ItemFilter.IsMatch(newStack.ItemIdentification, newStack.ItemDecorator))
            {
                itemStack = newStack;
                return;
            }

            throw new Exception("该物品栏不可添加此ItemStack");
        }

        #endregion

        #region 查询

        public bool Match(ItemStack itemStack)
        {
            return itemFilter.IsMatch(itemStack.ItemIdentification, itemStack.ItemDecorator);
        }

        public int CanAddNumber(ItemIdentification itemIdentification, ItemDecorator itemDecorator)
        {
            if (!ItemFilter.IsMatch(itemIdentification,itemDecorator)) return 0;
            if (itemStack == null) return itemIdentification.MaxStack;
            return itemStack.CanAddNumber(itemIdentification, itemDecorator);
        }

        public int CanRemoveNumber(ItemIdentification toRemoveItem, ItemDecorator itemDecorator)
        {
            if (itemStack == null) return 0;

            return itemStack.CanRemoveNumber(toRemoveItem, itemDecorator);
        }

        #endregion

        #region 序列化

        [SerializeField]
        private string itemNameSave = string.Empty;

        [SerializeField]
        private int numberSave;

        [SerializeField]
        private ItemDecorator itemDecorator = new ItemDecorator();

        [SerializeField]
        private FilterType filterTypeSave;

        [SerializeField]
        private string[] filterStringsSave = Array.Empty<string>();

        public void OnBeforeSerialize()
        {
            if (ItemStack != null)
            {
                itemNameSave = ItemStack.ItemIdentification.GetType().FullName;
                numberSave = ItemStack.Number;
                itemDecorator = ItemStack.ItemDecorator;
            }

            {
                filterTypeSave = ItemFilter.filterType;
                filterStringsSave = ItemFilter.strings;
            }
        }

        public void OnAfterDeserialize()
        {
            if (itemNameSave != string.Empty)
            {
                ItemIdentification findItemIdentification = SOFinder.FindItemIdentification(itemNameSave);
                ItemIdentification itemIdentification =
                    SOHelper.CloneScriptableObject(findItemIdentification) ?? throw new InvalidOperationException();
                int itemNumber = numberSave;
                Set(new ItemStack(itemIdentification, itemDecorator, itemNumber));
            }

            {
                itemFilter = new ItemFilter(filterTypeSave, filterStringsSave);
            }
        }

        #endregion

    }
}