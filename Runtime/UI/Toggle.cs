using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Toggle")]
	public class Toggle : UnityEngine.UI.Toggle
	{
		[SerializeField] private ToggleStyleObject style;

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
