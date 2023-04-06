using System;
using Core.Architectures;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Models;
using Core.Save_And_Load;
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

        [Serializable]
        public struct CharacterData
        {
            public System.Numerics.Vector3 position;
            public Vector4 rotation;
            public System.Numerics.Vector3 scale;
            public int inventoryKey;

            public CharacterData(Transform transform, int inventoryKey)
            {
                (System.Numerics.Vector3 vector3, Vector4 vector4, System.Numerics.Vector3 s) = transform.Save();
                position = vector3;
                rotation = vector4;
                scale = s;
                this.inventoryKey = inventoryKey;
            }

            public void Deconstruct(Transform transform, out int inventoryKey)
            {
                transform.Set(position, rotation, scale);
                inventoryKey = this.inventoryKey;
            }
        }

        public object Save()
        {
            return new CharacterData(transform, inventoryKey);
        }

        public void Load(object save)
        {
            ((JObject)save).ToObject<CharacterData>().Deconstruct(transform, out inventoryKey);
        }
    }
}