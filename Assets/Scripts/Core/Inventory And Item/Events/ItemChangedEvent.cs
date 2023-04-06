using Core.Inventory_And_Item.Data;

namespace Core.Inventory_And_Item.Events
{
    public struct ItemChangedEvent
    {
        public readonly int holder;
        public readonly ItemStack stack;
        public readonly ChangeType changeType;

        public ItemChangedEvent(int holder, ItemStack stack, ChangeType changeType)
        {
            this.holder = holder;
            this.stack = stack;
            this.changeType = changeType;
        }

        public enum ChangeType
        {
            Add,
            Remove,
            Move
        }
    }
}