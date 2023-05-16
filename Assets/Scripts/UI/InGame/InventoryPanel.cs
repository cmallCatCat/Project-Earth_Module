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

        public InventoryPanelData(Inventory inventory)
        {
            this.inventory = inventory;
        }
    }
	public partial class InventoryPanel : UIPanel
	{
        private Inventory inventory;

        [SerializeField]
        private Transform slotsParent;

        private Image[] slotImages;
        private TMP_Text[] slotTexts;

        private void Refresh()
        {
            int minLength = slotImages.Length;
            if (slotImages.Length>inventory.AllSlots().Length)
            {
                Debug.LogWarning("the capacity of inventory is not enough to display all slots");
                minLength = inventory.AllSlots().Length;
            }
            for (int index = 0; index < minLength; index++)
            {
                slotImages[index].sprite = inventory.AllSlots()[index].ItemStack?.ItemIdentification.SpriteIcon;
                slotTexts[index].text = inventory.AllSlots()[index].ItemStack?.Number.ToString();
            }
        }

        protected override void OnInit(IUIData uiData = null)
		{
        }

        protected override void OnOpen(IUIData uiData = null)
		{
            if (uiData==null)
            {
                uiData = new InventoryPanelData(new Inventory(0));
                Debug.LogError("uiData is null");
            }
            mData = (InventoryPanelData)uiData;
            // please add init code here
            inventory = mData.inventory;
            slotImages=new Image[slotsParent.childCount];
            slotTexts=new TMP_Text[slotsParent.childCount];
            for (int i = 0; i < slotImages.Length; i++)
            {
                slotImages[i] = slotsParent.GetChild(i).GetComponentInChildren<Image>();
                slotTexts[i] = slotsParent.GetChild(i).GetComponentInChildren<TMP_Text>();
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
