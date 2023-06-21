using AYellowpaper.SerializedCollections;
using Core.Inventory_And_Item.Data;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Inventory_And_Item.Controllers.UI.InventoryUI
{
	public class InventoryUIPanelData : UIPanelData
	{
		public readonly Inventory          inventory;
		public readonly InventoryUISetting inventoryUISetting;


		public InventoryUIPanelData(Inventory inventory, InventoryUISetting inventoryUISetting)
		{
			this.inventory          = inventory;
			this.inventoryUISetting = inventoryUISetting;
		}
	}

	public abstract partial class InventoryUIPanel : UIPanel, IController
	{
		private  Inventory    inventory;
		private ItemSlotUI[] itemSlots;

		[SerializeField]
		private Transform slotsParent;

		[SerializeField]
		private GameObject slotPrefab;

		public InventoryUISetting inventoryUISetting;

		private int selectedIndex;
		
		public Inventory Inventory => inventory;

		private int Length => inventory.Slots.Count;

		public bool SelectableNow => inventoryUISetting.selectable && !UIController.Instance.IsPaused;

		public int SelectedIndex
		{
			get => selectedIndex;
			set
			{
				selectedIndex = value;
				Refresh();
			}
		}


		protected override void OnOpen(IUIData uiData = null)
		{
			if (uiData == null)
			{
				inventory = new Inventory(20, transform);
				uiData    = new InventoryUIPanelData(inventory, InventoryUISetting.Create());
				Debug.LogError("uiData is null");
			}

			mUIPanelData       = (InventoryUIPanelData)uiData;
			inventory          = mUIPanelData.inventory;
			inventoryUISetting = mUIPanelData.inventoryUISetting;

			inventory.onItemChanged += Refresh;
			SetLayout();
			Refresh();
		}

		private void SetLayout()
		{
			//读取
			inventoryUISetting.Deconstruct(
				out int startIndex,
				out bool selectable,
				out int displayCapacity,
				out int maxX,
				out int maxY,
				out RectOffset padding,
				out Vector2 spacing,
				out Vector2 slotSize,
				out Vector2 anchoredPosition,
				out Vector2 anchor,
				out Vector2 pivot,
				out Sprite panelBackground,
				out Sprite slotDefaultBackground,
				out Sprite slotSelectedBackground,
				out SerializedDictionary<int, Sprite> slotBackground);

			GridLayoutGroup gridLayoutGroup         = slotsParent.GetComponent<GridLayoutGroup>();
			RectTransform   slotParentRectTransform = slotsParent.GetComponent<RectTransform>();
			Image           image                   = slotsParent.GetComponent<Image>();

			//计算
			displayCapacity = Mathf.Min(displayCapacity, maxX * maxY, Length - startIndex);
			int xNum = Mathf.Min(maxX, displayCapacity);
			int yNum = Mathf.Min(maxY, Mathf.CeilToInt(displayCapacity * 1.0f / xNum));
			Vector2 sizeDelta = new Vector2(xNum * slotSize.x + (xNum - 1) * spacing.x + padding.left + padding.right,
				yNum * slotSize.y + (yNum - 1) * spacing.y + padding.top + padding.bottom);

			//创建
			itemSlots = new ItemSlotUI[displayCapacity];
			for (int i = 0; i < displayCapacity; i++)
			{
				Transform instantiate = Instantiate(slotPrefab, slotsParent).transform;
				itemSlots[i] = instantiate.GetComponent<ItemSlotUI>().Init(this, i);
			}

			//赋值
			gridLayoutGroup.padding.top              = padding.top;
			gridLayoutGroup.padding.bottom           = padding.bottom;
			gridLayoutGroup.padding.left             = padding.left;
			gridLayoutGroup.padding.right            = padding.right;
			gridLayoutGroup.spacing                  = spacing;
			gridLayoutGroup.cellSize                 = slotSize;
			slotParentRectTransform.sizeDelta        = sizeDelta;
			slotParentRectTransform.anchoredPosition = anchoredPosition;
			slotParentRectTransform.anchorMin        = anchor;
			slotParentRectTransform.anchorMax        = anchor;
			slotParentRectTransform.pivot            = pivot;

			image.sprite = panelBackground;
			image.type   = Image.Type.Sliced;

			selectedIndex = selectable ? 0 : -1;

			//刷新
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)slotsParent);
		}

		[ContextMenu("Refresh")]
		private void Refresh()
		{
			int minLength = itemSlots.Length;
			if (itemSlots.Length > Length)
			{
				Debug.LogWarning(
					$"the displayCapacity of inventory is not enough to display all slots: slotImages.Length = {itemSlots.Length}, inventory.AllSlots().Length = {Length}");
				minLength = Length;
			}

			for (int index = 0; index < minLength; index++) itemSlots[index].Refresh();
		}

		protected override void OnInit(IUIData uiData = null) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		public bool IsSelected(int displayIndex)
		{
			return displayIndex == SelectedIndex;
		}

		protected override void OnClose()
		{
			inventory.onItemChanged -= Refresh;
		}

		public abstract IArchitecture GetArchitecture();
	}
}