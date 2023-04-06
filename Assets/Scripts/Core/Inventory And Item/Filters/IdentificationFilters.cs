namespace Core.Inventory_And_Item.Filters
{
    public static class IdentificationFilters
    {
        public static readonly IdentificationFilter All = new IdentificationFilter(_ => true);

        public static readonly IdentificationFilter None = new IdentificationFilter(_ => false);
    }
}