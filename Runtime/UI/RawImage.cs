using UnityEngine;

using REF.Runtime.UI.Style.Graphic;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Raw Image")]
	public class RawImage : UnityEngine.UI.RawImage
	{
		[SerializeField] private RawImageStyleObject style;

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
