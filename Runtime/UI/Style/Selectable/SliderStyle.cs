using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[System.Serializable]
	public class SliderStyle : Style<UnityEngine.UI.Slider>, ISliderStyle
	{
		[SerializeField] private SelectableStyle selectable;
		//[SerializeField] private Slider.Direction direction = Slider.Direction.LeftToRight;
		[SerializeField, Min(0)] private float minValue = 0f;
		[SerializeField, Min(0)] private float maxValue = 1f;
		[SerializeField] private bool wholeNumbers = false;

		public override void Apply(UnityEngine.UI.Slider element)
		{
			selectable.Apply(element);
			//element.direction = GetDirection();
			element.minValue = GetMinValue();
			element.maxValue = GetMaxValue();
			element.wholeNumbers = IsWholeNumbers();
		}

		public AnimationTriggers GetAnimationTriggers()
		{
			return selectable.GetAnimationTriggers();
		}

		public ColorBlock GetColors()
		{
			return selectable.GetColors();
		}

		//public Slider.Direction GetDirection()
		//{
		//	return direction;	
		//}

		public float GetMaxValue()
		{
			return maxValue;
		}

		public float GetMinValue()
		{
			return minValue;
		}

		public Navigation.Mode GetNavigationMode()
		{
			return selectable.GetNavigationMode();
		}

		public SpriteState GetSpriteState()
		{
			return selectable.GetSpriteState();
		}

		public UnityEngine.UI.Selectable.Transition GetTransition()
		{
			return selectable.GetTransition();
		}

		public bool IsWholeNumbers()
		{
			return wholeNumbers;
		}
	}
}
