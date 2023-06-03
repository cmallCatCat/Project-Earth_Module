using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using Core.Inventory_And_Item.Data;
using QFramework;

namespace Core.Inventory_And_Item.Command
{
    public class TryHoldStackCommand : AbstractCommand
    {
        private readonly ItemSlotUI itemSlotUI;
        private readonly int        number;

        public TryHoldStackCommand(ItemSlotUI itemSlotUI, int number = -1)
        {
            this.itemSlotUI = itemSlotUI;
            this.number     = number;
        }
        

        protected override void OnExecute()
        {
            ItemStack stack = itemSlotUI.ItemSlot.ItemStack;
            ItemStack toGet = stack!.Clone(number == -1 ? stack.Number : number);
            
            PointerStack.Instance.CreateOrAdd(toGet);
            itemSlotUI.Inventory.Remove(toGet, itemSlotUI.InventoryIndex);
        }
    }
}