using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryGravitater : MonoBehaviour, IController
    {
        [SerializeField]
        protected int force = 10;

        private InventoryHolder inventoryHolder;

        public InventoryHolder InventoryHolder
        {
            get
            {
                if (inventoryHolder == null)
                {
                    inventoryHolder = GetInventoryHolder;
                }
                
                return inventoryHolder;
            }
            set => inventoryHolder = value;
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("ItemPicker"))
            {
                return;
            }
            
            ItemStack itemStack = other.GetComponent<ItemPicker>().ItemStack;
            if (!InventoryHolder.Inventory.CanAdd(itemStack.ItemInfo, itemStack.ItemDecorator))
            {
                return;
            }

            other.attachedRigidbody.AddForce((transform.position - other.transform.position).normalized * force);
        }

        protected abstract InventoryHolder GetInventoryHolder { get; }
        
        public abstract IArchitecture GetArchitecture();
    }
}