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
    public class ItemSlot : ISerializationCallbackReceiver, IEffectSender
    {
        private ItemStack? itemStack;
        private IdentificationFilter identificationFilter;

        public ItemStack? ItemStack => itemStack;

        public IdentificationFilter IdentificationFilter => identificationFilter;

        public ItemSlot()
        {
            itemStack = null;
            identificationFilter = new IdentificationFilter(FilterType.All, Array.Empty<string>());
        }

        internal void SetIdentificationFilter(IdentificationFilter newIdentificationFilter)
        {
            if (itemStack != null && !IdentificationFilter.IsMatch(itemStack.ItemIdentification))
                throw new Exception("因为该物品栏内物品不符合条件，该物品栏不可应用此IdentificationFilter，请转移物品后再试");

            identificationFilter = newIdentificationFilter;
        }

        #region 修改

        internal void Add(ItemIdentification toAddItem, int toAdd)
        {
            if (itemStack == null)
                itemStack = new ItemStack(toAddItem, toAdd);
            else
                itemStack.Add(toAddItem, toAdd);
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

            if (IdentificationFilter.IsMatch(newStack.ItemIdentification))
            {
                itemStack = newStack;
                return;
            }

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
            return identificationFilter.IsMatch(itemStack.ItemIdentification);
        }

        public int CanAddNumber(ItemIdentification itemIdentification)
        {
            if (!IdentificationFilter.IsMatch(itemIdentification)) return 0;

            if (itemStack == null) return itemIdentification.MaxStack;

            return itemStack.CanAddNumber(itemIdentification);
        }

        public int CanRemoveNumber(ItemIdentification toRemoveItem)
        {
            if (itemStack == null) return 0;

            return itemStack.CanRemoveNumber(toRemoveItem);
        }

        #endregion

        #region 序列化

        [SerializeField] private string itemNameSave = string.Empty;
        [SerializeField] private int numberSave;
        [SerializeField] private FilterType filterTypeSave;
        [SerializeField] private string[] filterStringsSave = Array.Empty<string>();

        public void OnBeforeSerialize()
        {
            if (ItemStack != null)
            {
                itemNameSave = ItemStack.ItemIdentification.GetType().FullName;
                numberSave = ItemStack.Number;
            }

            {
                filterTypeSave = IdentificationFilter.filterType;
                filterStringsSave = IdentificationFilter.strings;
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
                Set(new ItemStack(itemIdentification, itemNumber));
            }

            {
                identificationFilter = new IdentificationFilter(filterTypeSave, filterStringsSave);
            }
        }

        #endregion

        public Transform GetTransform()
        {
            throw new NotImplementedException();
        }
    }
}