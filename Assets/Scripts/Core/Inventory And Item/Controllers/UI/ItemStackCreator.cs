using InventoryAndItem.Core.Inventory_And_Item.Data;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI
{
    [UsedImplicitly]
    public class ItemStackCreator : Singleton<ItemStackCreator>
    {
        private ResLoader resLoader = ResLoader.Allocate();
        private GameObject stackPrefab;

        protected ItemStackCreator()
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            resLoader.Recycle2Cache();
            resLoader = null;
        }

        public override void OnSingletonInit()
        {
            stackPrefab = resLoader.LoadSync<GameObject>("stack");
        }

        public ItemStackUI Create(ItemStack itemStack, Transform parent, Vector2 size)
        {
            GameObject instantiate = Object.Instantiate(stackPrefab, parent);
            ItemStackUI itemStackUI = instantiate.GetComponent<ItemStackUI>();
            itemStackUI.Init(itemStack, size);
            return itemStackUI;
        }
    }
}