using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos.ItemEffects;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryHolder : MonoBehaviour, IController
    {
        [SerializeField]
        private int inventoryCapacity;

        [field: SerializeField]
        public Inventory Inventory { get; private set; }

        protected void Awake()
        {
            RegisterInventory(GetEnvironment);
        }

        protected void RegisterInventory(IEnvironment environment)
        {
            Inventory = new Inventory(inventoryCapacity, transform);
            environment.Register<Inventory>(this);
        }

        protected void UnregisterInventory(IEnvironment environment)
        {
            environment.Unregister<Inventory>(this);
            Inventory = null;
        }

        protected virtual void OnDestroy()
        {
            UnregisterInventory(GetEnvironment);
        }

        protected abstract IEnvironment GetEnvironment { get; }

        public abstract IArchitecture GetArchitecture();
    }
}