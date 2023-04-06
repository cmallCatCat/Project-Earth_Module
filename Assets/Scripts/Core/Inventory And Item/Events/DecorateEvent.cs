using Core.Inventory_And_Item.Data;

namespace Core.Inventory_And_Item.Events
{
    public class DecorateEvent
    {
        public readonly Decoration Decoration;
        public readonly ItemStack Stack;

        public DecorateEvent(ItemStack stack, Decoration decoration)
        {
            Stack = stack;
            Decoration = decoration;
        }
    }
}