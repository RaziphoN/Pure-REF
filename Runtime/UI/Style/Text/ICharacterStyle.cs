using UnityEngine;

namespace REF.Runtime.UI.Style.Text
{
	public interface ICharacterStyle
	{
		bool IsRichText();

		Font GetFont();
		FontStyle GetFontStyle();
		int GetFontSize();
		float GetLineSpacing();
	}
}
