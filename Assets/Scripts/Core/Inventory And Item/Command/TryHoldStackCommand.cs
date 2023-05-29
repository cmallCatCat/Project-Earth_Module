using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using QFramework;

namespace InventoryAndItem.Core.Inventory_And_Item.Command
{
    public class TryHoldStackCommand : AbstractCommand
    {
        private readonly ItemSlotUI itemSlotUI;
        public TryHoldStackCommand(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
        }

        protected override void OnExecute()
        {
            PointerStack.Instance.Create(itemSlotUI.ItemSlot.ItemStack);
            itemSlotUI.Inventory.Remove(itemSlotUI.InventoryIndex);
        }
    }
}