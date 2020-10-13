using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[System.Serializable]
	public class SelectableStyle : Style<UnityEngine.UI.Selectable>, ISelectableStyle
	{
		[Space(10)]
		[SerializeField] private UnityEngine.UI.Selectable.Transition transition;
		[Header("Color Transition")]
		[SerializeField] private ColorBlock colors = ColorBlock.defaultColorBlock;
		[Header("Sprite Transition")]
		[SerializeField] private SpriteState sprites = new SpriteState();
		[Header("Animation Transition")]
		[SerializeField] private AnimationTriggers triggers = new AnimationTriggers();

		[Header("Other")]
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

		public override void Copy(UnityEngine.UI.Selectable element)
		{
			colors = element.colors;
			sprites = element.spriteState;
			transition = element.transition;
			triggers = element.animationTriggers;
			navigationMode = element.navigation.mode;
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
