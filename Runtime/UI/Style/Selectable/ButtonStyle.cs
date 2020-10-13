using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[System.Serializable]
	public class ButtonStyle : Style<UnityEngine.UI.Button>, IButtonStyle
	{
		[SerializeField] private SelectableStyle selectable = new SelectableStyle();

		public override void Apply(UnityEngine.UI.Button element)
		{
			selectable.Apply(element);
		}

		public override void Copy(UnityEngine.UI.Button element)
		{
			selectable.Copy(element);
		}

		public AnimationTriggers GetAnimationTriggers()
		{
			return selectable.GetAnimationTriggers();
		}

		public ColorBlock GetColors()
		{
			return selectable.GetColors();
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
	}
}
