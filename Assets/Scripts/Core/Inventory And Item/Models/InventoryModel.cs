using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Events;
using Core.Inventory_And_Item.Filters;
using Core.QFramework;
using Core.Save_And_Load.DictionaryAndList;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Models
{
    [Serializable]
    public class InventoryModel : AbstractModel
    {
        public int inventoryKey;
        public Dictionary<int, Inventory> inventories; // TODO: 正式运行时改为private
        private int playerKey = -1;

        protected override void OnInit()
        {
            inventoryKey = 0;
            inventories = new Dictionary<int, Inventory>();
        }

        public int Register(Inventory inventory, bool isPlayer = false)
        {
            if (isPlayer)
            {
                if (playerKey != -1)
                {
                    Debug.LogWarning("InventoryModel.Register: PlayerKey is assigned. Please confirm it.");
                }
                playerKey = inventoryKey;
            }

            inventories.Add(inventoryKey, inventory);
            return inventoryKey++;
        }

        public void Unregister(int holder)
        {
            inventories.Remove(holder);
        }

        public int PlayerKey => playerKey;

        #region 增删改查

        public void Add(int holder, ItemStack stack)
        {
            inventories[holder].Add(stack);
            this.SendEvent(new ItemChangedEvent(holder, stack, ItemChangedEvent.ChangeType.Add));
        }

        public void Add(int holder, ItemStack stack, int index)
        {
            inventories[holder].Add(stack, index);
            this.SendEvent(new ItemChangedEvent(holder, stack, ItemChangedEvent.ChangeType.Add));
        }

        public void Remove(int holder, ItemStack stack)
        {
            inventories[holder].Remove(stack);
            this.SendEvent(new ItemChangedEvent(holder, stack, ItemChangedEvent.ChangeType.Remove));
        }

        public void Remove(int holder, ItemStack itemStack, int index)
        {
            inventories[holder].Remove(itemStack, index);
            this.SendEvent(new ItemChangedEvent(holder, itemStack, ItemChangedEvent.ChangeType.Remove));
        }

        public void MergeOrSwitch(int holder, int fromIndex, int toIndex)
        {
            inventories[holder].MergeOrSwitch(fromIndex, toIndex);
            this.SendEvent(new ItemChangedEvent(holder, null, ItemChangedEvent.ChangeType.Move));
        }

        public void Switch(int holder, int fromIndex, int toIndex)
        {
            inventories[holder].Switch(fromIndex, toIndex);
            this.SendEvent(new ItemChangedEvent(holder, null, ItemChangedEvent.ChangeType.Move));
        }

        public ItemSlot[] Search(int holder, SlotFilter filter)
        {
            return inventories[holder].SearchSlot(filter);
        }

        #endregion

        #region 存储

        [Serializable]
        public class InventorySave
        {
            public List<KeyValueStruct<int, Inventory>> inventories;
            public int inventoryKey;

            public InventorySave(Dictionary<int, Inventory> inventories, int inventoryKey)
            {
                this.inventories = DictionaryTransList.DictionaryToList(inventories);
                this.inventoryKey = inventoryKey;
            }
        }

        public InventorySave GetSave()
        {
            return new InventorySave(inventories, inventoryKey);
        }

        public void GetLoad(InventorySave inventorySave)
        {
            inventories = DictionaryTransList.ListToDictionary(inventorySave.inventories);
            inventoryKey = inventorySave.inventoryKey;
        }

        #endregion
    }
}