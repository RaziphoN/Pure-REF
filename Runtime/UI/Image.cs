using UnityEngine;

using REF.Runtime.UI.Style.Graphic;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Image")]
	public class Image : UnityEngine.UI.Image
	{
		[SerializeField] private ImageStyleObject style;

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
