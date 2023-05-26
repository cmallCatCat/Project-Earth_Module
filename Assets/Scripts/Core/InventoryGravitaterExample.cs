using Core.Architectures;
using InventoryAndItem.Core.Inventory_And_Item.Controllers;
using QFramework;

namespace Core
{
    public class InventoryGravitaterExample: InventoryGravitater
    {
        protected override InventoryHolder GetInventoryHolder=>transform.parent.GetComponent<InventoryHolder>();

        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}