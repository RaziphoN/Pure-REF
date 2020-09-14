using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Button")]
	public class Button : UnityEngine.UI.Button
	{
		[SerializeField] private ButtonStyleObject style;

		protected override void Awake()
		{
			base.Awake();
			style?.Apply(this);
		}

		protected override void OnValidate()
		{
			base.OnValidate();

			if (style != null)
			{
				style.Apply(this);
			}
		}
	}
}
