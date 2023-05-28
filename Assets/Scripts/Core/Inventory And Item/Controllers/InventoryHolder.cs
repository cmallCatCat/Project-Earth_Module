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
            RegisterInventory();
        }

        protected void RegisterInventory()
        {
            Inventory = new Inventory(inventoryCapacity, transform);
            IEnvironment.Instance.Register<Inventory>(this);
        }

        protected void UnregisterInventory()
        {
            IEnvironment.Instance.Unregister<Inventory>(this);
            Inventory = null;
        }

        protected virtual void OnDestroy()
        {
            UnregisterInventory();
        }


        public abstract IArchitecture GetArchitecture();
    }
}