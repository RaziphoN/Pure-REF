using UnityEngine;

namespace REF.Runtime.UI.Style.Graphic
{
	[CreateAssetMenu(fileName = "RawImageStyle", menuName = "REF/UI/Style/RawImage Style")]
	public class RawImageStyleObject : StyleObject<UnityEngine.UI.RawImage>
	{
		[SerializeField] private RawImageStyle style;

		public override void Apply(UnityEngine.UI.RawImage element)
		{
			style.Apply(element);
		}
	}
}
