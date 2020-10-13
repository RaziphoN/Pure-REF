using UnityEngine;

namespace REF.Runtime.UI.Style.Text
{
	[System.Serializable]
	public class ParagraphStyle : Style<UnityEngine.UI.Text>, IParagraphStyle
	{
		[SerializeField] private TextAnchor textAlignment = TextAnchor.UpperLeft;
		[SerializeField] private bool alignByGeometry;
		[SerializeField] private HorizontalWrapMode horizontalWrapMode = HorizontalWrapMode.Wrap;
		[SerializeField] private VerticalWrapMode verticalWrapMode = VerticalWrapMode.Truncate;
		[SerializeField] private bool bestFit = false;
		[SerializeField] private int minFitSize = 14;
		[SerializeField] private int maxFitSize = 14;

		public override void Apply(UnityEngine.UI.Text element)
		{
			element.alignment = GetAlignment();
			element.alignByGeometry = IsAlignByGeometry();
			element.horizontalOverflow = GetHorizontalWrapMode();
			element.verticalOverflow = GetVerticalWrapMode();
			element.resizeTextForBestFit = IsBestFit();
			element.resizeTextMinSize = GetMinFitFontSize();
			element.resizeTextMaxSize = GetMaxFitFontSize();
		}

		public override void Copy(UnityEngine.UI.Text element)
		{
			textAlignment = element.alignment;
			alignByGeometry = element.alignByGeometry;
			horizontalWrapMode = element.horizontalOverflow;
			verticalWrapMode = element.verticalOverflow;
			bestFit = element.resizeTextForBestFit;
			minFitSize = element.resizeTextMinSize;
			maxFitSize = element.resizeTextMaxSize;
		}

		public TextAnchor GetAlignment()
		{
			return textAlignment;
		}

		public HorizontalWrapMode GetHorizontalWrapMode()
		{
			return horizontalWrapMode;
		}

		public int GetMaxFitFontSize()
		{
			return maxFitSize;
		}

		public int GetMinFitFontSize()
		{
			return minFitSize;
		}

		public VerticalWrapMode GetVerticalWrapMode()
		{
			return verticalWrapMode;
		}

		public bool IsAlignByGeometry()
		{
			return alignByGeometry;
		}

		public bool IsBestFit()
		{
			return bestFit;
		}
	}
}
