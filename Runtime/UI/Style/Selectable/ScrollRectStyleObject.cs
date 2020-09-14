using UnityEngine;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "ScrollRectStyle", menuName = "REF/UI/Style/ScrollRect Style")]
	public class ScrollRectStyleObject : StyleObject<UnityEngine.UI.ScrollRect>
	{
		[SerializeField] private ScrollRectStyle style;

		public override void Apply(UnityEngine.UI.ScrollRect element)
		{
			style.Apply(element);
		}
	}
}
