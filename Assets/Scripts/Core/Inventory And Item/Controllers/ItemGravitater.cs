using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    public abstract class ItemGravitater : MonoBehaviour, IController
    {
        [SerializeField]
        private float range;
        [SerializeField]
        private int time;

        private InventoryHolder  inventoryHolder;
        private CircleCollider2D circleCollider2D;

        public float Range => range;
        public float T     => 1f / time;

        private void Awake()
        {
            circleCollider2D           = gameObject.GetOrAddComponent<CircleCollider2D>();
            circleCollider2D.isTrigger = true;
        }

        public void Init(float range = 5f, int time = 20)
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