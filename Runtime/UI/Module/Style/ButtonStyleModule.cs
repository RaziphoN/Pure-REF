using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI.Module.Style
{
	[RequireComponent(typeof(UnityEngine.UI.Button))]
	public class ButtonStyleModule : StyleModule<UnityEngine.UI.Button, ButtonStyleObject>, IButtonModule
	{
		public void OnClick()
		{
			
		}
	}
}
