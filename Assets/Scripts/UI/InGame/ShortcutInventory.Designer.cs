namespace UI.InGame
{
	// Generate Id:1276c8e2-e54a-43ed-bc72-f138358eb0f3
	public partial class ShortcutInventory
	{
		public const string Name = "ShortcutInventory";
		
		
		private ShortcutInventoryData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public ShortcutInventoryData Data
		{
			get
			{
				return mData;
			}
		}
		
		ShortcutInventoryData mData
		{
			get
			{
				return mPrivateData ?? throw new System.InvalidOperationException("Data is null");
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
