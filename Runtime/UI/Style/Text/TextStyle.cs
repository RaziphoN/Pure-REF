using UnityEngine;

using REF.Runtime.UI.Style.Graphic;

namespace REF.Runtime.UI.Style.Text
{
	[System.Serializable]
	public class TextStyle : Style<UnityEngine.UI.Text>, ITextStyle
	{
		[SerializeField] private CharacterStyle character;
		[SerializeField] private ParagraphStyle paragraph;
		[SerializeField] private MaskableGraphicStyle graphic;

		public override void Apply(UnityEngine.UI.Text element)
		{
			character.Apply(element);
			paragraph.Apply(element);
			graphic.Apply(element);
		}

		public TextAnchor GetAlignment()
		{
			return paragraph.GetAlignment();
		}

		public Color GetColor()
		{
			return graphic.GetColor();
		}

		public Font GetFont()
		{
			return character.GetFont();
		}

		public int GetFontSize()
		{
			return character.GetFontSize();
		}

		public FontStyle GetFontStyle()
		{
			return character.GetFontStyle();
		}

		public HorizontalWrapMode GetHorizontalWrapMode()
		{
			return paragraph.GetHorizontalWrapMode();
		}

		public float GetLineSpacing()
		{
			return character.GetLineSpacing();
		}

		public Material GetMaterial()
		{
			return graphic.GetMaterial();
		}

		public int GetMaxFitFontSize()
		{
			return paragraph.GetMaxFitFontSize();
		}

		public int GetMinFitFontSize()
		{
			return paragraph.GetMinFitFontSize();
		}

		public VerticalWrapMode GetVerticalWrapMode()
		{
			return paragraph.GetVerticalWrapMode();
		}

		public bool IsAlignByGeometry()
		{
			return paragraph.IsAlignByGeometry();
		}

		public bool IsBestFit()
		{
			return paragraph.IsBestFit();
		}

		public bool IsMaskable()
		{
			return graphic.IsMaskable();
		}

		public bool IsRaycastTarget()
		{
			return graphic.IsRaycastTarget();
		}

		public bool IsRichText()
		{
			return character.IsRichText();
		}
	}
}
