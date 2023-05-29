#nullable enable
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Command
{
    public class TryDropStackOnNonemptyCommand : AbstractCommand
    {
        public readonly ItemSlotUI itemSlotUI;

        public TryDropStackOnNonemptyCommand(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
        }

        protected override void OnExecute()
        {
            PointerStack toDrop = PointerStack.Instance;
            Inventory inventory = itemSlotUI.Inventory;
            int index = itemSlotUI.InventoryIndex;
            ItemSlot itemSlot = inventory.GetSlot(index);
            ItemStack toDropStack_temp = new ItemStack(toDrop.ItemInfo, toDrop.ItemDecorator, toDrop.Number, null);

            if (!itemSlot.Match(toDropStack_temp))
            {
                return;
            }

            int canAddNumber = itemSlot.CanAddNumber(toDrop.ItemInfo, toDrop.ItemDecorator);
            int finalAddNumber = Mathf.Min(canAddNumber, toDrop.Number);
            if (finalAddNumber > 0)
            {
                inventory.Add(new ItemStack(toDrop.ItemInfo, toDrop.ItemDecorator, finalAddNumber, null), index);
                toDrop.Remove(finalAddNumber);
                return;
            }

            ItemStack swapStack = itemSlot.ItemStack!.Clone();
            inventory.Remove(index);
            inventory.Add(toDropStack_temp, index);
            toDrop.RemoveAll();
            toDrop.Create(swapStack);
            
            
        }
    }
}