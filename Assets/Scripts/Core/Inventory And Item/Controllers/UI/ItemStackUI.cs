using System.Collections.Generic;
using Core.QFramework.Framework.Scripts;
using InventoryAndItem.Core.Inventory_And_Item.Command;
using InventoryAndItem.Core.Inventory_And_Item.Data;
using InventoryAndItem.Core.Inventory_And_Item.Events;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI
{
    public abstract class ItemStackUI : MonoBehaviour, IController, IPointerClickHandler
    {
        [Header("UI")]
        public Image stackImage;

        public Image icon;
        public TMP_Text count;
        
        public ItemStack itemStack;
        
        public delegate void Removed();

        public Removed OnRemoved;


        public void OnPointerClick(PointerEventData eventData)
        {
            if (PointerStack.Instance.itemOnHold)
            {
                this.SendCommand(new TryDropStackOnStackCommand(this));
            }
            else
            {
                this.SendCommand(new TryHoldStackCommand(this));
            }
        }

        public void Init(ItemStack itemSlotItemStack, Vector2 sizeDelta)
        {
            itemStack = itemSlotItemStack;
            GetComponent<RectTransform>().sizeDelta = sizeDelta;
            icon.sprite = itemSlotItemStack.ItemInfo.SpriteIcon;
            count.text = itemSlotItemStack.Number.ToString();
        }

        public abstract IArchitecture GetArchitecture();
    }
}