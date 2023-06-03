using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using QFramework;

namespace Core.Inventory_And_Item.Command
{
    public class SelectItemCommand : AbstractCommand
    {
        private readonly ItemSlotUI itemSlotUI;

        public SelectItemCommand(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
        }

        protected override void OnExecute()
        {
            itemSlotUI.inventoryUIPanel.SelectedIndex = itemSlotUI.displayIndex;
        }
    }
}