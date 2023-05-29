#nullable enable
using System;
using Core.QFramework.Framework.Scripts;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using InventoryAndItem.Core.Inventory_And_Item.Filters;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Data
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
            if (itemStack != null && !ItemFilter.IsMatch(itemStack.ItemInfo, itemStack.ItemDecorator))
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

            if (ItemFilter.IsMatch(newStack.ItemInfo, newStack.ItemDecorator))
            {
                itemStack = newStack;
                return;
            }

            throw new Exception("该物品栏不可添加此ItemStack");
        }

        #endregion

        #region 查询

        public bool Match(ItemStack? itemStack)
        {
            return itemFilter.IsMatch(itemStack?.ItemInfo, itemStack?.ItemDecorator);
        }

        public int CanAddNumber(ItemInfo itemInfo, ItemDecorator itemDecorator)
        {
            if (!ItemFilter.IsMatch(itemInfo, itemDecorator)) return 0;
            if (itemStack == null) return itemInfo.MaxStack;
            return itemStack.CanAddNumber(itemInfo, itemDecorator);
        }

        public int CanRemoveNumber(ItemInfo toRemoveItem, ItemDecorator itemDecorator)
        {
            if (itemStack == null) return 0;

            return itemStack.CanRemoveNumber(toRemoveItem, itemDecorator);
        }

        #endregion

        #region 序列化

        [SerializeField]
        private string packageNameSave = string.Empty;
        
        [SerializeField]
        private string itemNameSave = string.Empty;

        [SerializeField]
        private int numberSave;

        [SerializeField]
        private ItemDecorator itemDecoratorSave = new ItemDecorator();

        [SerializeField]
        private FilterType filterTypeSave;

        [SerializeField]
        private string[] filterStringsSave = Array.Empty<string>();

        public void OnBeforeSerialize()
        {
            if (ItemStack != null)
            {
                packageNameSave = ItemStack.ItemInfo.PackageName;
                itemNameSave = ItemStack.ItemInfo.ItemName;
                numberSave = ItemStack.Number;
                itemDecoratorSave = ItemStack.ItemDecorator;
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
                ItemInfo findItemInfo
                    = ItemDatabaseHandler.Instance.FindItem(new ItemIdentification(packageNameSave, itemNameSave));
                ItemInfo itemInfo =
                    SOHelper.CloneScriptableObject(findItemInfo) ?? throw new InvalidOperationException();
                int itemNumber = numberSave;
                Set(new ItemStack(itemInfo, itemDecoratorSave, itemNumber, null));
            }

            {
                itemFilter = new ItemFilter(filterTypeSave, filterStringsSave);
            }
        }

        #endregion
    }
}