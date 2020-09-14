using UnityEngine;
using static UnityEngine.UI.Image;

namespace REF.Runtime.UI.Style.Graphic
{
	public interface IImageStyle : IMaskableGraphicStyle
	{
		bool IsUseSpriteMesh();
		bool IsPreserveAspect();
		bool IsFillCenter();
		bool IsFillClockwise();

		Type GetImageType();
		FillMethod GetFillMethod();
		int GetFillOrigin();
		float GetFillAmount();

		int GetPixelPerUnitMultiplier();
	}
}
