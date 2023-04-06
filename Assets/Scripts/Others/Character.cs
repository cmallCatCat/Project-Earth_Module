using System;
using Core.Architectures;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using Core.Save_And_Load;
using Core.Save_And_Load.Interfaces;
using Framework;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Vector4 = System.Numerics.Vector4;

namespace Others
{
    public class Character : MonoBehaviour, IController, ISave
    {
        public Inventory inventory;
        public int inventoryKey;

        private void Start()
        {
            // this.GetSystem<SaveAndLoadSystem>().Register(this, "CharacterData");
            inventory = new Inventory(10);
            inventoryKey = this.GetModel<InventoryModel>().Register(inventory);
        }

        private void OnDestroy()
        {
            // this.GetSystem<SaveAndLoadSystem>().Unregister(this, "CharacterData");
            this.GetModel<InventoryModel>().Unregister(inventoryKey);
        }

        public IArchitecture GetArchitecture()
        {
            return InGame.Interface;
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

        public object Save()
        {
            return new CharacterData(inventoryKey);
        }

        public void Load(object save)
        {
            ((JObject)save).ToObject<CharacterData>().Deconstruct(out inventoryKey);
        }
    }
}