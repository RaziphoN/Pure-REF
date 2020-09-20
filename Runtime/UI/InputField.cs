using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Input Field")]
	public class InputField : UnityEngine.UI.InputField
	{
		[SerializeField] private InputFieldStyleObject style;

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
