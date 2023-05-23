using System;
using Core;
using Core.Inventory_And_Item.Data;
using MeadowGames.MakeItFlow;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace UI.InGame
{
    public class ShortcutItemStack : MonoBehaviour, IController, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [Header("UI")]
        public Image stackImage;

        public Image icon;
        public TMP_Text count;

        [HideInInspector]
        public Transform parentAfterDrag;

        public int index;

        private void Awake()
        {
            parentAfterDrag = transform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            stackImage.raycastTarget = false;
            parentAfterDrag = transform.parent;
            GetComponent<TranslateToTargetBehavior>().TargetPosition = transform.parent;
            transform.SetParent(transform.parent.parent.parent.parent);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            stackImage.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
        }

        public IArchitecture GetArchitecture()
        {
            return Core.Architectures.InGame.Interface;
        }

        public void Init(ItemStack itemSlotItemStack, int i)
        {
            icon.sprite = itemSlotItemStack.ItemIdentification.SpriteIcon;
            count.text = itemSlotItemStack.Number.ToString();
            index = i;
        }
    }
}