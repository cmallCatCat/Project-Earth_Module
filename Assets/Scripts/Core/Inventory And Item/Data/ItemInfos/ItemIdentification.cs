namespace Core.Inventory_And_Item.Data.ItemInfos
{
    public class ItemIdentification
    {
        public readonly string packageName;
        public readonly string name;

        public ItemIdentification(string packageName, string name)
        {
            this.packageName = packageName;
            this.name        = name;
        }
    }
}