using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[CreateAssetMenu(fileName = "SliderStyle", menuName = "REF/UI/Style/Slider Style")]
	public class SliderStyleObject : StyleObject<UnityEngine.UI.Slider>
	{
		[SerializeField] private SliderStyle style;

		public override void Apply(UnityEngine.UI.Slider element)
		{
			style.Apply(element);
		}
	}
}
