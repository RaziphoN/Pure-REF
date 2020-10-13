using UnityEngine;

namespace REF.Runtime.UI.Style.Graphic
{
	[System.Serializable]
	public class RawImageStyle : Style<UnityEngine.UI.RawImage>, IRawImageStyle
	{
		[SerializeField] private MaskableGraphicStyle graphic;

		public override void Apply(UnityEngine.UI.RawImage element)
		{
			graphic.Apply(element);
		}

		public override void Copy(UnityEngine.UI.RawImage element)
		{
			graphic.Copy(element);
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
			return graphic.IsMaskable();
		}

		public bool IsRaycastTarget()
		{
			return graphic.IsRaycastTarget();
		}
	}
}
