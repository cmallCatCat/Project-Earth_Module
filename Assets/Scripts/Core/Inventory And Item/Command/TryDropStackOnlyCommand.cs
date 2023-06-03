#nullable enable
using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using Core.Inventory_And_Item.Data;
using QFramework;

namespace Core.Inventory_And_Item.Command
{
    public class TryDropStackOnlyCommand : AbstractCommand
    {
        private readonly ItemSlotUI itemSlotUI;
        private readonly int        number;

        public TryDropStackOnlyCommand(ItemSlotUI itemSlotUI, int number = -1)
        {
            this.itemSlotUI = itemSlotUI;
            this.number     = number;
        }

        protected override void OnExecute()
        {
            ItemStack target = PointerStack.Instance.Clone();
            ItemStack toDrop = target.Clone(number == -1 ? target.Number : number);

            int finalDropNumber = itemSlotUI.Inventory.CanAddNumberFinal(toDrop);
            if (finalDropNumber > 0)
            {
                itemSlotUI.Inventory.Add(toDrop.Clone(finalDropNumber), itemSlotUI.InventoryIndex);
                PointerStack.Instance.Remove(finalDropNumber);
            }
        }
    }
}