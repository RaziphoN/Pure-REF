using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	public interface ISelectableStyle : IStyle
	{
		Navigation.Mode GetNavigationMode();
		ColorBlock GetColors();
		SpriteState GetSpriteState();
		AnimationTriggers GetAnimationTriggers();
		UnityEngine.UI.Selectable.Transition GetTransition();
	}
}
