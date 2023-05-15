using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.UI
{
	// Generate Id:3411c6fa-26b9-425b-bedc-8ade210c1bf8
	public partial class ItemBar
	{
		public const string Name = "ItemBar";
		
		[SerializeField]
		public UISlot[] Slots;

        private ItemBarData mPrivateData = null;
		
		protected override void ClearUIComponents()
        {
            for (var i = 0; i < Slots.Length; i++)
            {
                Slots[i] = null;
            }

            mData = null;
        }
		
		public ItemBarData Data
		{
			get
			{
				return mData;
			}
		}
		
		ItemBarData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ItemBarData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
