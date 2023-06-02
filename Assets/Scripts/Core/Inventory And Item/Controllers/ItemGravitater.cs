using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public abstract class ItemGravitater : MonoBehaviour, IController
    {
        private float range;
        private float time;

        private InventoryHolder  inventoryHolder;
        private CircleCollider2D circleCollider2D;


        public float Range => range;
        public float Time  => time;


        private void Awake()
        {
            circleCollider2D           = gameObject.GetOrAddComponent<CircleCollider2D>();
            circleCollider2D.isTrigger = true;
            SetValue(range, time);
        }

        private void SetValue(float range = 5f, float time = 0.4f)
        {
            this.range              = range;
            this.time               = time;
            circleCollider2D.radius = Range;
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

        public abstract IArchitecture GetArchitecture();
    }
}