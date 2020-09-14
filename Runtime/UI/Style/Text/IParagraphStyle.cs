using UnityEngine;

namespace REF.Runtime.UI.Style.Text
{
	public interface IParagraphStyle
	{
		bool IsAlignByGeometry();
		bool IsBestFit();

		HorizontalWrapMode GetHorizontalWrapMode();
		VerticalWrapMode GetVerticalWrapMode();
		TextAnchor GetAlignment();

		int GetMaxFitFontSize();
		int GetMinFitFontSize();
	}
}
