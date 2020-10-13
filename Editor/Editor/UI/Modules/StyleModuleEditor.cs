using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

using REF.Runtime.UI;
using REF.Runtime.UI.Module.Style;

using REF.Runtime.UI.Style;
using REF.Runtime.UI.Style.Text;
using REF.Runtime.UI.Style.Graphic;
using REF.Runtime.UI.Style.Selectable;

namespace REF.Editor.Editor.UI.Modules
{
	public abstract class StyleModuleEditor<T, U> : UnityEditor.Editor where T : UIBehaviour where U : StyleObject<T>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Apply style"))
			{
				var casted = target as StyleModule<T, U>;
				casted.Apply();
			}

			if (GUILayout.Button("Create style"))
			{
				var casted = target as StyleModule<T, U>;
				var style = casted.CreateStyle();

				if (style != null)
				{
					Selection.activeObject = style;
				}
			}
		}
	}

	[CustomEditor(typeof(ButtonStyleModule))]
	public class ButtonStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.Button, ButtonStyleObject> { }

	[CustomEditor(typeof(DropdownStyleModule))]
	public class DropdownStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.Dropdown, DropdownStyleObject> { }

	[CustomEditor(typeof(ImageStyleModule))]
	public class ImageStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.Image, ImageStyleObject> { }

	[CustomEditor(typeof(InputFieldStyleModule))]
	public class InputFieldStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.InputField, InputFieldStyleObject> { }

	[CustomEditor(typeof(RawImageStyleModule))]
	public class RawImageStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.RawImage, RawImageStyleObject> { }

	[CustomEditor(typeof(ScrollbarStyleModule))]
	public class ScrollbarStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.Scrollbar, ScrollbarStyleObject> { }

	[CustomEditor(typeof(ScrollRectStyleModule))]
	public class ScrollRectStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.ScrollRect, ScrollRectStyleObject> { }

	[CustomEditor(typeof(SliderStyleModule))]
	public class SliderStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.Slider, SliderStyleObject> { }

	[CustomEditor(typeof(TextStyleModule))]
	public class TextStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.Text, TextStyleObject> { }

	[CustomEditor(typeof(ToggleStyleModule))]
	public class ToggleStyleModuleEditor : StyleModuleEditor<UnityEngine.UI.Toggle, ToggleStyleObject> { }
}
