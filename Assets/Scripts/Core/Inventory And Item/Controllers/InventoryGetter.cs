using System;
using Core.Root.Expand;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryGetter : MonoBehaviour, IController
    {
        private InventoryHolder inventoryHolder;

        private void Awake()
        {
            CircleCollider2D circleCollider2D = gameObject.GetOrAddComponent<CircleCollider2D>();
            circleCollider2D.isTrigger = true;
            circleCollider2D.radius = 0.15f;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("ItemPicker")) return;
            
            ItemStack itemStack = other.GetComponent<ItemPicker>().itemStack;
            if (!InventoryHolder.Inventory.CanAdd(itemStack.ItemInfo, itemStack.ItemDecorator)) return;

            InventoryHolder.Inventory.Add(itemStack);
            Destroy(other.gameObject);
        }

        public InventoryHolder InventoryHolder
        {
            get
            {
                if (inventoryHolder == null) inventoryHolder = GetInventoryHolder;
                return inventoryHolder;
            }
        }

        protected abstract InventoryHolder GetInventoryHolder { get; }

        public abstract    IArchitecture   GetArchitecture();
    }
}