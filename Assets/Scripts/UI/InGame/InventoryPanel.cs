using Core.Inventory_And_Item.Data;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class InventoryPanelData : UIPanelData
    {
        public readonly Inventory inventory;
        public readonly int displayCapacity;

        public InventoryPanelData(Inventory inventory, int displayCapacity)
        {
            this.inventory = inventory;
            this.displayCapacity = displayCapacity;
        }
    }

    public partial class InventoryPanel : UIPanel
    {
        private Inventory inventory;

        [SerializeField]
        private Transform slotsParent;

        [SerializeField]
        private GameObject slotPrefab;

        private int displayCapacity;
        private Image[] slotImages;
        private TMP_Text[] slotTexts;

        private void Refresh()
        {
            int minLength = slotImages.Length;
            if (slotImages.Length > inventory.AllSlots().Length)
            {
                Debug.LogWarning("the displayCapacity of inventory is not enough to display all slots: " +
                                 "slotImages.Length = " + slotImages.Length + ", inventory.AllSlots().Length = " + inventory.AllSlots().Length);
                minLength = inventory.AllSlots().Length;
            }

            for (int index = 0; index < minLength; index++)
            {
                ItemStack itemStack = inventory.AllSlots()[index].ItemStack;
                slotImages[index].sprite = itemStack?.ItemIdentification.SpriteIcon;
                slotImages[index].color = itemStack != null ? Color.white : Color.clear;
                slotTexts[index].text = itemStack?.Number.ToString();
            }
        }

        protected override void OnInit(IUIData uiData = null)
        {
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            if (uiData == null)
            {
                uiData = new InventoryPanelData(new Inventory(0), 0);
                Debug.LogError("uiData is null");
            }

            mData = (InventoryPanelData)uiData;
            // please add init code here
            inventory = mData.inventory;
            displayCapacity = mData.displayCapacity;
            slotImages = new Image[displayCapacity];
            slotTexts = new TMP_Text[displayCapacity];
            for (int i = 0; i < displayCapacity; i++)
            {
                Transform instantiate = Instantiate(slotPrefab, slotsParent).transform;
                slotImages[i] = instantiate.GetChild(0).GetChild(0).GetComponent<Image>();
                slotTexts[i] = instantiate.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            }

            inventory.onItemChanged += Refresh;
            Refresh();
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
            inventory.onItemChanged -= Refresh;
        }
    }
}