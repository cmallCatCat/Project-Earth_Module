using Core;
using Core.Inventory_And_Item.Controllers.UI.InventoryUI;
using Core.Root.Base;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace UI
{
    public class GameUI : MonoSingleton<GameUI>
    {
        private const string BackpackUiPanel  = "InventoryUIPanel";
        private const string ShortcutUiPanel  = "ShortcutUIPanel";
        private const string EquipmentUiPanel = "EquipmentUIPanel";

        private ResLoader resLoader = ResLoader.Allocate();

        private void Awake()
        {
            ResKit.Init();
            UIController.Instance.CloseAllPanel();
            IEnvironment.Instance.MainCamera.GetUniversalAdditionalCameraData().cameraStack.Add(IEnvironment.Instance.UiCamera);
            InputReader.openBackpack += OpenBackpackAndOthers;
            InputReader.closeBackpack += CloseBackpackAndOthers;
        }

        private void OpenBackpackAndOthers()
        {
            OpenBackpackUI();
            OpenEquipmentUI();
            InputReader.InputPause();
            UIController.Instance.IsPaused = true;
        }
        
        private void CloseBackpackAndOthers()
        {
            CloseBackpackUI();
            CloseEquipmentUI();
            InputReader.InputResume();
            UIController.Instance.IsPaused = false;
        }
        

        public void OpenBackpackUI()
        {
            InventoryUISetting inventoryUISetting
                = resLoader.LoadSync<InventoryUISetting>(QAssetBundle.Backpack_inventoryuisetting_asset
                    .BACKPACK_INVENTORYUISETTING);
            GameObject instancePlayer = IEnvironment.Player;
            UIController.Instance.OpenPanel<InventoryUIPanel>(
                new InventoryUIPanelData(
                    instancePlayer.GetComponentInChildren<InventoryHolderExample>().Inventory,
                    inventoryUISetting
                ),
                BackpackUiPanel,
                PanelOpenType.Multiple
            );
        }

        public void CloseBackpackUI()
        {
            UIController.Instance.ClosePanel(BackpackUiPanel);
        }

        public void OpenShortcutUI()
        {
            InventoryUISetting inventoryUISetting
                = resLoader.LoadSync<InventoryUISetting>(QAssetBundle.Shoutcut_inventoryuisetting_asset
                    .SHOUTCUT_INVENTORYUISETTING);
            GameObject instancePlayer = IEnvironment.Player;
            UIController.Instance.OpenPanel<InventoryUIPanel>(
                new InventoryUIPanelData(
                    instancePlayer.GetComponentInChildren<InventoryHolderExample>().Inventory,
                    inventoryUISetting
                ),
                ShortcutUiPanel,
                PanelOpenType.Multiple
            );
        }

        public void CloseShortcutUI()
        {
            UIController.Instance.ClosePanel(ShortcutUiPanel);
        }

        public void OpenEquipmentUI()
        {
            InventoryUISetting equipmentUISetting
                = resLoader.LoadSync<InventoryUISetting>(QAssetBundle.Equipment_inventoryuisetting_asset
                    .EQUIPMENT_INVENTORYUISETTING);
            GameObject instancePlayer = IEnvironment.Player;
            UIController.Instance.OpenPanel<InventoryUIPanel>(
                new InventoryUIPanelData(
                    instancePlayer.GetComponentInChildren<EquipmentHolderExample>().Inventory,
                    equipmentUISetting
                ),
                EquipmentUiPanel,
                PanelOpenType.Multiple
            );
        }

        public void CloseEquipmentUI()
        {
            UIController.Instance.ClosePanel(EquipmentUiPanel);
        }

        public void OpenItemInfoUI(ItemSlotUI itemSlotUI)
        {
            Debug.Log("OpenItemInfoUI"+((RectTransform)itemSlotUI.transform).rect);
        }

        public void CloseItemInfoUI()
        {
            Debug.Log("CloseItemInfoUI");
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            resLoader.Recycle2Cache();
            resLoader = null;
        }
    }
}