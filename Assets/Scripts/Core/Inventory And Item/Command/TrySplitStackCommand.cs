using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Command
{
    public class TrySplitStackCommand : AbstractCommand
    {
        private readonly ItemSlotUI itemSlotUI;

        public TrySplitStackCommand(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
        }

        protected override void OnExecute()
        {
            ItemStack stack = itemSlotUI.ItemSlot.ItemStack;
            if (stack != null)
            {
                ItemStack removed = new ItemStack(stack.ItemInfo, stack.ItemDecorator,
                    Mathf.CeilToInt(stack.Number / 2.0f), stack.Transform);
                itemSlotUI.Inventory.Remove(removed, itemSlotUI.InventoryIndex);
                PointerStack.Instance.CreateOrAdd(removed);
            }
        }
    }
}