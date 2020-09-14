using UnityEngine;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "ScrollbarStyle", menuName = "REF/UI/Style/Scrollbar Style")]
	public class ScrollbarStyleObject : StyleObject<UnityEngine.UI.Scrollbar>
	{
		[SerializeField] private ScrollbarStyle style;

		public override void Apply(UnityEngine.UI.Scrollbar element)
		{
			style.Apply(element);
		}
	}
}
