using UnityEngine;

namespace REF.Runtime.UI.Style.Text
{
	[System.Serializable]
	public class CharacterStyle : Style<UnityEngine.UI.Text>, ICharacterStyle
	{
		[SerializeField] private bool richText = false;
		[SerializeField] private Font font;
		[SerializeField] private FontStyle fontStyle = FontStyle.Normal;
		[SerializeField] private int fontSize = 14;
		[SerializeField] private float lineSpacing = 1;

		public override void Apply(UnityEngine.UI.Text element)
		{
			element.supportRichText = IsRichText();
			element.font = GetFont();
			element.fontSize = GetFontSize();
			element.fontStyle = GetFontStyle();
			element.lineSpacing = GetLineSpacing();
		}

		public Font GetFont()
		{
			return font;
		}

		public int GetFontSize()
		{
			return fontSize;
		}

		public FontStyle GetFontStyle()
		{
			return fontStyle;
		}

		public float GetLineSpacing()
		{
			return lineSpacing;
		}

		public bool IsRichText()
		{
			return richText;
		}
	}
}
