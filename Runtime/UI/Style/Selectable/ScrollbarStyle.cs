using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[System.Serializable]
	public class ScrollbarStyle : Style<UnityEngine.UI.Scrollbar>, IScrollbarStyle
	{
		[SerializeField] private SelectableStyle selectable;
		//[SerializeField] private Scrollbar.Direction direction = Scrollbar.Direction.LeftToRight;
		[SerializeField, Range(0f, 1f)] private float size = 0.2f;
		[SerializeField, Range(0, 11)] private int numberOfSteps = 0;

		public override void Apply(UnityEngine.UI.Scrollbar element)
		{
			selectable.Apply(element);
			//element.direction = GetDirection();
			element.size = size;
			element.numberOfSteps = numberOfSteps;
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

		public Navigation.Mode GetNavigationMode()
		{
			return selectable.GetNavigationMode();
		}

		public int GetNumberOfSteps()
		{
			return numberOfSteps;
		}

		public float GetSize()
		{
			return size;
		}

		public SpriteState GetSpriteState()
		{
			return selectable.GetSpriteState();
		}

		public UnityEngine.UI.Selectable.Transition GetTransition()
		{
			return selectable.GetTransition();
		}
	}
}
