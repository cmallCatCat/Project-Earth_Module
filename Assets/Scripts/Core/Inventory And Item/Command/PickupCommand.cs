using QFramework;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers
{
    public class PickupCommand: AbstractCommand
    {
        private readonly ItemPicker          itemPicker;
        private readonly InventoryGravitater inventoryGravitater;


        public PickupCommand(ItemPicker itemPicker, InventoryGravitater inventoryGravitater)
        {
            this.itemPicker          = itemPicker;
            this.inventoryGravitater = inventoryGravitater;
        }

        protected override void OnExecute()
        {
            
        }
    }
}