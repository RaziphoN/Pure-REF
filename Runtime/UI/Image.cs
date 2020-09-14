using UnityEngine;

using REF.Runtime.UI.Style.Graphic;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Image")]
	public class Image : UnityEngine.UI.Image
	{
		[SerializeField] private ImageStyleObject style;

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
