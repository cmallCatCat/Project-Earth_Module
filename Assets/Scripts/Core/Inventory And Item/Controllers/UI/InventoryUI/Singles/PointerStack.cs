using Core.Root.Base;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Data.ItemInfos;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles
{
    [UsedImplicitly]
    public class PointerStack : Singleton<PointerStack>
    {
        private ItemStack itemStack;

        public bool itemOnHold => itemStack != null;

        private ResLoader resLoader = ResLoader.Allocate();

        private GameObject stackPrefab;

        public  Vector2     Size = new Vector2(60, 60);
        private GameObject  instantiate;
        private ItemStackUI itemStackUI;


        public ItemInfo ItemInfo => itemStack.ItemInfo;

        public ItemDecorator ItemDecorator => itemStack.ItemDecorator;

        public int Number => itemStack.Number;

        protected PointerStack() { }

        public ItemStackUI Create(ItemStack itemStack)
        {
            this.itemStack                 = new ItemStack(itemStack.ItemInfo, itemStack.ItemDecorator, itemStack.Number, null);
            instantiate                    = Object.Instantiate(stackPrefab, IEnvironment.Instance.UICanvas.transform);
            instantiate.transform.position = IEnvironment.Instance.FarAway;
            itemStackUI                    = instantiate.GetComponent<ItemStackUI>();
            itemStackUI.Init(this.itemStack, Size);
            instantiate.AddComponent<FollowPointer>();
            return itemStackUI;
        }

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            stackPrefab = resLoader.LoadSync<GameObject>("stack");
        }

        public override void Dispose()
        {
            base.Dispose();
            resLoader.Recycle2Cache();
            resLoader = null;
        }

        public void Remove(int toRemoveNumber)
        {
            itemStack.Remove(toRemoveNumber);
            Refresh();
        }

        public void RemoveAll()
        {
            itemStack.Remove(Number);
            Refresh();
        }

        public ItemStack Clone()
        {
            return itemStack.Clone();
        }

        private void Refresh()
        {
            if (itemStack == null) return;

            itemStackUI.Refresh(itemStack, Size);

            if (Number == 0)
            {
                itemStack = null;
                Object.Destroy(instantiate);
            }
        }
    }
}