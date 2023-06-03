using Core.Architectures;
using Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace Others
{
    public class Character : MonoBehaviour, IController
    {
        public Inventory inventory;
        public int       inventoryKey;

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }


        public struct CharacterData
        {
            public readonly int inventoryKey;

            public CharacterData(int inventoryKey)
            {
                this.inventoryKey = inventoryKey;
            }

            public void Deconstruct(out int inventoryKey)
            {
                inventoryKey = this.inventoryKey;
            }
        }
    }
}