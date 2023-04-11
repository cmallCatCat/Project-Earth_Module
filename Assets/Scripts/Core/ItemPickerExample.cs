using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using QFramework;

namespace Core
{
    public class ItemPickerExample:ItemPicker
    {
        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}