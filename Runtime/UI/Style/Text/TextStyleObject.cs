using UnityEngine;

namespace REF.Runtime.UI.Style.Text
{
	[CreateAssetMenu(fileName = "TextStyle", menuName = "REF/UI/Style/Text Style")]
	public class TextStyleObject : StyleObject<UnityEngine.UI.Text>
	{
		[SerializeField] TextStyle style;

		public override void Apply(UnityEngine.UI.Text element)
		{
			style.Apply(element);
		}
	}
}
