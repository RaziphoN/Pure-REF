namespace REF.Runtime.UI.Style.Selectable
{
	public interface IInputFieldStyle : ISelectableStyle
	{
		UnityEngine.UI.InputField.InputType GetInputType();
		UnityEngine.UI.InputField.LineType GetLineType();
		UnityEngine.UI.InputField.ContentType GetContentType();
		UnityEngine.UI.InputField.CharacterValidation GetCharacterValidation();

		int GetCharacterLimit();
		int GetCaretWidth();

		bool IsUseCustomCaretColor();
		UnityEngine.Color GetCutomCaretColor();
		float GetCaretBlinkRate();

		bool IsHideMobileInput();
		UnityEngine.Color GetSelectionColor();
		UnityEngine.TouchScreenKeyboardType GetKeyboardType();

		char GetAsteriskChar();
		bool IsReadOnly();
	}
}
