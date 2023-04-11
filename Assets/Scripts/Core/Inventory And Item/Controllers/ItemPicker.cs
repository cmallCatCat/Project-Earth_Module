using System;
using Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    public abstract class ItemPicker: MonoBehaviour, IController
    {
        private ItemStack itemStack;
        public ItemStack ItemStack => itemStack;

        private void Awake()
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.AddComponent<CircleCollider2D>();
        }

        public abstract IArchitecture GetArchitecture();
    }
}