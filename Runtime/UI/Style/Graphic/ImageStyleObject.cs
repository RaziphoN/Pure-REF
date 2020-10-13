using UnityEngine;

namespace REF.Runtime.UI.Style.Graphic
{
	[CreateAssetMenu(fileName = "ImageStyle", menuName = "REF/UI/Style/Image Style")]
	public class ImageStyleObject : StyleObject<UnityEngine.UI.Image>
	{
		[SerializeField] private ImageStyle style = new ImageStyle();

		public override void Apply(UnityEngine.UI.Image element)
		{
			style.Apply(element);
		}

		public override void Copy(UnityEngine.UI.Image element)
		{
			style.Copy(element);
		}
	}
}
