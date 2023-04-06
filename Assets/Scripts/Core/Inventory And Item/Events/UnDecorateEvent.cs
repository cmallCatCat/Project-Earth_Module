using Core.Inventory_And_Item.Data;

namespace Core.Inventory_And_Item.Events
{
    public class UnDecorateEvent
    {
        public readonly Decoration Decoration;
        public readonly ItemStack Stack;

        public UnDecorateEvent(ItemStack stack, Decoration decoration)
        {
            Stack = stack;
            Decoration = decoration;
        }
    }
}