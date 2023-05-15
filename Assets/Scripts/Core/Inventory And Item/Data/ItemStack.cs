using System;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using Core.QFramework.Framework.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    public class  ItemStack : ISerializationCallbackReceiver
    {
        [SerializeField] private int number;
        [NonSerialized] private ItemIdentification itemIdentification;

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

        public static bool IsEmpty(ItemStack itemStack)
        {
            return itemStack == null || itemStack.Number == 0;
        }

        public void Add(ItemIdentification toAddItem, int toAdd)
        {
            if (itemIdentification != null && itemIdentification != toAddItem) throw new Exception("该ItemStack不可应用此添加不同的物品");

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

        #region 序列化

        [SerializeField] private string itemNameSave;

        public void OnBeforeSerialize()
        {
            itemNameSave = ItemIdentification.GetType().FullName;
        }

        public void OnAfterDeserialize()
        {
            ItemIdentification findItemIdentification = SOFinder.FindItemIdentification(itemNameSave);
            itemIdentification = SOHelper.CloneScriptableObject(findItemIdentification);
        }

        #endregion
    }

    public static class SOFinder
    {
        public static ItemIdentification FindItemIdentification(string FullName)
        {
            // TODO: 在这里添加查找物品的逻辑
            throw new NotImplementedException();
        }
    }
}