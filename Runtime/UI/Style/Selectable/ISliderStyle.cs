namespace REF.Runtime.UI.Style.Selectable
{
	public interface ISliderStyle : ISelectableStyle
	{
		// UnityEngine.UI.Slider.Direction GetDirection();

		float GetMinValue();
		float GetMaxValue();
		bool IsWholeNumbers();
	}
}
