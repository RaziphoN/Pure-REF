using UnityEngine;

namespace REF.Runtime.UI.Style.Text
{
	[CreateAssetMenu(fileName = "TextStyle", menuName = "REF/UI/Style/Text Style")]
	public class TextStyleObject : StyleObject<UnityEngine.UI.Text>
	{
		[SerializeField] TextStyle style = new TextStyle();

		public override void Apply(UnityEngine.UI.Text element)
		{
			style.Apply(element);
		}

		public override void Copy(UnityEngine.UI.Text element)
		{
			style.Copy(element);
		}
	}
}
