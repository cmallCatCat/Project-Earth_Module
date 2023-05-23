using System;
using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemIdentifications;
using QFramework;
using UI.InGame;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShortcutInventoryTest : MonoBehaviour, IController
    {
        private ResLoader resLoader = ResLoader.Allocate();
        public ItemIdentification itemIdentification;

        private void Awake()
        {
            ResKit.Init();
            UIKit.CloseAllPanel();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("创建"))
            {
                if (SceneEnvironment.Instance.Player != null)
                {
                    throw new Exception("Player is already created");
                }

                GameObject loadSync = resLoader.LoadSync<GameObject>(QAssetBundle.Player_prefab.PLAYER);
                GameObject instantiate = Instantiate(loadSync);
                UIKit.OpenPanel<ShortcutInventory>(new ShortcutInventoryData(
                    instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory, 12, 12, 1)
                );
                instantiate.name = "Player";
                instantiate.GetComponentInChildren<InventoryHolderExample>().Inventory
                    .Add(new ItemStack(itemIdentification, new ItemDecorator(), 1));
            }
        }

        public IArchitecture GetArchitecture()
        {
            return InGame.Interface;
        }
    }
}