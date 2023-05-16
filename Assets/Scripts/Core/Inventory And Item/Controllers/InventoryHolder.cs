using System;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using QFramework;
using UnityEngine;

namespace Core.Inventory_And_Item.Controllers
{
    public abstract class InventoryHolder : MonoBehaviour, IController
    {
        [SerializeField] private int inventoryCapacity;
        [SerializeField] private bool isPlayer;
        [SerializeField] private Inventory inventory; // TODO: 正式运行时删除SerializeField

        public Inventory Inventory => inventory;

        protected void Awake()
        {
            RegisterInventory(GetEnvironment());
        }

        protected void RegisterInventory(IEnvironment environment)   
        {
            inventory = new Inventory(inventoryCapacity);
            environment.Register<Inventory>(this);
        }

        protected void UnregisterInventory(IEnvironment environment)
        {
            environment.Unregister<Inventory>(this);
            inventory = null;
        }

        protected virtual void OnDestroy()
        {
            UnregisterInventory(GetEnvironment());
        }

        protected abstract IEnvironment GetEnvironment(); 
            
        public abstract IArchitecture GetArchitecture();
    }
}