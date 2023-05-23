using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using QFramework;
using UnityEngine;

namespace Core
{
    public class ItemPickerExample : ItemPicker
    {
        protected override SpriteRenderer SpriteRenderer => GetComponentInChildren<SpriteRenderer>();

        public override IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}