using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "ToggleStyle", menuName = "REF/UI/Style/Toggle Style")]
	public class ToggleStyleObject : StyleObject<UnityEngine.UI.Toggle>
	{
		[SerializeField] private ToggleStyle style;

		public override void Apply(UnityEngine.UI.Toggle element)
		{
			style.Apply(element);
		}
	}
}
