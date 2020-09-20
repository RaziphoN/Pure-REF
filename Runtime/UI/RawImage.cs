using UnityEngine;

using REF.Runtime.UI.Style.Graphic;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Raw Image")]
	public class RawImage : UnityEngine.UI.RawImage
	{
		[SerializeField] private RawImageStyleObject style;

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
