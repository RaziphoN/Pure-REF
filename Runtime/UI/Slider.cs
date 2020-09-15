using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Slider")]
	public class Slider : UnityEngine.UI.Slider
	{
		[SerializeField] private SliderStyleObject style;

		protected override void Awake()
		{
			base.Awake();
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
