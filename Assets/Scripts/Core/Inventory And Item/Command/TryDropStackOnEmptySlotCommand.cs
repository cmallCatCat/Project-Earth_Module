#nullable enable
using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using Core.Inventory_And_Item.Data;
using QFramework;

namespace Core.Inventory_And_Item.Command
{
    public class TryDropStackOnEmptySlotCommand : AbstractCommand
    {
        public ItemSlotUI itemSlotUI;

        public TryDropStackOnEmptySlotCommand(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
        }

        protected override void OnExecute()
        {
            PointerStack toDrop = PointerStack.Instance;
            ItemStack    clone  = toDrop.Clone();

            int finalAddNumber = itemSlotUI.ItemSlot.CanAddNumberFinal(clone);
            if (finalAddNumber == 0) return;

            itemSlotUI.Inventory.Add(toDrop.Clone(finalAddNumber), itemSlotUI.InventoryIndex);
            toDrop.Remove(finalAddNumber);
        }
    }
}