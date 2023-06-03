using System.Collections.Generic;
using System.Linq;
using Core.Root.Expand;
using InventoryAndItem.Core.Inventory_And_Item.Command;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public abstract class ItemPicker : MonoBehaviour, IController
    {
        public  ItemStack            itemStack;
        private List<ItemGravitater> inventoryGravitaterList;

        private new Transform        transform;
        private new CircleCollider2D collider;

        public const float Range_Normal   = 1f;
        public const float Range_Long     = 3f;
        public const float Range_VeryLong = 5f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ItemGravitater inventoryGravitater))
                inventoryGravitaterList.Add(inventoryGravitater);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ItemGravitater inventoryGravitater))
                inventoryGravitaterList.Remove(inventoryGravitater);
        }

        private void Update()
        {
            ItemGravitater target = inventoryGravitaterList.FirstOrDefault(gravitater =>
                gravitater.InventoryHolder.Inventory.CanAdd(itemStack.ItemInfo, itemStack.ItemDecorator));
            if (target == null)
                return;

            Vector3 distance = (target.transform.position - transform.position);
            float   speed    = 20 / (distance.magnitude + 2f);
            transform.Translate(distance.normalized * (speed * Time.deltaTime));

            if (transform.position.InDistance(target.transform.position, 0.1f))
            {
                this.SendCommand(new PickupCommand(this, target));
                if (itemStack.Number == 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void Awake()
        {
            inventoryGravitaterList = new List<ItemGravitater>();
            collider                = GetComponent<CircleCollider2D>();
            transform               = GetComponent<Transform>();
        }

        public void Init(ItemStack itemStack, float range = 0.1f)
        {
            this.itemStack        = itemStack;
            collider.radius       = range;
            SpriteRenderer.sprite = this.itemStack.ItemInfo.SpriteInGame;
            this.itemStack.SetTransform(transform);
        }

        protected abstract SpriteRenderer SpriteRenderer { get; }
        public abstract    IArchitecture  GetArchitecture();
    }
}