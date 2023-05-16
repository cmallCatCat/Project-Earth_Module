using Core.Inventory_And_Item.Data;

namespace UI.InGame
{
	// Generate Id:1cdc163e-fb66-480f-9265-ee1992ed6324
	public partial class InventoryPanel
	{
		public const string Name = "InventoryPanel";
		
		
		private InventoryPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public InventoryPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		InventoryPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new InventoryPanelData(new Inventory(0)));
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
