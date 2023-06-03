using InventoryAndItem.Core.Inventory_And_Item.Controllers;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;

namespace InventoryAndItem.Core.Inventory_And_Item.Command
{
    public class PickupCommand: AbstractCommand
    {
        private readonly ItemPicker          itemPicker;
        private readonly ItemGravitater itemGravitater;


        public PickupCommand(ItemPicker itemPicker, ItemGravitater itemGravitater)
        {
            this.itemPicker          = itemPicker;
            this.itemGravitater = itemGravitater;
        }

        protected override void OnExecute()
        {
            ItemStack toAdd             = itemPicker.itemStack;
            Inventory inventory         = itemGravitater.InventoryHolder.Inventory;
            int       numberFinal = inventory.CanAddNumberFinal(toAdd);
            inventory.Add(new ItemStack(toAdd.ItemInfo, toAdd.ItemDecorator, numberFinal, null));
            toAdd.Remove(numberFinal);
        }
    }
}