using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "InputFieldStyle", menuName = "REF/UI/Style/InputField Style")]
	public class InputFieldStyleObject : StyleObject<UnityEngine.UI.InputField>
	{
		[SerializeField] private InputFieldStyle style;

		public override void Apply(UnityEngine.UI.InputField element)
		{
			style.Apply(element);
		}
	}
}
