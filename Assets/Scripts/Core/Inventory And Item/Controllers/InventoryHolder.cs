using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using Core.Save_And_Load.Interfaces;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryHolder : MonoBehaviour, IController, ISave
    {
        [SerializeField] private int inventoryCapacity;
        [SerializeField] private int inventoryKey = -1;
        [SerializeField] private Inventory inventory; // TODO: 正式运行时删除SerializeField

        public int InventoryKey => inventoryKey;
        public Inventory Inventory => inventory;

        protected void SetInventory(int inventoryKey)
        {
            if (this.inventoryKey != -1)
                UnregisterInventory();
            this.inventoryKey = inventoryKey;
            inventory = this.GetModel<InventoryModel>().inventories[inventoryKey];
        }

        public void RegisterInventory()
        {
            inventory = new Inventory(inventoryCapacity);
            inventoryKey = this.GetModel<InventoryModel>().Register(inventory);
        }

        protected void UnregisterInventory()
        {
            this.GetModel<InventoryModel>().Unregister(inventoryKey);
            inventoryKey = -1;
            inventory = null;
        }

        protected virtual void OnDestroy()
        {
            UnregisterInventory();
        }

        public abstract IArchitecture GetArchitecture();

        public object Save()
        {
            return inventoryKey;
        }

        public void Load(object save)
        {
            SetInventory((int)save);
        }
    }
}