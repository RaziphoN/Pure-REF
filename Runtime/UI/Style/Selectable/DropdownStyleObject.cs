using UnityEngine;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "DropdownStyle", menuName = "REF/UI/Style/Dropdown Style")]
	public class DropdownStyleObject : StyleObject<UnityEngine.UI.Dropdown>
	{
		[SerializeField] private DropdownStyle style = new DropdownStyle();

		public override void Apply(UnityEngine.UI.Dropdown element)
		{
			style.Apply(element);
		}

		public override void Copy(UnityEngine.UI.Dropdown element)
		{
			style.Copy(element);
		}
	}
}
