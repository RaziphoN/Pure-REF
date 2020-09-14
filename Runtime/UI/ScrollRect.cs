using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Scroll Rect")]
	[SelectionBase]
	public class ScrollRect : UnityEngine.UI.ScrollRect
	{
		[SerializeField] private ScrollRectStyleObject style;

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
