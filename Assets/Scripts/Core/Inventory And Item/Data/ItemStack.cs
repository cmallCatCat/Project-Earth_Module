using System;
using Core.Save_And_Load.Utilities;
using UnityEngine;

namespace Core.Inventory_And_Item.Data
{
    [Serializable]
    public class ItemStack : ISerializationCallbackReceiver
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

        [SerializeField] private string itemJson;

        public void OnBeforeSerialize()
        {
            itemJson = JsonUtility.ToJson(ItemIdentification);
        }

        public void OnAfterDeserialize()
        {
            itemIdentification = SOHelper.JsonToScriptableObject<ItemIdentification>(itemJson);
        }

        #endregion
    }
}