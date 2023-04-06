namespace Core.Inventory_And_Item.Filters
{
    public static class SlotFilters
    {
        public static readonly SlotFilter All = new SlotFilter(_ => true);
        public static readonly SlotFilter None = new SlotFilter(_ => false);
    }
}