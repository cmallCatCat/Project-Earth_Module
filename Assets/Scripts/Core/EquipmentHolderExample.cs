using Core.Architectures;
using Core.Inventory_And_Item.Data;
using QFramework;

namespace Core
{
    public class EquipmentHolderExample : EquipmentHolder
    {
        public override IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}