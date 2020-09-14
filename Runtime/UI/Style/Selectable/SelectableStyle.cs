using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[System.Serializable]
	public class SelectableStyle : Style<UnityEngine.UI.Selectable>, ISelectableStyle
	{
		[SerializeField] private ColorBlock colors;
		[SerializeField] private SpriteState sprites;
		[SerializeField] private UnityEngine.UI.Selectable.Transition transition;
		[SerializeField] private AnimationTriggers triggers;
		[SerializeField] private Navigation.Mode navigationMode;

		public override void Apply(UnityEngine.UI.Selectable element)
		{
			element.colors = GetColors();
			element.spriteState = GetSpriteState();
			element.transition = GetTransition();
			element.animationTriggers = GetAnimationTriggers();

			var navigation = element.navigation;
			navigation.mode = GetNavigationMode();
			element.navigation = navigation;
		}

		public AnimationTriggers GetAnimationTriggers()
		{
			return triggers;
		}

		public ColorBlock GetColors()
		{
			return colors;
		}

		public Navigation.Mode GetNavigationMode()
		{
			return navigationMode;
		}

		public SpriteState GetSpriteState()
		{
			return sprites;
		}

		public UnityEngine.UI.Selectable.Transition GetTransition()
		{
			return transition;
		}
	}
}
