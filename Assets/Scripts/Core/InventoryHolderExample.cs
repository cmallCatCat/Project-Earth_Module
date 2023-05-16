using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using QFramework;

namespace Core
{
    public class InventoryHolderExample: InventoryHolder
    {
        protected override IEnvironment GetEnvironment()
        {
            return SceneEnvironment.Instance;
        }

        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}