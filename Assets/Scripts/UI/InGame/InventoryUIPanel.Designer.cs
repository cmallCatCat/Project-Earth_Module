namespace UI.InGame
{
	// Generate Id:1276c8e2-e54a-43ed-bc72-f138358eb0f3
	public partial class InventoryUIPanel
	{
		public const string Name = "InventoryUIPanel";
		
		
		private InventoryUIPanelData mPrivateUIPanelData = null;
		
		protected override void ClearUIComponents()
		{
			
			MUIPanelData = null;
		}
		
		public InventoryUIPanelData UIPanelData
		{
			get
			{
				return MUIPanelData;
			}
		}
		
		InventoryUIPanelData MUIPanelData
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
