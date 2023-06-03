using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemInfos;
using Core.Root.Base;
using JetBrains.Annotations;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles
{
    [UsedImplicitly]
    public class PointerStack : Singleton<PointerStack>
    {
        private ItemStack hold;

        public bool ItemOnHold => hold != null;

        private ResLoader resLoader = ResLoader.Allocate();

        private GameObject stackPrefab;

        public  Vector2     Size = new Vector2(60, 60);
        private GameObject  instantiate;
        private ItemStackUI itemStackUI;


        public ItemInfo ItemInfo => hold.ItemInfo;

        public ItemDecorator ItemDecorator => hold.ItemDecorator;

        public int Number => hold.Number;

        protected PointerStack() { }

        public void CreateOrAdd(ItemStack itemStack)
        {
            if (itemStack.Number==0)
            {
                return;
            }
            if (hold == null)
            {
                Create(itemStack);
            }
            else
            {
                Add(itemStack);
            }
        }

        private void Create(ItemStack itemStack)
        {
            hold                           = new ItemStack(itemStack.ItemInfo, itemStack.ItemDecorator, itemStack.Number, IEnvironment.Player.transform);
            instantiate                    = Object.Instantiate(stackPrefab, IEnvironment.Canvas.transform);
            instantiate.transform.position = IEnvironment.Instance.FarAway;
            itemStackUI                    = instantiate.GetComponent<ItemStackUI>();
            itemStackUI.Init(hold, Size);
            instantiate.AddComponent<FollowPointer>();
        }

        public bool CanAdd(ItemStack itemStack)
        {
            if (hold == null) return true;
            return hold.CanAddNumber(itemStack.ItemInfo, itemStack.ItemDecorator) > 0;
        }

        public void Add(ItemStack itemStack)
        {
            hold.Add(itemStack);
            Refresh();
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
            hold.Remove(toRemoveNumber);
            Refresh();
        }

        public void RemoveAll()
        {
            hold.Remove(Number);
            Refresh();
        }

        public ItemStack Clone(int number = -1)
        {
            return hold.Clone(number);
        }

        private void Refresh()
        {
            if (hold == null) return;

            itemStackUI.Refresh(hold, Size);

            if (Number == 0)
            {
                hold = null;
                Object.Destroy(instantiate);
            }
        }


    }
}