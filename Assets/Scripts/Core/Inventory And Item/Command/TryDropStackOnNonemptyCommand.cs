#nullable enable
using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using Core.Inventory_And_Item.Data;
using QFramework;

namespace Core.Inventory_And_Item.Command
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
            PointerStack toDrop    = PointerStack.Instance;
            Inventory    inventory = itemSlotUI.Inventory;
            int          index     = itemSlotUI.InventoryIndex;
            ItemSlot     itemSlot  = inventory.GetSlot(index);
            ItemStack    clone     = toDrop.Clone();

            int finalAddNumber = itemSlot.CanAddNumberFinal(toDrop.Clone());
            if (finalAddNumber > 0)
            {
                inventory.Add(new ItemStack(toDrop.ItemInfo, toDrop.ItemDecorator, finalAddNumber, null), index);
                toDrop.Remove(finalAddNumber);
                return;
            }

            ItemStack swapStack = itemSlot.ItemStack!.Clone();
            inventory.Remove(index);
            inventory.Add(clone, index);
            toDrop.RemoveAll();
            toDrop.CreateOrAdd(swapStack);
        }
    }
}