using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryGetter : MonoBehaviour, IController
    {
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
            if (!InventoryHolder.Inventory.CanAdd(itemStack.ItemIdentification, itemStack.ItemDecorator))
            {
                return;
            }
            
            InventoryHolder.Inventory.Add(itemStack);
            Destroy(other.gameObject);
        }

        protected abstract InventoryHolder GetInventoryHolder { get; }
        public abstract IArchitecture GetArchitecture();
    }
}