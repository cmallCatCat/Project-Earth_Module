using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using QFramework;

namespace Core.Inventory_And_Item.Command
{
    public class GetItemCommand: AbstractCommand
    {
        private readonly int inventoryKey;
        private readonly ItemStack itemStack;

        public GetItemCommand(int inventoryKey, ItemStack itemStack)
        {
            this.inventoryKey = inventoryKey;
            this.itemStack = itemStack;
        }

        protected override void OnExecute()
        {
            this.GetModel<InventoryModel>().Add(inventoryKey, itemStack);
        }
    }
}