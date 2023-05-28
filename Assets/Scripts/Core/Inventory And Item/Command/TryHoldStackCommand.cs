using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI;
using QFramework;

namespace InventoryAndItem.Core.Inventory_And_Item.Command
{
    public class TryHoldStackCommand : AbstractCommand
    {
        public ItemStackUI stackUI;
        public TryHoldStackCommand(ItemStackUI itemStackUI)
        {
            stackUI = itemStackUI;
        }

        protected override void OnExecute()
        {
            PointerStack.Instance.Create(stackUI.itemStack);
            stackUI.OnRemoved?.Invoke();
        }
    }
}