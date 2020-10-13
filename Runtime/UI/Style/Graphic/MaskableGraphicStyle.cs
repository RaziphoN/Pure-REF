using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Graphic
{
	[System.Serializable]
	public class MaskableGraphicStyle : Style<UnityEngine.UI.MaskableGraphic>, IMaskableGraphicStyle
	{
		[SerializeField] GraphicStyle graphic = new GraphicStyle();
		[SerializeField] private bool maskable = true;

		public override void Apply(MaskableGraphic element)
		{
			graphic.Apply(element);
			element.maskable = IsMaskable();
		}

		public override void Copy(MaskableGraphic element)
		{
			graphic.Copy(element);
			maskable = element.maskable;
		}

		public Color GetColor()
		{
			return graphic.GetColor();
		}

		public Material GetMaterial()
		{
			return graphic.GetMaterial();
		}

		public bool IsMaskable()
		{
			return maskable;
		}

		public bool IsRaycastTarget()
		{
			return graphic.IsRaycastTarget();
		}
	}
}
