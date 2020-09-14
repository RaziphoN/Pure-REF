using UnityEngine;
using UnityEngine.UI;

namespace REF.Runtime.UI.Style.Selectable
{
	[System.Serializable]
	public class InputFieldStyle : Style<UnityEngine.UI.InputField>, IInputFieldStyle
	{
		private static char defaultAsteriskChar = '*';

		[SerializeField] private SelectableStyle selectable;
		[SerializeField] private UnityEngine.UI.InputField.InputType inputType = InputField.InputType.AutoCorrect;
		[SerializeField] private UnityEngine.UI.InputField.ContentType contentType = InputField.ContentType.Standard;
		[SerializeField] private UnityEngine.UI.InputField.LineType lineType = InputField.LineType.SingleLine;
		[SerializeField] private UnityEngine.UI.InputField.CharacterValidation characterValidation = InputField.CharacterValidation.None;
		[SerializeField] private UnityEngine.TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default;
		[SerializeField] private string asteriskChar = "*";
		[SerializeField, Min(0)] private int characterLimit = 0;
		[SerializeField, Range(0f, 4f)] private float caretBlinkRate = 0.85f;
		[SerializeField, Range(1, 5)] private int caretWidth = 1;
		[SerializeField] private bool customCaretColor = false;
		[SerializeField] private Color caretColor = new Color(0f, 0f, 0f, 1f);
		[SerializeField] private Color selectionColor = new Color(0.6588235f, 0.8078431f, 1f, 0.7529412f);
		[SerializeField] private bool hideMobileInput = false;
		[SerializeField] private bool readOnly = false;

		public override void Apply(UnityEngine.UI.InputField element)
		{
			selectable.Apply(element);
			element.inputType = GetInputType();
			element.contentType = GetContentType();
			element.lineType = GetLineType();
			element.characterValidation = GetCharacterValidation();
			element.keyboardType = GetKeyboardType();
			element.asteriskChar = GetAsteriskChar();
			element.characterLimit = GetCharacterLimit();
			element.caretBlinkRate = GetCaretBlinkRate();
			element.caretWidth = GetCaretWidth();
			element.customCaretColor = IsUseCustomCaretColor();
			element.caretColor = GetCutomCaretColor();
			element.selectionColor = GetSelectionColor();
			element.shouldHideMobileInput = IsHideMobileInput();
			element.readOnly = IsReadOnly();
		}

		public UnityEngine.UI.InputField.InputType GetInputType()
		{
			return inputType;
		}

		public UnityEngine.UI.InputField.LineType GetLineType()
		{
			return lineType;
		}

		public UnityEngine.UI.InputField.ContentType GetContentType()
		{
			return contentType;
		}

		public UnityEngine.UI.InputField.CharacterValidation GetCharacterValidation()
		{
			return characterValidation;
		}

		public int GetCharacterLimit()
		{
			return characterLimit;
		}

		public int GetCaretWidth()
		{
			return caretWidth;
		}

		public bool IsUseCustomCaretColor()
		{
			return customCaretColor;
		}

		public UnityEngine.Color GetCutomCaretColor()
		{
			return caretColor;
		}

		public float GetCaretBlinkRate()
		{
			return caretBlinkRate;
		}

		public bool IsHideMobileInput()
		{
			return hideMobileInput;
		}

		public UnityEngine.Color GetSelectionColor()
		{
			return selectionColor;
		}

		public UnityEngine.TouchScreenKeyboardType GetKeyboardType()
		{
			return keyboardType;
		}

		public char GetAsteriskChar()
		{
			if (!string.IsNullOrEmpty(asteriskChar))
			{
				return asteriskChar[0];
			}

			return defaultAsteriskChar;
		}

		public bool IsReadOnly()
		{
			return readOnly;
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
