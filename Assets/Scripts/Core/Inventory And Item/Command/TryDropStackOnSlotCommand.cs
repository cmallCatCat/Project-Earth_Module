#nullable enable
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace InventoryAndItem.Core.Inventory_And_Item.Command
{
    public class TryDropStackOnSlotCommand : AbstractCommand
    {
        public ItemSlotUI itemSlotUI;

        public TryDropStackOnSlotCommand(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
        }

        protected override void OnExecute()
        {
            PointerStack toDrop = PointerStack.Instance;

            int canAddNumber = itemSlotUI.itemSlot.CanAddNumber(toDrop.ItemInfo, toDrop.ItemDecorator);
            int finalAddNumber = Mathf.Min(canAddNumber, toDrop.Number);
            if (canAddNumber == 0)
            {
                return;
            }

            itemSlotUI.inventory.Add(new ItemStack(toDrop.ItemInfo, toDrop.ItemDecorator, finalAddNumber, null),
                itemSlotUI.index);
            toDrop.Remove(finalAddNumber);
            
        }
    }
}