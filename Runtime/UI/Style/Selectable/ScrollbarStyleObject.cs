using UnityEngine;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "ScrollbarStyle", menuName = "REF/UI/Style/Scrollbar Style")]
	public class ScrollbarStyleObject : StyleObject<UnityEngine.UI.Scrollbar>
	{
		[SerializeField] private ScrollbarStyle style = new ScrollbarStyle();

		public override void Apply(UnityEngine.UI.Scrollbar element)
		{
			style.Apply(element);
		}

		public override void Copy(UnityEngine.UI.Scrollbar element)
		{
			style.Copy(element);
		}
	}
}
