/****************************************************************************
 * 2023.4 DESKTOP-IK7QDMM
 ****************************************************************************/

using System;
using System.Collections.Generic;
using Core.Inventory_And_Item.Data;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.UI
{
    public partial class UISlot : UIElement
    {
        private void Awake()
        {
            SetEmpty();
        }

        private void SetEmpty()
        {
            Number.SetText(string.Empty);
            Image.color = Color.clear;
        }

        protected override void OnBeforeDestroy()
        {
        }

        public void Refresh(ItemSlot itemSlot)
        {
            if (itemSlot == null)
                throw new ArgumentNullException(nameof(itemSlot));
            ItemStack stack = itemSlot.ItemStack;
            if (ItemStack.IsEmpty(stack))
            {
                SetEmpty();
                return;
            }

            Number.SetText(stack.Number > 1 ? stack.Number.ToString() : string.Empty);
            Image.color = Color.white;
            Image.sprite = stack.ItemIdentification.SpriteIcon;
        }
    }
}