using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Button")]
	public class Button : UnityEngine.UI.Button
	{
		[SerializeField] private ButtonStyleObject style;

		protected override void Start()
		{
			base.Start();
			style?.Apply(this);
		}

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();

			if (style != null)
			{
				style.Apply(this);
			}
		}
#endif
	}
}
