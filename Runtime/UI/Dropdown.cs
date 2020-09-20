using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Dropdown")]
	public class Dropdown : UnityEngine.UI.Dropdown
	{
		[SerializeField] private DropdownStyleObject style;

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
