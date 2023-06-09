﻿using Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryHolder : MonoBehaviour, IController
    {
        [SerializeField]
        private int inventoryCapacity;

        [field: SerializeField]
        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            Inventory = new Inventory(inventoryCapacity, transform);
        }

        public abstract IArchitecture GetArchitecture();
    }
}