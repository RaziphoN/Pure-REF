using UnityEngine;

using REF.Runtime.UI.Style.Text;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Text")]
	public class Text : UnityEngine.UI.Text
	{
		[SerializeField] private TextStyleObject style;

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
