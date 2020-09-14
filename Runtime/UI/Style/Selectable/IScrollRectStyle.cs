namespace REF.Runtime.UI.Style.Selectable
{
	public interface IScrollRectStyle : IStyle
	{
		bool UseHorizontal();
		UnityEngine.UI.ScrollRect.ScrollbarVisibility GetHorizontalScrollbarVisibility();
		float GetHorizontalScrollbarSpacing();

		bool UseVertical();
		UnityEngine.UI.ScrollRect.ScrollbarVisibility GetVerticalScrollbarVisibility();
		float GetVerticalScrollbarSpacing();

		UnityEngine.UI.ScrollRect.MovementType GetMovementType();
		bool UseIntertia();

		float GetDecelerationRate();
		float GetSensivity();
		float GetElasticity();
	}
}
