namespace InventoryAndItem.Core.Inventory_And_Item.Controllers.UI
{
	// Generate Id:1276c8e2-e54a-43ed-bc72-f138358eb0f3
	public abstract partial class InventoryUIPanel
	{
		public const string Name = "InventoryUIPanel";
		
		
		private InventoryUIPanelData mPrivateUIPanelData = null;
		
		protected override void ClearUIComponents()
		{
			
			mUIPanelData = null;
		}
		
		public InventoryUIPanelData UIPanelData
		{
			get
			{
				return mUIPanelData;
			}
		}
		
		InventoryUIPanelData mUIPanelData
		{
			get
			{
				return mPrivateUIPanelData ?? throw new System.InvalidOperationException("UIPanelData is null");
			}
			set
			{
				mUIData = value;
				mPrivateUIPanelData = value;
			}
		}
	}
}
