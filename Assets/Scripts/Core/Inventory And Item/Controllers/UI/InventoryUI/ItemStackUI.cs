using System;
using InventoryAndItem.Core.Inventory_And_Item.Command;
using InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI.Singles;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI.InventoryUI
{
    public abstract class ItemStackUI : MonoBehaviour, IController
    {
        [Header("UI")]
        public Image stackImage;

        public Image icon;
        public TMP_Text count;

        public ItemSlotUI itemSlotUI;
        public ItemStack itemStack;
        public RectTransform RectTransform;


        public void Init(ItemStack itemStack, Vector2 sizeDelta)
        {
            Refresh(itemStack, sizeDelta);
        }

        public void Init(ItemSlotUI itemSlotUI)
        {
            this.itemSlotUI = itemSlotUI;
            Refresh();
        }

        public void Refresh()
        {
            itemStack = itemSlotUI.ItemSlot.ItemStack;
            RectTransform.sizeDelta = itemSlotUI.RectTransform.sizeDelta;
            icon.sprite = itemStack!.ItemInfo.SpriteIcon;
            count.text = itemStack!.Number.ToString();
        }

        public void Refresh(ItemStack itemStack, Vector2 sizeDelta)
        {
            this.itemStack = itemStack;
            RectTransform.sizeDelta = sizeDelta;
            icon.sprite = itemStack.ItemInfo.SpriteIcon;
            count.text = itemStack.Number.ToString();
        }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public abstract IArchitecture GetArchitecture();
    }
}