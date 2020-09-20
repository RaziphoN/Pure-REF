using UnityEngine;
using UnityEditor;

using REF.Runtime.Online;

namespace REF.Editor.Drawer
{
	[CustomPropertyDrawer(typeof(Value))]
	public class CustomDrawerConstValue : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var typeProperty = property.FindPropertyRelative("type");

			position.height = EditorGUI.GetPropertyHeight(typeProperty);
			EditorGUI.PropertyField(position, typeProperty);
			position.y += position.height + EditorGUIUtility.standardVerticalSpacing;

			var type = (Type)typeProperty.enumValueIndex;

			switch (type)
			{
				case Type.String:
				{
					var stringProp = property.FindPropertyRelative("stringValue");
					position.height = EditorGUI.GetPropertyHeight(stringProp);
					EditorGUI.PropertyField(position, stringProp);
					position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				case Type.Long:
				{
					var longProp = property.FindPropertyRelative("longValue");
					position.height = EditorGUI.GetPropertyHeight(longProp);
					EditorGUI.PropertyField(position, longProp);
					position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				case Type.Double:
				{
					var doubleProp = property.FindPropertyRelative("doubleValue");
					position.height = EditorGUI.GetPropertyHeight(doubleProp);
					EditorGUI.PropertyField(position, doubleProp);
					position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				case Type.Bool:
				{
					var boolProp = property.FindPropertyRelative("boolValue");
					position.height = EditorGUI.GetPropertyHeight(boolProp);
					EditorGUI.PropertyField(position, boolProp);
					position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				default:
				{
					Debug.Log("Drawer of this type is not implemented!");
				}
				break;
			}

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var typeProperty = property.FindPropertyRelative("type");
			var height = EditorGUI.GetPropertyHeight(typeProperty) + EditorGUIUtility.standardVerticalSpacing;

			var type = (Type)typeProperty.enumValueIndex;

			switch (type)
			{
				case Type.String:
				{
					var stringProp = property.FindPropertyRelative("stringValue");
					height += EditorGUI.GetPropertyHeight(stringProp) + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				case Type.Long:
				{
					var longProp = property.FindPropertyRelative("longValue");
					height += EditorGUI.GetPropertyHeight(longProp) + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				case Type.Double:
				{
					var doubleProp = property.FindPropertyRelative("doubleValue");
					height += EditorGUI.GetPropertyHeight(doubleProp) + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				case Type.Bool:
				{
					var boolProp = property.FindPropertyRelative("boolValue");
					height += EditorGUI.GetPropertyHeight(boolProp) + EditorGUIUtility.standardVerticalSpacing;
				}
				break;

				default:
				{
					Debug.Log("Drawer of this type is not implemented!");
				}
				break;
			}

			return height;
		}
	}
}
