using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "ToggleStyle", menuName = "REF/UI/Style/Toggle Style")]
	public class ToggleStyleObject : StyleObject<UnityEngine.UI.Toggle>
	{
		[SerializeField] private ToggleStyle style = new ToggleStyle();

		public override void Apply(UnityEngine.UI.Toggle element)
		{
			style.Apply(element);
		}

		public override void Copy(UnityEngine.UI.Toggle element)
		{
			style.Copy(element);
		}
	}
}
