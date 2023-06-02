using Core.Architectures;
using Core.Root.Expand;
using InventoryAndItem.Core.Inventory_And_Item.Controllers;
using QFramework;

namespace Core
{
    public class ItemGravitaterExample : ItemGravitater
    {
        protected override InventoryHolder GetInventoryHolder => transform.GetComponentInCounterparts<InventoryHolder>();

        public override IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}