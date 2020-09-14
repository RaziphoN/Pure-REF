using UnityEditor;
using UnityEngine;

using REF.Runtime.Data;

namespace REF.Editor.Drawer
{
	public abstract class CustomReferenceDrawer<T, K, U> : PropertyDrawer where T : Reference<U, K> where K : Variable<U>
	{
		private const float height = 17f;
		private const float btnWidth = 18f;

		private T reference;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			CheckInitialize(property, label);

			position.height = height;

			var controlRect = EditorGUI.PrefixLabel(position, label);

			var buttonRect = controlRect;
			buttonRect.x = controlRect.x + 2;
			buttonRect.width = btnWidth + 2;

			EditorGUI.BeginChangeCheck();

			var selected = (ReferenceType)EditorGUI.EnumPopup(buttonRect, reference.Type);
			reference.Type = selected;


			var fieldRect = controlRect;
			fieldRect.x = buttonRect.x + btnWidth + 4;
			fieldRect.width = controlRect.width - btnWidth - 4;

			switch (reference.Type)
			{
				case ReferenceType.Constant:
				{
					EditorGUI.BeginChangeCheck();
					var newValue = DrawerUtility.DoField(fieldRect, typeof(U), reference.Const);
					if (EditorGUI.EndChangeCheck())
					{
						reference.Const = newValue;
					}
				}
				break;

				case ReferenceType.Reference:
				{
					reference.Ref = EditorGUI.ObjectField(fieldRect, GUIContent.none, reference.Ref, typeof(K), true) as K;
				}
				break;
			}

			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(property.serializedObject.targetObject);
			}
		}

		private void CheckInitialize(SerializedProperty property, GUIContent label)
		{
			var target = property.serializedObject.targetObject;
			reference = fieldInfo.GetValue(target) as T;
		}
	}

	[CustomPropertyDrawer(typeof(BoolReference))]
	public class BoolReferenceDrawer : CustomReferenceDrawer<BoolReference, BoolVariable, bool> { }

	[CustomPropertyDrawer(typeof(IntReference))]
	public class IntReferenceDrawer : CustomReferenceDrawer<IntReference, IntVariable, int> { }

	[CustomPropertyDrawer(typeof(FloatReference))]
	public class FloatReferenceDrawer : CustomReferenceDrawer<FloatReference, FloatVariable, float> { }

	[CustomPropertyDrawer(typeof(DoubleReference))]
	public class DoubleReferenceDrawer : CustomReferenceDrawer<DoubleReference, DoubleVariable, double> { }

	[CustomPropertyDrawer(typeof(StringReference))]
	public class StringReferenceDrawer : CustomReferenceDrawer<StringReference, StringVariable, string> { }
}
