using UnityEngine;

using REF.Runtime.UI.Style.Selectable;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Input Field")]
	public class InputField : UnityEngine.UI.InputField
	{
		[SerializeField] private InputFieldStyleObject style;

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
