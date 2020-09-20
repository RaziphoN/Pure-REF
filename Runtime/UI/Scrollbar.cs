using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Scrollbar")]
	public class Scrollbar : UnityEngine.UI.Scrollbar
	{
		[SerializeField] private ScrollbarStyleObject style;

		protected override void Start()
		{
			base.Start();
			style?.Apply(this);
		}

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
			style?.Apply(this);
		}
#endif
	}
}
