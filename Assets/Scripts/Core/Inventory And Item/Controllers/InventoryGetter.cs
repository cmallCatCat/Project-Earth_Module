using System;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    [RequireComponent(typeof(InventoryHolder))]
    public abstract class InventoryGetter : MonoBehaviour, IController
    {
        [SerializeField]protected InventoryHolder inventoryHolder;
        protected new Transform transform;


        private void Awake()
        {
            transform = GetComponent<Transform>();
            gameObject.AddComponent<CircleCollider2D>();
        }

        public void SetHolder(InventoryHolder inventoryHolder)
        {
            this.inventoryHolder = inventoryHolder;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("ItemPicker"))
            {
                return;
            }

            other.attachedRigidbody.AddForce((transform.position - other.transform.position).normalized * 100);
        }

        public abstract IArchitecture GetArchitecture();
    }
}