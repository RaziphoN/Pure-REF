using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Graphic
{
	[System.Serializable]
	public class ImageStyle : Style<UnityEngine.UI.Image>, IImageStyle
	{
		[SerializeField] private Image.Type imageType = Image.Type.Simple;
		[SerializeField] private Image.FillMethod fillMethod = Image.FillMethod.Radial360;
		[SerializeField] private bool fillCenter = true;
		[SerializeField] private int fillOrigin = 0;
		[SerializeField] private float fillAmount = 1f;
		[SerializeField] private bool fillClockwise = false;

		[SerializeField] private bool useSpriteMesh = false;

		[SerializeField] private int pixerPerUnitMultiplier = 1;
		[SerializeField] private bool preserveAspect = true;

		[SerializeField] private MaskableGraphicStyle graphic = new MaskableGraphicStyle();

		public override void Apply(UnityEngine.UI.Image element)
		{
			graphic.Apply(element);

			element.type = GetImageType();
			element.fillMethod = GetFillMethod();
			element.fillCenter = IsFillCenter();
			element.fillOrigin = GetFillOrigin();
			element.fillAmount = GetFillAmount();
			element.fillClockwise = IsFillClockwise();
			element.useSpriteMesh = IsUseSpriteMesh();
			element.pixelsPerUnitMultiplier = GetPixelPerUnitMultiplier();
			element.preserveAspect = IsPreserveAspect();
		}

		public Color GetColor()
		{
			return graphic.GetColor();
		}

		public float GetFillAmount()
		{
			throw new System.NotImplementedException();
		}

		public Image.Type GetImageType()
		{
			return imageType;
		}

		public Image.FillMethod GetFillMethod()
		{
			return fillMethod;
		}

		public int GetFillOrigin()
		{
			return fillOrigin;
		}

		public Material GetMaterial()
		{
			return graphic.GetMaterial();
		}

		public int GetPixelPerUnitMultiplier()
		{
			return pixerPerUnitMultiplier;
		}

		public bool IsFillCenter()
		{
			return fillCenter;
		}

		public bool IsFillClockwise()
		{
			return fillClockwise;
		}

		public bool IsMaskable()
		{
			return graphic.IsMaskable();
		}

		public bool IsPreserveAspect()
		{
			return preserveAspect;
		}

		public bool IsRaycastTarget()
		{
			return graphic.IsRaycastTarget();
		}

		public bool IsUseSpriteMesh()
		{
			return useSpriteMesh;
		}
	}
}
