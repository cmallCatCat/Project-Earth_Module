using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemIdentifications;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public abstract class ItemPicker : MonoBehaviour, IController
    {
        public ItemStack ItemStack;
        public ItemIdentification itemIdentification;
        public ItemDecorator itemDecorator = new ItemDecorator();
        public int number;

        private void Awake()
        {
            if (number > 0)
            {
                ItemStack = new ItemStack(itemIdentification, itemDecorator, number, transform);
            }
        }

        private void Start()
        {
            SpriteRenderer.sprite = ItemStack.ItemIdentification.SpriteInGame;
        }

        public void SetStack(ItemStack itemStack)
        {
            ItemStack = itemStack;
        }

        protected abstract SpriteRenderer SpriteRenderer { get; }

        public abstract IArchitecture GetArchitecture();
    }
}