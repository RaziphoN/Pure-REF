using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "ButtonStyle", menuName = "REF/UI/Style/Button Style")]
	public class ButtonStyleObject : StyleObject<UnityEngine.UI.Button>
	{
		[SerializeField] private ButtonStyle style = new ButtonStyle();

		public override void Apply(UnityEngine.UI.Button element)
		{
			style.Apply(element);
		}

		public override void Copy(UnityEngine.UI.Button element)
		{
			style.Copy(element);
		}
	}
}
