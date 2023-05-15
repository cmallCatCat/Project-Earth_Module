/****************************************************************************
 * 2023.4 DESKTOP-IK7QDMM
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.UI
{
	public partial class UISlot
	{
		[SerializeField] public UnityEngine.UI.Image Image;
		[SerializeField] public TMPro.TextMeshProUGUI Number;

		public void Clear()
		{
			Image = null;
			Number = null;
		}

		public override string ComponentName
		{
			get { return "UISlot";}
		}
	}
}
