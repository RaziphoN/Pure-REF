using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Graphic
{
	[System.Serializable]
	public class MaskableGraphicStyle : Style<UnityEngine.UI.MaskableGraphic>, IMaskableGraphicStyle
	{
		[SerializeField] GraphicStyle graphic;
		[SerializeField] private bool maskable = true;

		public override void Apply(MaskableGraphic element)
		{
			graphic.Apply(element);
			element.maskable = IsMaskable();
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
