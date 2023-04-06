using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using Framework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryHolder : MonoBehaviour, IController
    {
        [SerializeField] private int inventoryCapacity;
        [SerializeField] private int inventoryKey;
        [SerializeField] private Inventory inventory; // TODO: 正式运行时删除SerializeField
        public int InventoryKey => inventoryKey;
        public Inventory Inventory => inventory;

        protected virtual void Awake()
        {
            inventory = new Inventory(inventoryCapacity);
            inventoryKey = this.GetModel<InventoryModel>().Register(inventory);
        }

        private void OnDestroy()
        {
            this.GetModel<InventoryModel>().Unregister(inventoryKey);
        }


        public abstract IArchitecture GetArchitecture();

        public void Load(int inventoryKey)
        {
            this.inventoryKey = inventoryKey;
            inventory = this.GetModel<InventoryModel>().inventories[inventoryKey];
        }
    }
}