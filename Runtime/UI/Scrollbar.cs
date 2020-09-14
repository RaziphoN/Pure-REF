using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Scrollbar")]
	public class Scrollbar : UnityEngine.UI.Scrollbar
	{
		[SerializeField] private ScrollbarStyleObject style;

		protected override void Awake()
		{
			base.Awake();
			style?.Apply(this);
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			style?.Apply(this);
		}
	}
}
