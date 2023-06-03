using System;
using System.Collections;
using Core;
using Core.Architectures;
using Core.Inventory_And_Item.Controllers;
using Core.Inventory_And_Item.Data;
using Core.Inventory_And_Item.Data.ItemInfos;
using Core.Root.Base;
using Core.Root.Utilities;
using QAssetBundle;
using QFramework;
using UI;
using UnityEngine;

namespace Tests
{
    public class SampleTest : MonoBehaviour, IController
    {
        private ResLoader resLoader = ResLoader.Allocate();

        public ItemInfo itemInfo;

        private void Awake()
        {
            ResKit.Init();
            InputReader.Init();

            GameObject pickerPrefab = resLoader.LoadSync<GameObject>(Itempickerprefab_prefab.ITEMPICKERPREFAB);
            GameObject instantiate
                = IEnvironment.Instance.Instantiate(pickerPrefab, false, new Vector3(10, 0, 0), Quaternion.identity);
            instantiate.GetComponent<ItemPicker>().Init(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));

            // TimeScaleModifier timeScaleModifier= new TimeScaleModifier("Test", 0.1f);
            // TimeScaleManager.Instance.AddModifier(timeScaleModifier);
            // TimeScaleManager.Instance.RemoveModifier(timeScaleModifier);
        }

        TimeScaleModifier timeScaleModifier = new TimeScaleModifier("Test", 0.1f);


        private void OnGUI()
        {
            if (GUILayout.Button("创建"))
            {
                if (IEnvironment.Player != null) throw new Exception("Player is already created");

                GameObject loadSync = resLoader.LoadSync<GameObject>(Player_prefab.PLAYER);
                GameObject instantiate
                    = IEnvironment.Instance.Instantiate(loadSync, false, new Vector3(0, 0, 0), Quaternion.identity);
                instantiate.GetComponentInChildren<ItemGravitater>().Init(5, 40);
            }

            if (GUILayout.Button("添加"))
                IEnvironment.Player.GetComponentInChildren<InventoryHolderExample>().Inventory
                    .Add(new ItemStack(itemInfo, new ItemDecorator(), 1, transform));

            if (GUILayout.Button("慢镜头"))
            {
                StartCoroutine(TimeScaleManager.Instance.ApplySlowMotion(timeScaleModifier));
            }
            if (GUILayout.Button("慢镜头移除"))
            {
                StartCoroutine(TimeScaleManager.Instance.RemoveSlowMotion(timeScaleModifier));
            }
        }

        private void OnDestroy()
        {
            resLoader.Recycle2Cache();
            resLoader.Dispose();
        }

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}